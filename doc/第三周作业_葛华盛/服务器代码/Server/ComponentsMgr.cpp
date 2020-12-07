#include "../include/ComponentsMgr.h"
#include "../components/TransformComponent.h"


INSTANCE_SINGLETON(ComponentsMgr)

ComponentsMgr::ComponentsMgr()
{
    m_ComponentsMap.clear();
}

ComponentsMgr::~ComponentsMgr()
{

}

bool ComponentsMgr::Init()
{
    // components register
    m_ComponentsMap[COMPONENTS_POSITION] = new PositionComponent(); 
    m_ComponentsMap[COMPONENTS_ORIENT] = new OrientComponent(); 
    m_ComponentsMap[COMPONENTS_SPEED] = new SpeedComponent(); 

    return true;
}

void ComponentsMgr::Uinit()
{
   m_ComponentsMap.clear(); 
    return;
}

BaseComponent* ComponentsMgr::GetComponent(INT32 comtypeid)
{
   if(m_ComponentsMap.find(comtypeid) == m_ComponentsMap.end())
   {
       return nullptr;
   }

    return m_ComponentsMap[comtypeid]->Clone();
}
