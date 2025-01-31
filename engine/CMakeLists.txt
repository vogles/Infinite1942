cmake_minimum_required(VERSION 3.10)
project(Infinite1942_engine VERSION 0.0.1 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)

# add external libraries
add_subdirectory(external/glm)
add_subdirectory(external/glfw)

#file(GLOB SOURCES "src/**.cpp")
#add_library(${PROJECT_NAME} ${SOURCES})
add_library(${PROJECT_NAME} 
	"include/services/service.h"
	"include/services/window.h" 
	"include/game.h" 
	"include/service_locator.h" 
	"src/platform/GLWindow.h" 
	"src/platform/GLWindow.cpp"  
	"src/engine.cpp"
	"src/game.cpp" 
	"src/service_locator.cpp"
)

target_include_directories(${PROJECT_NAME}
	PUBLIC
		$<INSTALL_INTERFACE:include>
		$<BUILD_INTERFACE:${CMAKE_CURRENT_SOURCE_DIR}/include>
	PRIVATE
		glm
		glfw
)

target_link_libraries(${PROJECT_NAME}
	PUBLIC

	PRIVATE
		glm
		glfw
)