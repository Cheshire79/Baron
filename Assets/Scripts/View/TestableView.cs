using System;
using Uniject;
using Uniject.Impl;
using UnityEngine;


namespace Baron.View
{
    public class TestableView : TestableComponent, IView
    {
        // todo panel
        private Transform _panelLowest;
        public virtual Transform transform { get; set; }
        public virtual string name { get; set; }

        public TestableView(TestableGameObject obj)
            : base(obj)
        {
            transform = ((UnityGameObject)obj).obj.transform;
            name = ((UnityGameObject)obj).obj.name;
          //  transform.parent = Panel;
            transform.localScale = Vector3.one;
            Obj.SetActive(false);
        }

        //private Transform Panel
        //{
        //    get
        //    {// because another  UIpanel can be at scrollList
        //        if (_panelLowest == null)
        //        {
        //         //   var panels = Object.FindObjectsOfType<UIPanel>();
        //         //   _panelLowest = panels.OrderBy(s => s.depth).First().transform;
        //        }
        //        return _panelLowest;
        //    }
        //}

        public virtual void Show(int zIndex)
        {
            Obj.SetActive(true);
        }

        public virtual void Show()
        {
            Obj.SetActive(true);
        }
        public virtual void Hide()
        {
            Obj.SetActive(false);
        }
        public virtual void Close()
        {
            Obj.SetActive(false);
        }

        public virtual void Invalidate()
        {
            throw new NotImplementedException();
        }

        public virtual bool IsShown()
        {
            return Obj.activeSelf;
        }

        public event Action BackButtonClicked;

        protected virtual void OnBackButtonClicked()
        {
            Action handler = BackButtonClicked;
            if (handler != null) handler();
        }

        public override void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                OnBackButtonClicked();
        }
    }
}
