namespace DesignPatterns.DependencyInjection.Interfaces
{
    public interface IDynamicObjectProvider
    {
        T GetDynamicObject<T>();
    }
}