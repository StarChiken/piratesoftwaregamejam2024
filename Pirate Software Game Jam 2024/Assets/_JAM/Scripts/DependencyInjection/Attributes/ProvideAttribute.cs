using System;

namespace DesignPatterns.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class ProvideAttribute : Attribute { }
}