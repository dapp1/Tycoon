using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, Inherited = false)]
public class InjectAttribute : Attribute
{
}