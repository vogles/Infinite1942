#include <engine.h>
#include <iostream>
#include <memory>
#include <window.h>

#include "platform/GLWindow.h"
#include <service_locator.h>

void GameEngine::PrintHelloWorld()
{
	std::cout << "Hello World from the game engine" << std::endl;

	ServiceLocator::Provide(new GLWindow());
	const std::unique_ptr<Window>& window = ServiceLocator::GetWindow();

	window->OpenWindow("GL Window");

	while (!window->Update())
	{

	}
}