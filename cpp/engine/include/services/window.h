#pragma once
#include "service.h"
#include <string>

class Window : public IService
{
public:
	virtual void Initialize() override { }
	virtual void Shutdown() override { }

	virtual void OpenWindow(std::string windowTitle) = 0;
	virtual bool Update() = 0;
};