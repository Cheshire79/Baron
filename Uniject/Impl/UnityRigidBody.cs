using UnityEngine;

namespace Uniject.Impl
{
    public class UnityRigidBody : IRigidBody
    {
        private readonly Rigidbody body;

        public UnityRigidBody(GameObject obj)
        {
            body = obj.GetComponent<Rigidbody>();
            if (body == null)
            {
                body = obj.AddComponent<Rigidbody>();
            }
        }

        public void AddForce(Vector3 force)
        {
            body.AddForce(force);
        }

        public void AddTorque(Vector3 torque, ForceMode mode)
        {
            body.AddTorque(torque, mode);
        }

        public float drag
        {
            get { return body.drag; }
            set { body.drag = value; }
        }

        public float mass
        {
            get { return body.mass; }
            set { body.mass = value; }
        }

        public bool enabled
        {
            get { return !body.isKinematic; }
            set { body.isKinematic = !value; }
        }

        public Quaternion Rotation
        {
            get { return body.rotation; }
            set { body.rotation = value; }
        }

        public Vector3 Position
        {
            get { return body.position; }
            set { body.position = value; }
        }

        public Vector3 Forward
        {
            get { return body.transform.forward; }
        }

        public RigidbodyConstraints constraints
        {
            get { return body.constraints; }
            set { body.constraints = value; }
        }

        public bool useGravity
        {
            get { return body.useGravity; }
            set { body.useGravity = value; }
        }

        public bool isKinematic
        {
            get { return body.isKinematic; }
            set { body.isKinematic = value; }
        }
    }
}