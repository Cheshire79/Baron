using Uniject.Impl;
using UnityEngine;

public class UnityGameObjectBridge : MonoBehaviour
{
    public UnityGameObject wrapping;

    public void OnDestroy()
    {
        wrapping.Destroy();
    }

    public void Update()
    {
        wrapping.Update();
    }

    public void OnCollisionEnter(Collision c)
    {
        var other = c.gameObject.GetComponent<UnityGameObjectBridge>();
        if (null != other)
        {
            var testableCollision =
                new Uniject.Collision(c.relativeVelocity,
                    other.wrapping.Transform,
                    other.wrapping,
                    c.contacts);
            wrapping.OnCollisionEnter(testableCollision);
        }
    }

    public void OnApplicationQuit()
    {
        wrapping.OnApplicationQuit();
    }
}