#ifndef _BASECOMPONENT_H_
#define _BASECOMPONENT_H_

#define FUNCCLONE(TYPE) \
    TYPE *Clone() override\
    {\
        return new TYPE();\
    }\


class BaseComponent
{
public:
    BaseComponent() {}
    ~BaseComponent() {}
    virtual BaseComponent* Clone() = 0;


};
#endif
