using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using Uniject.Impl;
using UnityEngine;

namespace Baron
{
    public class BaronInjectors
    {
        private static IKernel kernel;

        public static object levelScope = new object();

        public static IKernel get()
        {
            if (null == kernel)
            {
                kernel = new StandardKernel(new UnityNinjectSettings(), new INinjectModule[]
                {
                    new UnityModule(),
                    new LateBoundModule(),
                    new BaronModule()
                });

                var listener = new GameObject();
                Object.DontDestroyOnLoad(listener);
                listener.name = "LevelLoadListener";
                listener.AddComponent<LevelLoadListener>();
            }
            return kernel;
        }

        private static object scoper(IContext context)
            //todo
        {
            return levelScope;
        }
    }
}
