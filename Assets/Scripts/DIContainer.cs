using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DIContainer
{
    private readonly Dictionary<Type, object> _registrations = new Dictionary<Type, object>();
    
    public void Register<T>(T instance)
    {
        Type initializableType = instance.GetType();
        _registrations[initializableType] = instance;
    }

    public object Resolve(Type type)
    {
        return _registrations.TryGetValue(type, out var instance) ? instance : null;
    } 
    
    public T Resolve<T>()
    {
        return (T)Resolve(typeof(T));
    }

    public void InjectDependencies(object target)
    {
        if (target == null) return;
        Type targetType = target.GetType();

        // Внедрение в поля
        foreach (var field in targetType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            if (field.GetCustomAttribute<InjectAttribute>() != null)
            {
                object dependency = Resolve(field.FieldType);
                if (dependency != null)
                {
                    field.SetValue(target, dependency);
                }
            }
        }
        
        foreach (var method in targetType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            if (method.GetCustomAttribute<InjectAttribute>() != null)
            {
                var parameters = method.GetParameters();
                List<object> resolvedDependencies = new List<object>(parameters);

                for (int i = 0; i < parameters.Length; i++)
                {
                    resolvedDependencies[i] = Resolve(parameters[i].ParameterType);
                    if (resolvedDependencies[i] == null)
                    {
                        return;
                    }
                }

                method.Invoke(target, resolvedDependencies.ToArray());
            }
        }
    }
}