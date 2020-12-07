#ifndef _EVENTSYSTEM_H_
#define _EVENTSYSTEM_H_

#include "util.h"
#include "Singleton.h"
#include "MsgHandler.h"
//#include "EntityMgr.h"
#include <functional>
//#include "../systems/BagSystem.h"

class EventSystem
{
private:
    EventSystem();
    ~EventSystem();
    DECLARE_SINGLETON(EventSystem);
public:
    bool Init();
    void Uinit();

    MsgHandler<EventSystem>*  GetMsgHandler() { return m_msgHandler; }
    
    INT32 ServerRegisterRsp(const MesgInfo &stHead, const char *body,const INT32 len,const INT32 connfd);
    INT32 ServerRegisterReq(const MesgInfo &stHead, const char *body,const INT32 len,const INT32 connfd);
    INT32 PlayerRegisterToGate(const MesgInfo &stHead, const char *body,const INT32 len,const INT32 connfd);
private:
    MsgHandler<EventSystem>* m_msgHandler;

    //BagSystem m_bagSystem;
};

#endif
