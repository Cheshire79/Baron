using UnityEngine;

namespace Uniject.Impl
{
    public class UnityGameObject : TestableGameObject
    {
        public UnityGameObject(GameObject obj) : base(new UnityTransform(obj))
        {
            this.obj = obj;
            obj.AddComponent<UnityGameObjectBridge>().wrapping = this;
        }

        public GameObject obj { get; private set; }

        public override bool activeSelf
        {
            get { return obj.activeSelf; }
        }

        public override string name
        {
            get { return obj.name; }
            set { obj.name = value; }
        }

        public override int layer
        {
            get { return obj.layer; }
            set { obj.layer = value; }
        }

        public override void Destroy()
        {
            base.Destroy();
            Object.Destroy(obj);
        }

        public override void SetActive(bool value)
        {
            obj.SetActive(value);
        }
    }
}