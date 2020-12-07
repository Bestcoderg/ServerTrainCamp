#ifndef _LOGINMGR_H_
#define _LOGINMGR_H_

#include "util.h"
#include "Singleton.h"

class RouterMgr
{
private:
    RouterMgr();
    ~RouterMgr();
    DECLARE_SINGLETON(RouterMgr);
public:
    bool Init();
    void Uinit();
    
    bool PlayerRegister(INT32 uid);   
    bool GateServerRegister(INT32 connfd, std::string);
    

    
private:
    std::map<INT32, INT32> m_registedPlayerMap;  // playerid(uid) -> gateserver(connfd),已经登录的玩家id对应相应gateserver的connfd 
    std::map<INT32, std::string> m_gateServerMap;  // gateserver(connfd) -> ip addr,保存gate的ip传给玩家


};

#endif
