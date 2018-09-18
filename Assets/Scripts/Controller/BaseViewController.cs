using Baron.View;

namespace Barons.Controller
{
    public interface IBaseViewController
    {
        bool IsOnTop(IBaseViewController baseViewController);
        void ShowView();
        void ShowView(bool back);
        void ShowModal();
        void Close();
        IView View { get; }
        void CloseFromAplication();
        void Hide();
    }

    public abstract class BaseViewController : IBaseViewController
    {
        protected readonly IViewStack ViewStack;
        public abstract IView View { get; }

        protected BaseViewController(IViewStack viewStack)
        {
            ViewStack = viewStack;
        }

        public bool IsOnTop(IBaseViewController baseViewController)
        {
            return ViewStack.Peek() == baseViewController;
        }

        protected virtual void Back()
        {
            if (ViewStack.Peek() == this)// run if only on the top
            {
                ViewStack.Pop().Close();
                if (ViewStack.Any())
                    ((BaseViewController)ViewStack.Peek()).ShowView(true);
            }
        }

        public virtual void ShowView()
        {
            ShowView(false);
        }

        public virtual void ShowView(bool back)
        {
            if (!back)
            {
                if (ViewStack.Any())
                    ViewStack.Peek().Close();
                ViewStack.Push(this);
            }
            View.Show();
        }

        protected virtual void BackModal()
        {
            if (ViewStack.Peek() == this)// run if only on the top // we can InvitationToPlay from another player, so Modal window can be not at the top
                ViewStack.Pop().Close();
        }


        public virtual void ShowModal()
        {
            ViewStack.Push(this);
            View.Show(ViewStack.TopNumber());
        }
        public virtual void CloseFromAplication()
        {
            ViewStack.Remove(this);
            View.Close();

        }

        public void Close()
        {
            View.Close();
        }
        public void Hide()
        {
            View.Hide();
        }
    }
}
