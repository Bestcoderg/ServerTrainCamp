#ifndef _EVENTSYSTEM_H_
#define _EVENTSYSTEM_H_

#include "util.h"
#include "Singleton.h"
#include "MsgHandler.h"
#include "EntityMgr.h"
#include <functional>


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
    
    INT32 PlayerRegister(MesgInfo &stHead, const char *body, const INT32 len);




private:
    //MsgHandler<Player>* m_msgHandler;
    MsgHandler<EventSystem>* m_msgHandler;
};

#endif
