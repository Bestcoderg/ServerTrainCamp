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
    m_msgHandler->RegisterMsg(MSGID::MSG_SERVER_REGISTER_REQ, &EventSystem::ServerRegister);
    
    return true;
}

void EventSystem::Uinit()
{
    m_msgHandler->Uinit();
    delete m_msgHandler;
    m_msgHandler = nullptr;
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
    
    if(req.type() == GameSpec::TYPE_GAMESERVER)
    {
        SocketServer::Instance()->RegisterLinkType(connfd, GameSpec::TYPE_GAMESERVER);
        std::cout << "GameServer register in DBServer! connfd = " << connfd << std::endl;
        rsp.set_type(GameSpec::TYPE_DBSERVER);
    }


    rsp.set_errcode(GameSpec::ERROR_SERVER_REGISTER_SUCCESS);
    rsphed.packLen = rsp.ByteSizeLong();
    SocketServer::Instance()->SendMsg(rsphed, rsp, connfd);

    return 0;
}
