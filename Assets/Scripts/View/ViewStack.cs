using System;
using System.Collections.Generic;
using Barons.Controller;
using CustomTools;

namespace Baron.View
{
    public class ViewStack : IViewStack
    {// todo made thred independent
        // private readonly Stack<IBaseViewController> _stack = new Stack<IBaseViewController>();
        private readonly List<IBaseViewController> _stack = new List<IBaseViewController>();
        private int _topNumber;
        private readonly Object _thisLock = new Object();
        public void Push(IBaseViewController view)
        {
            lock (_thisLock) // todo really need ?
            {
                //if (_stack.Exists((x => x == view))) // because  of Lobby controller ()
                //{
                //    Remove(view);
                //    _stack.Add(view);
                //    string text = "";
                //    foreach (var item in _stack)
                //        text = text + " " + item;
                //    Logger.LogWarning(" OnPushed  ----" + text);
                //    Logger.LogWarning("   ----" + view);

                //}
                //else
                //{
                _topNumber++;
                _stack.Add(view);
                string text = "";
                //  _stack.Push(view);
                foreach (var item in _stack)
                    text = text + " " + item;
                CustomLogger.LogWarning(" OnPushed  ----" + text);
                CustomLogger.LogWarning(" Pushed  ----" + view);
                // }
            }
        }

        public IBaseViewController Pop()
        {
            lock (_thisLock) // todo really need ?
            {
                _topNumber--;
                string text = "";
                foreach (var item in _stack)
                    text = text + " " + item;
                IBaseViewController element = _stack[_stack.Count - 1];
                _stack.RemoveAt(_stack.Count - 1);
                CustomLogger.LogWarning(" OnPop  ----" + text);
                CustomLogger.LogWarning(" Poped  ----" + element);
                return element;
            }
        }

        public void Remove(IBaseViewController baseViewController)
        {
            lock (_thisLock) // todo really need ?
            {
                string text = "";
                foreach (var item in _stack)
                    text = text + " " + item;
                CustomLogger.LogWarning("re = " + baseViewController + " OnRemove  ----" + text);
                List<IBaseViewController> elements = _stack.FindAll(x => x == baseViewController);
                if (elements.Count > 1)
                    CustomLogger.LogWarning("More then one View: Error");
                if (elements.Count < 1)
                    CustomLogger.LogWarning("No View: Error");
                if (elements.Count == 1)
                {
                    if (elements[0].IsOnTop(elements[0]))
                    {
                        _topNumber--;
                    }
                    _stack.Remove(elements[0]);
                    CustomLogger.LogWarning(" Removed  ----" + elements[0]);
                }
            }


        }

        public IBaseViewController Peek()
        {
            return _stack[_stack.Count - 1];
            // return _stack.Peek();
        }

        public bool Any()
        {
            return _stack.Count > 0;
        }

        public int Count()
        {
            return _stack.Count;

        }
        public int TopNumber()
        {
            return _topNumber;
        }
    }
}
