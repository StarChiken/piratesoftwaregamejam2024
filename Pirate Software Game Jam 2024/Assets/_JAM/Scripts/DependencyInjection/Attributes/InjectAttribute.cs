using System;

namespace DesignPatterns.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public sealed class InjectAttribute : Attribute { }
}