#include "GLWindow.h"

GLWindow::GLWindow()
{
	window = NULL;
}

void GLWindow::Initialize()
{
	glfwInit();
}

void GLWindow::Shutdown()
{
	glfwDestroyWindow(window);
}

void GLWindow::OpenWindow(std::string windowTitle)
{
	window = glfwCreateWindow(1280, 720, windowTitle.c_str(), NULL, NULL);
}

bool GLWindow::Update()
{
	glfwPollEvents();

	return glfwWindowShouldClose(window);
}