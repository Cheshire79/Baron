using UnityEngine;

namespace Uniject.Impl
{
    public class UnityLayerMask : ILayerMask
    {
        public int NameToLayer(string name)
        {
            return LayerMask.NameToLayer(name);
        }
    }
}