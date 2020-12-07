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
    inline void RegisterLinkType(INT32 connfd,INT32 type) { m_epoll.RegisterType(connfd,type); }
    inline bool RegisterPlayer2G(INT32 uid) { 
        if(m_playertable2G.find(uid) != m_playertable2G.end())
        {
            return false;
        }
        if(m_gamelinker.size() == 0)
        {
            return false;
        }
        pos = pos%m_gamelinker.size();
        std::cout << "!!!!!!!!!!!!!!!!!!uid = " << uid <<" m_gamelinke connfd = " <<  m_gamelinker[pos] << std::endl;
        m_playertable2G[uid] = m_gamelinker[pos++]; 
        return true; 
    }
    inline bool RegisterPlayer2C(INT32 uid,INT32 connfd) { 
        if(m_playertable2C.find(uid) != m_playertable2C.end())
        {
            return false;
        }
        m_playertable2C[uid] = connfd; 
        return true; 
    }
    //TODO:目前没有校验这个connfd是否正确
    inline void RegisterGameServer(INT32 connfd) { m_gamelinker.push_back(connfd); } 
private:
    Epoll m_epoll;

    INT32  m_basefd;
    INT32 m_LoginServerfd;
    INT32 pos = 0;
    bool OpenSocket(INT32 port);
    
    std::unordered_map<INT32, INT32>  m_playertable2G;
    std::unordered_map<INT32, INT32>  m_playertable2C;
    std::vector<INT32> m_gamelinker;
};

#endif
