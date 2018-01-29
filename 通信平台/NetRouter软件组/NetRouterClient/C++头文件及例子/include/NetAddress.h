#ifndef NET_ADDRESS_H
#define NET_ADDRESS_H

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

class Address;
class COMMUNIEXPORTS NetAddress{
public:
	NetAddress(const unsigned char& code = 0, 
		const unsigned char& type = 0, 
		const unsigned short& id = 0,
		const unsigned char& devtype = 0, 
		const unsigned short &devid = 0);
	NetAddress(const NetAddress& netaddr);
	NetAddress& operator=(const NetAddress& netaddr);
	virtual ~NetAddress();
	unsigned char getBureauCode() const;
	unsigned char  getUnitType() const;
	unsigned short getUnitId() const;
	unsigned char  getDevType() const;
	unsigned short getDevId() const;
	void setBureauCode(const unsigned char &code);
	void setUnitType(const unsigned char &type);
	void setUnitId(const unsigned short &id);
	void setDevType(const unsigned char &type);
	void setDevId(const unsigned short &id);

private:
	unsigned char bureauCode_;
	unsigned char unitType_;
	unsigned short unitId_;
	unsigned char devType_;
	unsigned short devId_;
};

}

#endif