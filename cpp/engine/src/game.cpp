#include <game.h>
#include <service_locator.h>
#include "platform/GLWindow.h"

Game::Game() : Game(800, 600, "Game Window") { }

Game::Game(int width, int height, std::string windowTitle) : _windowWidth(width), _windowHeight(height), _title(windowTitle), _isInitialized(false)
{
}

void Game::Initialize()
{
	// Provide a window
}

void Game::Shutdown()
{
}

void Game::Run()
{
	if (!_isInitialized)
	{
		Initialize();
		_isInitialized = true;
	}

	ServiceLocator::Provide(new GLWindow());
	const std::unique_ptr<Window>& window = ServiceLocator::GetWindow();

	window->OpenWindow(_title);

	while (!window->Update())
	{

	}
}
