#ifndef RECV_MESSAGE_H
#define RECV_MESSAGE_H

#include <string>

#include "NetAddress.h"

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

class COMMUNIEXPORTS RecvMessage{
public:
	RecvMessage();
	RecvMessage(NetAddress srcAddr, std::string msg);
	RecvMessage(const RecvMessage& recvMsg);
	RecvMessage& operator=(const RecvMessage& recvMsg);
	virtual ~RecvMessage();
	void setSrcAddr(NetAddress srcAddr);
	void setMessage(std::string msg);
	NetAddress getSrcAddr();
	std::string getMessage();

private:
	NetAddress srcAddr_;
	std::string msg_;
};

}

#endif