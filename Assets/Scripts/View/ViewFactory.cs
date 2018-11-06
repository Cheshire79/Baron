using Baron.Tools;
using Baron.View.BranchView;
using Baron.View.LobbyView;
using CustomTools;
using Ninject;
using System;

namespace Baron.View
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
		public override IBranchView CreateBranchView()
		{
			// if (!MainThreadRunner.IsMainThread) todo
			//    Logger.LogException(new InvalidOperationException("CreateLobbyView should be run from the Main thread"));
			return _kernel.Get<IBranchView>();
		}

		public override TestableView CreateView<T>()
		{
			if (!MainThreadRunner.IsMainThread)
				CustomLogger.LogException(new InvalidOperationException("CreateView should be run from the Main thread"));
			return _kernel.Get<T>();
		}

	}
}
