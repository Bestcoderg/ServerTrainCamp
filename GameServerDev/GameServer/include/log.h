//#include "error_code.h"
#include <iostream>
/*
#define CHECK_RTN(rtn) \
{ \
    int tmp = (rtn); \
    if(tmp != SUCC) \
    { \
            std::cout << "\033[31m[ERROR]\33[0m " << __FILE__ \
                          << "(" << __func__ << ": " << __LINE__ \
                          << "), error_code: " << tmp << std::endl; \
                return tmp; \
            } \
}

#define CHECK_RTN_LOG(rtn, log) \
{ \
    int tmp = (rtn); \
    if(tmp != SUCC) \
    { \
            std::cout << "\033[31m[ERROR]\033[0m " << __FILE__ \
                          << "(" << __func__ << ": " << __LINE__ \
                          << "), error_code: " << tmp << ", " << log << std::endl; \
                return tmp; \
            } \
}

#define CHECK_POINTER_RTN(ptr, log) \
{ \
    if(ptr == nullptr) \
    { \
            std::cout << "\033[31m[ERROR]\33[0m " << __FILE__ \
                          << "(" << __func__ << ": " << __LINE__ \
                          << "), error_code: " << NULL_POINTER_ERROR << ", " << log << std::endl; \
                return NULL_POINTER_ERROR; \
            } \
}*/

#define LOGINFO(info) \
{ \
    std::cout << "[INFO] " << __FILE__ <<"(" << __func__ \
                  << ": " << __LINE__ << "): " << info << std::endl; \
}

#define LOGERROR(info) \
{ \
    std::cout << "\033[31m[ERROR]\033[0m " << __FILE__ \
                  << "(" << __func__ << ": " << __LINE__ << "): " << info << std::endl; \
}



