cmake_minimum_required(VERSION 3.0)
project(NetWorkdev)

set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)


add_subdirectory(./Server)

add_subdirectory(./Client)
