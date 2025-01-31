#pragma once
#include <services/window.h>
#include <GLFW/glfw3.h>

class GLWindow : public Window
{
private:
	GLFWwindow* window;
public:
	GLWindow();
	
	virtual void Initialize() override;
	virtual void Shutdown() override;

	virtual void OpenWindow(std::string windowTitle) override;
	virtual bool Update() override;
};