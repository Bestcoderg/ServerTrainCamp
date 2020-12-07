#ifndef _TRANSFORMSYSTEM_H_
#define _TRANSFORMSYSTEM_H_

#include "../include/util.h"
#include "../components/TransformComponent.h"

class TransformSystem
{
public:
    TransformSystem();
    ~TransformSystem();

    void UpdatePosition(const PositionComponent & pos,const OrientComponent & ori,const SpeedComponent & spe);    











};
#endif
