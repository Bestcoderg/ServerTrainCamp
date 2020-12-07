#ifndef _TRANSFORMCOMPONENT_H_
#define _TRANSFORMCOMPONENT_H_

#include "../include/util.h"
#include "../include/BaseComponent.h"

// enum = 1
class PositionComponent : public BaseComponent 
{
public:
    INT32 x,y;
    PositionComponent(){};
    ~PositionComponent(){};
    bool Init(INT32 x,INT32 y) 
    {
        this->x = x;
        this->y = y;
        return true;
    }
    FUNCCLONE(PositionComponent);
};
// enum = 2
class OrientComponent : public BaseComponent
{
public:
    INT32 y;
    OrientComponent(){};
    ~OrientComponent(){};
    bool Init(INT32 y) 
    {
        this->y = y;
        return true;
    }
    FUNCCLONE(OrientComponent);
};
// enum = 3
class SpeedComponent : public BaseComponent
{
public:
    INT32 speed;
    SpeedComponent(){};
    ~SpeedComponent(){};
    bool Init(INT32 speed){ this->speed = speed; return true; };
    FUNCCLONE(SpeedComponent);
};

#endif
