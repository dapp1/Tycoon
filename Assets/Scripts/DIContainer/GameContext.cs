
public class GameContext
{
    public static DIContainer Container = new();
    
    public static void Register<T>(T instance)
    {
        Container.Register(instance);
    }
    
    public static T Resolve<T>()
    {
        return Container.Resolve<T>();
    }

    public static void InjectDependencies(object target)
    {
        Container.InjectDependencies(target);
    }
}
