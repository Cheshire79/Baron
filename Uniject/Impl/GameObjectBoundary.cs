using System;

namespace Uniject
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter)]
    public class GameObjectBoundary : Attribute
    {
    }
}