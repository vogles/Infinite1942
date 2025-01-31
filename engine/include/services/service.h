#pragma once

class IService
{
public:
	virtual ~IService() {}

	virtual void Initialize() = 0;
	virtual void Shutdown() = 0;
};