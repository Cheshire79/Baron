using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baron.View.LobbyView;
using Ninject;

namespace Assets.Scripts.View
{
    public class ViewFactory : AbstractViewFactory
    {
        private readonly IKernel _kernel;

        public ViewFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override ILobbyView CreateLobbyView()
        {
           // if (!MainThreadRunner.IsMainThread) todo
            //    Logger.LogException(new InvalidOperationException("CreateLobbyView should be run from the Main thread"));
            return _kernel.Get<ILobbyView>();
        }


    }
}
