#pragma once
#include <map>
#include <memory>
#include <string>
#include <services/window.h>

class ServiceLocator
{
private:
	std::map<std::string, IService*> _services;
	static inline std::unique_ptr<Window> _window = NULL;

public:
	static inline const std::unique_ptr<Window>& GetWindow() { return _window; }
	static inline void Provide(Window* pWindow)
	{
		if (_window != NULL) { return; }

		_window = std::unique_ptr<Window>(pWindow);
	}

	void RegisterInstance(IService *pService);
	IService* GetInstance(std::string serviceName);
};