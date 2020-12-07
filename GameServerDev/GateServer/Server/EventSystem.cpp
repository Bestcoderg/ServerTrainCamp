#include "../include/EventSystem.h"
#include "../protocal/MsgID.pb.h"
#include "../include/SocketServer.h"
#include "../protocal/GameSpec.pb.h"
#include "../include/MySqlMgr.h"
#include "../include/CacheMgr.h"

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
    m_msgHandler->RegisterMsg(MSGID::MSG_SERVER_REGISTER_RSP, &EventSystem::ServerRegisterRsp);
    m_msgHandler->RegisterMsg(MSGID::MSG_SERVER_REGISTER_REQ, &EventSystem::ServerRegisterReq);
    m_msgHandler->RegisterMsg(MSGID::MSG_PLAYER_REGISTERTOGATE, &EventSystem::PlayerRegisterToGate);
    return true;
}

void EventSystem::Uinit()
{
    m_msgHandler->Uinit();
    delete m_msgHandler;
    m_msgHandler = nullptr;
}


INT32 EventSystem::ServerRegisterRsp(const MesgInfo &stHead, const char *body,const INT32 len,const INT32 connfd)
{
    GameSpec::ServerRegisterRsp rsp;
    if(!rsp.ParseFromArray(body, len))
    {
        std::cout << "ParseFromArray palyer login failed" <<std::endl;
        return -1;
    }

    if(rsp.errcode() == GameSpec::ERROR_SERVER_REGISTER_SUCCESS)
    {
        SocketServer::Instance()->RegisterLinkType(connfd,rsp.type());
        std::cout << "Server registered to Login server success!" << std::endl;
    }
    else 
    {
        std::cout << "Server registered failed, exit!" << std::endl;
        exit(0);
    }
    return 0;
}

INT32 EventSystem::ServerRegisterReq(const MesgInfo &stHead, const char *body,const INT32 len,const INT32 connfd)
{
    GameSpec::ServerRegisterReq req;
    GameSpec::ServerRegisterRsp rsp;
    MesgInfo rsphed ;
    rsphed.msgID = MSG_SERVER_REGISTER_RSP;
    rsphed.uID = stHead.uID;

    if(!req.ParseFromArray(body, len))
    {
        std::cout << "ParseFromArray server register failed" <<std::endl;
        rsp.set_errcode(GameSpec::ERROR_PARSE_FAILED);
        rsphed.packLen = rsp.ByteSizeLong();
        SocketServer::Instance()->SendMsg(rsphed, rsp, connfd);
        return -1;
    }
    
    if(req.type() == GameSpec::TYPE_GAMESERVER)
    {
        SocketServer::Instance()->RegisterLinkType(connfd,req.type());
        SocketServer::Instance()->RegisterGameServer(connfd);
        std::cout << "GameServer registered to Login server success! connfd = "<<connfd << std::endl;
        rsp.set_type(GameSpec::TYPE_GATESERVER);
    }
    rsp.set_errcode(GameSpec::ERROR_SERVER_REGISTER_SUCCESS);

    rsphed.packLen = rsp.ByteSizeLong();
    SocketServer::Instance()->SendMsg(rsphed, rsp, connfd);
    return 0;
}

INT32 EventSystem::PlayerRegisterToGate(const MesgInfo &stHead, const char *body,const INT32 len,const INT32 connfd)
{
    // player register to gate
    GameSpec::PlayerRegisterToGateReq req;
    GameSpec::PlayerRegisterToGateRsp rsp;

    if(!req.ParseFromArray(body, len))
    {
        std::cout << "ParseFromArray player register failed" <<std::endl;
        rsp.set_errcode(GameSpec::ERROR_PARSE_FAILED);
        SocketServer::Instance()->SendMsg(stHead, rsp, connfd);
        return -1;
    }

    if(SocketServer::Instance()->RegisterPlayer2G(req.uid()) == false)
    {
        //TODO 设置回包
    }

    rsp.set_errcode(GameSpec::ERROR_NO_ERROR);
    
    
    return 0;
}
