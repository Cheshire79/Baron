using System;
using Ninject.Activation;

namespace Uniject
{
    public abstract class Scoping
    {
        /*
         * Ninject scoping function; traverse the activation context hierarchy to the root, or a type 
         * that has a GameObjectBoundary attribute.
         */

        public static object GameObjectBoundaryScoper(IContext context)
        {
            if (context.Request.Target != null)
            {
                if (context.Request.Target.IsDefined(typeof (GameObjectBoundary), true) ||
                    context.Request.Target.Type.IsDefined(typeof (GameObjectBoundary), true))
                {
                    return context.Request.Target;
                }
            }

            if (context.Request.ParentContext != null)
            {
                return GameObjectBoundaryScoper(context.Request.ParentContext);
            }

            return context;
        }

        public static T getContextAttribute<T>(IContext context) where T : Attribute
        {
            if (context.Request.Target != null && context.Request.Target.IsDefined(typeof (T), false))
            {
                return (T) context.Request.Target.GetCustomAttributes(typeof (T), false)[0];
            }

            if (context.Request.ParentContext != null)
            {
                return getContextAttribute<T>(context.Request.ParentContext);
            }

            return null;
        }
    }
}