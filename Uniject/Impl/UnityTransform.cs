using UnityEngine;

namespace Uniject.Impl
{
    public class UnityTransform : ITransform
    {
        private ITransform actualParent;

        public UnityTransform(GameObject obj)
        {
            transform = obj.transform;
        }

        private Transform transform { get; set; }

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public Vector3 localScale
        {
            get { return transform.localScale; }
            set { transform.localScale = value; }
        }

        public Quaternion Rotation
        {
            get { return transform.rotation; }
            set { transform.rotation = value; }
        }

        public Vector3 Forward
        {
            get { return transform.forward; }
            set { transform.forward = value; }
        }

        public Vector3 Up
        {
            get { return transform.up; }
            set { transform.up = value; }
        }

        public ITransform Parent
        {
            get { return actualParent; }
            set
            {
                transform.parent = ((UnityTransform) value).transform;
                actualParent = value;
            }
        }

        public void Translate(Vector3 byVector)
        {
            transform.Translate(byVector);
        }

        public void LookAt(Vector3 point)
        {
            transform.LookAt(point);
        }

        public Vector3 TransformDirection(Vector3 dir)
        {
            return transform.TransformDirection(dir);
        }
    }
}