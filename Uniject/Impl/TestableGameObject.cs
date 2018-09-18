using System.Collections.Generic;

namespace Uniject
{
    /// <summary>
    ///     A testable equivalent of <c>UnityEngine.GameObject</c>.
    /// </summary>
    public abstract class TestableGameObject
    {
        private readonly List<TestableComponent> components = new List<TestableComponent>();

        protected TestableGameObject(ITransform transform)
        {
            Transform = transform;
        }

        public ITransform Transform { get; private set; }

        public bool destroyed { get; private set; }
        public abstract bool activeSelf { get; }
        public abstract string name { get; set; }
        public abstract int layer { get; set; }

        public void registerComponent(TestableComponent component)
        {
            components.Add(component);
        }

        public virtual void Destroy()
        {
            if (!destroyed)
            {
                foreach (TestableComponent component in components)
                {
                    component.OnDestroy();
                }
                destroyed = true;
            }
        }

        public void Update()
        {
            if (activeSelf)
            {
                for (int t = 0; t < components.Count; t++)
                {
                    TestableComponent component = components[t];
                    component.OnUpdate();
                }
            }
        }

        public void OnApplicationQuit()
        {
            if (activeSelf)
            {
                for (int t = 0; t < components.Count; t++)
                {
                    TestableComponent component = components[t];
                    component.OnApplicationQuit();
                }
            }
        }

        public IEnumerable<T> getComponents<T>() where T : class
        {
            var list = new List<T>();
            for (int t = 0; t < components.Count; t++)
            {
                TestableComponent component = components[t];
                if (component is T)
                {
                    list.Add(component as T);
                }
            }

            return list;
        }


        public T getComponent<T>() where T : class
        {
            for (int t = 0; t < components.Count; t++)
            {
                TestableComponent component = components[t];
                if (component is T)
                    return component as T;
            }

            return null;
        }


        public void OnCollisionEnter(Collision c)
        {
            for (int t = 0; t < components.Count; t++)
                components[t].OnCollisionEnter(c);
        }

        public abstract void SetActive(bool value);
    }
}