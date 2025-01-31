#pragma once
#include <window.h>
#include <GLFW/glfw3.h>

class GLWindow : public Window
{
private:
	GLFWwindow* window;
public:
	GLWindow();

	virtual void OpenWindow(std::string windowTitle) override;
	virtual bool Update() override;
};