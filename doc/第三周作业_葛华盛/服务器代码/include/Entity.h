#ifndef _ENTITY_H_
#define _ENTITY_H_

#include "util.h" 
#include "BaseComponent.h"
#include "ComponentsMgr.h"

class Entity
{
public:
    Entity(INT32 id){ m_id = id; m_components.clear(); return; }
    ~Entity(){ m_components.clear(); return; }
    //void SetID(INT32 id) { m_id = id; return; }
    INT32 GetID() { return m_id; }
    bool AddComponent(INT32 comtypeID)
    {
        if(m_components.find(comtypeID) != m_components.end())
        {
            std::cout << "components has alreader, comtypeid = "<< comtypeID << std::endl;
            return false;
        }
        m_components[comtypeID] = ComponentsMgr::Instance()->GetComponent(comtypeID);
        if(m_components[comtypeID] == nullptr) 
        {
            std::cout << "m_components[] == null " << std::endl;
            return false;
        }
        return true;
    }
    BaseComponent* GetComponent(INT32 comtypeID)
    {
        return m_components[comtypeID];
    }
private:
    INT32 m_id;
    std::unordered_map<INT32, BaseComponent*> m_components;

};
#endif
