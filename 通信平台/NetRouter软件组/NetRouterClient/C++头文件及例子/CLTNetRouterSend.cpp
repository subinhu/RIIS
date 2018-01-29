/*
测试用例内容：
向所有的其它客户端发送一定量的数据，收到全部回复后再发送下一包
*/

#include <cstdlib>
#include <iostream>
#include <windows.h>
#include "NetAddress.h"
#include "SentMessage.h"
#include "RecvMessage.h"
#include "NetRouterClient.h"
#include "Log.h"
#include "utility.h"

using namespace std;
using namespace Netrouter;
const int SENDSIZE = 100; //发送内容的大小，单位是KB
const int CLTREVNO = 98; //总接收数据客户端的个数
const int CLTINSTATION = 49;//每个站应有的设备数
const char * IP1 = "191.168.99.98";
const int PORT1 = 9003;
const char * IP2 = "191.169.99.98";
const int PORT2 = 9005;

std::string TakeRandString(const int lenth){
    std::string randString;
    int len = 0;
    while(len <= lenth){
        char app[10];
        int j = rand();
        if( j > 0){
            memset(app, 0 ,10);
            sprintf(app, "%d%s", j, "H");
            randString.append(app, strlen(app));
            len += strlen(app);
        }
    }
    return randString;
}

bool SendData(NetRouterClient &netClient, std::vector<NetAddress> &dst){
    SentMessage sMessage(dst, TakeRandString(SENDSIZE*1024));
    if(!netClient.sendMessage(sMessage)){
        printf("Client : 1 Send Rand String False!!!\n");
        LOG_INFO("Client : 1 Send Rand String False!!!");
        return false;
    }
    return true;
}

int main(int argc, char* argv[]){
    CLogger::InitLogger();
    NetAddress localAddr(8,2,0,1,6);
    NetRouterClient netClient("CLTNetRouterSend", IP1, PORT1, IP2,  PORT2, localAddr);
    
    std::vector<NetAddress> destAddrs;
    NetAddress dst;
    dst.setBureauCode(8);
    dst.setUnitType(1);
    int no = (CLTREVNO / (CLTINSTATION+1)) + 1;//每个单位里最多有20个设备
    for(int i = 1; i <= no; i++){
        dst.setUnitId(i);
        destAddrs.push_back(dst);
    }
    
    uint32_t sendTime = 0;
    uint32_t RevAckNo = 0;

    if(netClient.init()){
        while(!netClient.start()){
            Sleep(1000);
        }
        printf("Client : 1 Connect to Server suc!!!\n");
        LOG_INFO("Client : 1 Connect to Server suc!!!");

        if(!SendData(netClient, destAddrs)){
            printf("Client : 1 Send Rand String False!!!\n");
            LOG_INFO("Client : 1 Send Rand String False!!!");
            return 0;
        }
        sendTime = CUtility::GetCurrentStamp64();
        while(true){
            if(netClient.isNet1Connected() || netClient.isNet2Connected()){
                RecvMessage revMes;
                while(netClient.receiveMessage(revMes)){
                    LOG_INFO("Client : 1 Take Data , RevAckNo : " << RevAckNo);
                    if(RevAckNo == 0){
                        uint32_t elaptime = CUtility::GetElapseTick(sendTime);
                        printf("Client : 1 Rev ack first , Elaptime is %d\n", elaptime);
                    }
                    if( (revMes.getMessage().length() == 3) && ((++RevAckNo) >= CLTREVNO) ){
                        RevAckNo = 0;
                        uint32_t elaptime = CUtility::GetElapseTick(sendTime);
                        printf("Client : 1 Rev ack enough , Elaptime is %d\n", elaptime);
                        LOG_INFO("Client : 1 Rev ack enough , Elaptime is "<<elaptime);

                        if(SendData(netClient, destAddrs))
                            sendTime = CUtility::GetCurrentStamp64();
                    }else if(revMes.getMessage().length() != 3){
                        //printf("Client : %d Rev Wrong data! \n", _Client.GetCltId());
                        LOG_ERROR("Client : 1 Rev Wrong data! ");
                    }
                }
                Sleep(30);
            }
        }
    }
#ifdef WIN32
    system("PAUSE");
#else
    pause();
#endif
    return 0;
}
