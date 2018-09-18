using Barons.Controller;

namespace Baron.View
{
    public interface IViewStack
        // todo rename
    {
        /// <summary>
        /// Inserts IView object at the top of the ViewStack
        /// </summary>
        /// <param name="view">IView object to push onto the ViewStack</param>
        void Push(IBaseViewController view);
        /// <summary>
        /// Removes and returns IView object at the top of the ViewStack.
        /// </summary>
        /// <returns>The object removed from the top of the ViewStack</returns>
        IBaseViewController Pop();
        void Remove(IBaseViewController baseViewController);
        /// <summary>
        /// Returns IView object at the top of the ViewStack without removing
        /// </summary>
        /// <returns>The object at the top of the ViewStack</returns>
        IBaseViewController Peek();
        bool Any();
        int Count();
        int TopNumber();
    }
}
