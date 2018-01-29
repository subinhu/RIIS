#ifndef SEND_MESSAGE_H
#define SEND_MESSAGE_H

#include <vector>
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


namespace  Netrouter{

class COMMUNIEXPORTS SentMessage{
public:
	SentMessage();
	SentMessage(std::vector<NetAddress> destAddrs, std::string msg);
	SentMessage(const SentMessage& sendMsg);
	SentMessage& operator=(const SentMessage& sendMsg);
	virtual ~SentMessage();
	void setDestAddrs(std::vector<NetAddress> destAddrs);
	void setMessage(std::string msg);
	std::vector<NetAddress> getDestAddrs();
	std::string getMessage();

private:
	std::vector<NetAddress> destAddrs_;
	std::string msg_;
};

}

#endif