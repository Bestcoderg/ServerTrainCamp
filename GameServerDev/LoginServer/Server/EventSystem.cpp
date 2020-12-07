#include "../include/EventSystem.h"
#include "../protocal/MsgID.pb.h"
#include "../include/SocketServer.h"
#include "../protocal/GameSpec.pb.h"
#include "../include/MySqlMgr.h"
#include "../include/CacheMgr.h"
#include "../include/RouterMgr.h" 
//using namespace google::protobuf;

INSTANCE_SINGLETON(EventSystem)

EventSystem::EventSystem()
{
    m_msgHandler = nullptr;
    m_msgHandler = new MsgHandler<EventSystem>();
}

EventSystem::~EventSystem()
{

}

bool EventSystem::Init()
{
    // 绑定处理函数
    m_msgHandler->RegisterMsg(MSGID::MSG_PLAYER_LOGIN, &EventSystem::PlayerLogin);
    m_msgHandler->RegisterMsg(MSGID::MSG_SERVER_REGISTER_REQ, &EventSystem::ServerRegister);
    
    return true;
}

void EventSystem::Uinit()
{
    m_msgHandler->Uinit();
    delete m_msgHandler;
    m_msgHandler = nullptr;
}

INT32 EventSystem::PlayerLogin(const MesgInfo &stHead, const char *body,const INT32 len,const INT32 connfd)
{
    //std::cout << "msgid " << stHead.msgID << std::endl;
    //std::cout << "uid " << stHead.uID << std::endl;
    //std::cout << "lens " << stHead.packLen << std::endl;
    GameSpec::CtlMsgLoginReq req;
    GameSpec::CtlMsgLoginRsp rsp;
    if(!req.ParseFromArray(body, len))
    {
        std::cout << "ParseFromArray palyer login failed" <<std::endl;
        rsp.set_errcode(GameSpec::ERROR_PARSE_FAILED);
        SocketServer::Instance()->SendMsg(stHead, rsp, connfd);
        return -1;
    }

    
    std::cout << "login  uid = " << stHead.uID << std::endl; 
    std::cout << "name = " << req.name() << std::endl;    
    std::cout << "password = " << req.password() << std::endl;
    
    //id / passwd
    std::pair<INT32, std::string> pswdinfo = MySqlMgr::Instance()->GetPlayerLoginInfo(req.name());
    if(pswdinfo.first == -1)
    {
        std::cout << "Player not exist!" <<std::endl;
        rsp.set_errcode(GameSpec::ERROR_PLAYER_NOTEXIST);
        SocketServer::Instance()->SendMsg(stHead, rsp, connfd);
        return -1;
    }
    if(req.password() != (std::string)pswdinfo.second)
    {
        std::cout << "Passwd is wrong!" <<std::endl;
        rsp.set_errcode(GameSpec::ERROR_PASSWD_WRONG);
        SocketServer::Instance()->SendMsg(stHead, rsp, connfd);
        return -1;
        
    }

    
    // ----- test --------
    
    // connfd , port, ip
    std::pair<INT32,std::pair<INT32, std::string>> t_ginfo = RouterMgr::Instance()->GetGateInfo();
    rsp.set_ip(t_ginfo.second.second);
    rsp.set_port(t_ginfo.second.first);
    rsp.set_uid(pswdinfo.first);
    rsp.set_errcode(GameSpec::ERROR_NO_ERROR);
    
    GameSpec::PlayerRegisterToGateReq tGReq;
    tGReq.set_uid(pswdinfo.first);
    MesgInfo tGhed;
    tGhed.msgID = MSG_PLAYER_REGISTERTOGATE;
    tGhed.uID = 0;
    tGhed.packLen = tGReq.ByteSizeLong();

    // ----- test -------
    

    SocketServer::Instance()->SendMsg(stHead, rsp, connfd);
    SocketServer::Instance()->SendMsg(tGhed, tGReq, t_ginfo.first);
    return 0;
}

//Loginserver 只有req的操作没有rsp操作
INT32 EventSystem::ServerRegister(const MesgInfo &stHead, const char *body,const INT32 len,const INT32 connfd)
{
    GameSpec::ServerRegisterReq req;
    GameSpec::ServerRegisterRsp rsp;
    MesgInfo rsphed;
    rsphed.msgID = (INT32)MSG_SERVER_REGISTER_RSP;
    rsphed.uID = stHead.uID;
    if(!req.ParseFromArray(body, len))
    {
        std::cout << "ParseFromArray palyer login failed" <<std::endl;
        rsp.set_errcode(GameSpec::ERROR_PARSE_FAILED);
        rsp.set_type(GameSpec::TYPE_UNKNOW);
        rsphed.packLen = rsp.ByteSizeLong();
        SocketServer::Instance()->SendMsg(rsphed, rsp, connfd);
        return -1;
    }
    
    if(req.type() == GameSpec::TYPE_GATESERVER)
    {
        if(RouterMgr::Instance()->GateServerRegister(connfd, req.port(), req.ip()) == false)
        {
            std::cout << "GateServer has been Registered. Register failed!" <<std::endl;
            rsp.set_errcode(GameSpec::ERROR_SERVER_REGISTER_FAILED);
            SocketServer::Instance()->SendMsg(stHead, rsp, connfd);
            return -1;
        }
        SocketServer::Instance()->RegisterLinkType(connfd, GameSpec::TYPE_GATESERVER);
        std::cout << "GateServer register in LoginServer! connfd = " << connfd << std::endl;
        rsp.set_type(GameSpec::TYPE_LOGINSERVER);
    }


    rsp.set_errcode(GameSpec::ERROR_SERVER_REGISTER_SUCCESS);
    rsphed.packLen = rsp.ByteSizeLong();
    SocketServer::Instance()->SendMsg(rsphed, rsp, connfd);

    return 0;
}
