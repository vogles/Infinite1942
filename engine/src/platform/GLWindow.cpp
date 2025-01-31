#include "GLWindow.h"

GLWindow::GLWindow()
{
	window = NULL;
}

void GLWindow::OpenWindow(std::string windowTitle)
{
	glfwInit();

	window = glfwCreateWindow(1280, 720, windowTitle.c_str(), NULL, NULL);
}

bool GLWindow::Update()
{
	glfwPollEvents();

	return glfwWindowShouldClose(window);
}