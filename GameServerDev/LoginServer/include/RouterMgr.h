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
    bool GateServerRegister(INT32 connfd, INT32 port, std::string ip);
    std::pair<INT32,std::pair<INT32, std::string>> GetGateInfo();

    
private:
    std::map<INT32, INT32> m_registedPlayerMap;  // playerid(uid) -> gateserver(connfd),已经登录的玩家id对应相应gateserver的connfd 
    std::map<INT32, std::pair<INT32,std::string>> m_gateServerMap;  // gateserver(connfd) -> type
    INT32 pos = 0;
    std::vector<INT32> m_gateServerVec ; // gateserver's connfd

};

#endif
