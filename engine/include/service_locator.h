#pragma once
#include <memory>
#include <window.h>

class ServiceLocator
{
private:
	static inline std::unique_ptr<Window> _window = NULL;

public:
	static inline const std::unique_ptr<Window>& GetWindow() { return _window; }
	static inline void Provide(Window* pWindow)
	{
		if (_window != NULL) { return; }

		_window = std::unique_ptr<Window>(pWindow);
	}
};