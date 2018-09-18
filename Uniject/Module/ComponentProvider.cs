using System;
using Ninject.Activation;

namespace Uniject.Impl
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class Component : Attribute
    {
        public string Path { get; private set; }

        public Component(string path)
        {
            Path = path;
        }
    }

    public class ComponentProvider<T> : Provider<T> where T : TestableComponent
    {
        protected override T CreateInstance(IContext context)
        {
            var resource = Scoping.getContextAttribute<Component>(context);
            //resource.Path
            if (resource == null)
            {
                throw new ArgumentException("Injected resources must have Resource attributes");
            }

            return default(T);
        }
    }
}