#include "../include/RouterMgr.h"

INSTANCE_SINGLETON(RouterMgr)

RouterMgr::RouterMgr()
{

}

RouterMgr::~RouterMgr()
{

}

bool RouterMgr::Init()
{
    m_registedPlayerMap.clear();
    m_gateServerMap.clear();
    m_gateServerVec.clear();
    return true;
}

void RouterMgr::Uinit()
{
    m_registedPlayerMap.clear();
    m_gateServerMap.clear();
    m_gateServerVec.clear();
}

bool RouterMgr::PlayerRegister(INT32 uid)
{
    


    return true;
}

bool RouterMgr::GateServerRegister(INT32 connfd, INT32 port, std::string ip)
{
    if(m_gateServerMap.find(connfd) != m_gateServerMap.end())
    {
        return false;
    }
    m_gateServerMap[connfd] = std::make_pair(port,ip);
    m_gateServerVec.push_back(connfd);
    return true;
}

std::pair<INT32,std::pair<INT32,std::string>> RouterMgr::GetGateInfo()
{
    //todo
    pos = pos%m_gateServerVec.size();
    INT32 t_connfd = m_gateServerVec[pos++];
    auto it = m_gateServerMap.find(t_connfd);
    return std::make_pair(it->first,it->second);
}
