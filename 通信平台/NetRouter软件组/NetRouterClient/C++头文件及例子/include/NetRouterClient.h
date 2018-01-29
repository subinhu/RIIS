#ifndef NET_ROUTER_CLIENT_H
#define NET_ROUTER_CLIENT_H

#include <string>

#include "SentMessage.h"
#include "RecvMessage.h"

#ifdef __linux__
#define COMMUNIEXPORTS
#else

#ifdef COMMUNICATION_EXPORTS
#define COMMUNIEXPORTS _declspec(dllexport)
#else
#define COMMUNIEXPORTS _declspec(dllimport)
#endif

#endif


namespace Netrouter{

class CommunicateClient;
class COMMUNIEXPORTS NetRouterClient{
public:
	NetRouterClient(std::string appName);
	NetRouterClient(std::string appName, std::string ip1, int port1, std::string ip2,  int port2, const NetAddress& localAddr, std::string reginfo = "");
	virtual ~NetRouterClient();
	bool init();
	bool start();
	bool sendMessage(SentMessage& sendMsg);
	bool receiveMessage(RecvMessage& recvMsg);
	bool isNet1Connected();
	bool isNet2Connected();
	void close();

private:
	CommunicateClient* pCommClient_;
};

}

#endif