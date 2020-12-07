#ifndef _ENTITYMGR_H_
#define _ENTITYMGR_H_

#include "Entity.h"
#include "Singleton.h"

class EntityMgr
{
private:
    EntityMgr();
    ~EntityMgr();
    DECLARE_SINGLETON(EntityMgr)
public:
    bool Init();
    void Uinit();

    //正常来说直接有一个createentity就可以了，但是要做uid->entityid
    //的映射，不知道这个特判怎么处理，先直接写一个CreatePlayer
    //todo！
    INT32 CreatePlayer(INT32 uid);


      

private:
    std::unordered_map<INT32, Entity*> m_EntityMap;  //entityid -> entity
    std::unordered_map<INT32, INT32> m_PlayerMap;   //playerid -> entityid
    std::unordered_map<INT32, std::vector<INT32>> m_CreateEntityMap; // entity type id -> components[]
    INT32 m_eidpos; // 代表当前创建的entity的id，这个要优化
};

#endif
