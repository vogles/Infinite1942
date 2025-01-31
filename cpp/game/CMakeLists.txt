cmake_minimum_required(VERSION 3.10)
project(Infinite1942 VERSION 0.0.1 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)

add_executable(${PROJECT_NAME} src/main.cpp src/infinite1942Game.cpp include/infinite1942Game.h)

target_include_directories(${PROJECT_NAME} PRIVATE include Infinite1942_engine)
target_link_libraries(${PROJECT_NAME} Infinite1942_engine)