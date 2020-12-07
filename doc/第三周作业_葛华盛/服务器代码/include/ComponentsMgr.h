#ifndef _COMPONENTSMGR_H_
#define _COMPONENTSMGR_H_

#include "util.h"
#include "Singleton.h"
#include "BaseComponent.h"

class ComponentsMgr
{
private:
    ComponentsMgr();
    ~ComponentsMgr();
    DECLARE_SINGLETON(ComponentsMgr)
public:
    bool Init();
    void Uinit();
    BaseComponent* GetComponent(INT32 comtypeid);
    

private:
    std::unordered_map<INT32, BaseComponent*> m_ComponentsMap;

};
#endif
