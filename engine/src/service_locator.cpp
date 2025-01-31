#include <service_locator.h>

void ServiceLocator::RegisterInstance(IService *pService)
{
    auto serviceType = typeid(pService).name();
    _services.emplace(serviceType, pService);
}

IService* ServiceLocator::GetInstance(std::string serviceName)
{
    if (_services.find(serviceName) != _services.end())
    {
        return _services[serviceName];
    }
    
    return NULL;
}
