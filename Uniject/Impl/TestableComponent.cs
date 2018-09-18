namespace Uniject
{
    public class TestableComponent
    {
        private readonly TestableGameObject obj;

        public TestableComponent(TestableGameObject obj)
        {
            enabled = true;
            this.obj = obj;
            obj.registerComponent(this);
        }

        public bool enabled { get; set; }

        public TestableGameObject Obj
        {
            get { return obj; }
        }

        public void OnUpdate()
        {
            if (enabled)
            {
                Update();
            }
        }

        public virtual void Update()
        {
        }

        public virtual void OnDestroy()
        {
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
        }

        public virtual void OnApplicationQuit()
        {
        }
    }
}