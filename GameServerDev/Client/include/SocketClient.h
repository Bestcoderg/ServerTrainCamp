#ifndef _SOCKETSERVER_H_
#define _SOCKETSERVER_H_

#include "util.h"
#include "Singleton.h"
#include "baselink.h"
#include "Epoll.h"
#include "MesgHead.h"

class SocketClient
{
public:
    SocketClient();
    ~SocketClient();                            
    bool Init();
    void Uinit();
    void Dojob(INT32 port = DEFAULT_SERVER_PORT,std::string name = "jack1",std::string passwd = "jack1");
    inline void BroadCast(const MesgInfo& msghead, Message& msg){ m_epoll.BroadCast(msghead, msg); }
    inline void SendMsg(const MesgInfo& msghead, Message& msg, const INT32 connfd){ m_epoll.SendMsg(msghead, msg, connfd); }
    inline void RegisterLinkType(INT32 connfd, INT32 type) { m_epoll.RegisterType(connfd, type); }
private:
    INT32  m_basefd;
    INT32 m_gatefd;
    Epoll m_epoll;
    bool m_register = false;
    INT32 m_uid = 0;
    bool OpenSocket(INT32 port);
};

#endif
