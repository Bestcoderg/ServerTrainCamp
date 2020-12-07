#ifndef _SOCKETSERVER_H_
#define _SOCKETSERVER_H_

#include "util.h"
#include "Singleton.h"
#include "baselink.h"
#include "Epoll.h"
#include "MesgHead.h"

class SocketServer
{
private:
    SocketServer();
    ~SocketServer();                            
    DECLARE_SINGLETON(SocketServer)
public:
    bool Init();
    void Uinit();
    void Dojob(INT32 port = DEFAULT_SERVER_PORT);
    inline void BroadCast(const MesgInfo& msghead, Message& msg){ m_epoll.BroadCast(msghead, msg); }
    inline void SendMsg(const MesgInfo& msghead, Message& msg, const INT32 connfd){ m_epoll.SendMsg(msghead, msg, connfd); }
    inline void RegisterLinkType(INT32 connfd, INT32 type) { m_epoll.RegisterType(connfd, type); }
    


private:
    INT32  m_basefd;
    Epoll m_epoll;

    bool OpenSocket(INT32 port);
};

#endif
