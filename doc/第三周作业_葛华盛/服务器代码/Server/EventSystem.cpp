#include "../include/EventSystem.h"
#include "../protocal/MsgID.pb.h"

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
    m_msgHandler->RegisterMsg(MSGID::MSG_TEST, &EventSystem::PlayerRegister);
    
    return true;
}

void EventSystem::Uinit()
{
    m_msgHandler->Uinit();
}

INT32 EventSystem::PlayerRegister(MesgInfo &stHead, const char *body, const INT32 len)
{
    std::cout << "PlayerRegister in " << std::endl;
    EntityMgr::Instance()->CreatePlayer(2131);    

    return 0; 
}
