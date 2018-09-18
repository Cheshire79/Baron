
using System;
using CustomTools;
using Ninject.Parameters;
using UnityEngine;
using Ninject;

namespace Baron.DI
{
   public class InjectionRoot : MonoBehaviour
    {
        public string TypeToInstantiate;
        public void Start()
        {
            CustomLogger.Log("InjectionRoot Started ");
            Type service = Type.GetType(TypeToInstantiate);
            BaronInjectors.get().Get(service, new IParameter[] { });
        }
    }
}
