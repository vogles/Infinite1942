#pragma once
#include <string>

class Game
{
private:
	int _windowWidth;
	int _windowHeight;
	std::string _title;
	bool _isInitialized;
public:
	Game();
	Game(int width, int height, std::string windowTitle);
	~Game() = default;

	void Run();

protected:
	virtual void Initialize();
	void Shutdown();
};