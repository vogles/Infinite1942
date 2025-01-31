cmake_minimum_required(VERSION 3.10)
project(Infinite1942_engine VERSION 0.0.1 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)

# add external libraries
add_subdirectory(external/glm)
add_subdirectory(external/glfw)

#file(GLOB SOURCES "src/**.cpp")
#add_library(${PROJECT_NAME} ${SOURCES})
add_library(${PROJECT_NAME} src/engine.cpp "include/services/window.h" "include/service_locator.h" "src/platform/GLWindow.h" "src/platform/GLWindow.cpp"  "include/game.h" "src/game.cpp" "include/services/service.h")

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