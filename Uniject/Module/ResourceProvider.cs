using System;
using Ninject.Activation;
using Object = UnityEngine.Object;

namespace Uniject.Impl
{
    public class ResourceProvider<T> : Provider<T> where T : Object
    {
        private readonly IResourceLoader loader;

        public ResourceProvider(IResourceLoader loader)
        {
            this.loader = loader;
        }

        protected override T CreateInstance(IContext context)
        {
            var resource = Scoping.getContextAttribute<Resource>(context);
            if (resource == null)
            {
                throw new ArgumentException("Injected resources must have Resource attributes");
            }

            return loader.loadResource<T>(resource.Path);
        }
    }
}