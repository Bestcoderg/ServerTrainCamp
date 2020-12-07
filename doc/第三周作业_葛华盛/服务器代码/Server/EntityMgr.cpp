#include "../include/EntityMgr.h"

INSTANCE_SINGLETON(EntityMgr)

EntityMgr::EntityMgr()
{
    m_EntityMap.clear();
    m_PlayerMap.clear();
    m_CreateEntityMap.clear();
}

EntityMgr::~EntityMgr()
{
    m_EntityMap.clear();
    m_PlayerMap.clear();
    m_CreateEntityMap.clear();
}

bool EntityMgr::Init()
{
    m_eidpos = 1000;
    //PlayerEntity
    m_CreateEntityMap[ENTITY_PLAYER].push_back(COMPONENTS_POSITION);
    m_CreateEntityMap[ENTITY_PLAYER].push_back(COMPONENTS_ORIENT);
    m_CreateEntityMap[ENTITY_PLAYER].push_back(COMPONENTS_SPEED);


    //Entity...
    return true;
}

void EntityMgr::Uinit()
{

    return;
}

INT32 EntityMgr::CreatePlayer(INT32 uid)
{
    if( m_PlayerMap.find(uid) != m_PlayerMap.end() )
    {
        std::cout << "Player has already exist! Playerid = " << uid << std::endl;
        return -1;
    }
    m_PlayerMap[uid] = m_eidpos;
    Entity* tmp = new Entity(m_eidpos);
    std::vector<INT32>& vec = m_CreateEntityMap[ENTITY_PLAYER];
    for(INT32 i = 0; i < (INT32)vec.size();i++)
    {
        if(tmp->AddComponent(vec[i]) == false)
        {
            std::cout << "Player Entity add components failed!" << std::endl;
            return -1;
        }
    }
    m_EntityMap[m_eidpos] = tmp;
    m_eidpos++;
    std::cout << "Player register in server! Playerid = " << uid << std::endl;
    return 0; 
}
