using UnityEngine;

namespace Uniject.Impl
{
    public class UnityNavmeshAgent : INavmeshAgent
    {
        private readonly GameObject obj;
        private NavMeshAgent agent;

        public UnityNavmeshAgent(GameObject obj)
        {
            this.obj = obj;
            agent = obj.GetComponent<NavMeshAgent>();
        }

        public void Stop()
        {
            agent.Stop();
        }

        public void onPlacedOnNavmesh()
        {
            if (null == agent)
            {
                agent = obj.AddComponent<NavMeshAgent>();
            }
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            agent.autoRepath = false;
        }

        public void setDestination(Vector3 target)
        {
            agent.SetDestination(target);
        }

        public void setSpeedMultiplier(float multiplier)
        {
            agent.speed = multiplier;
        }

        public ObstacleAvoidanceType obstacleAvoidanceType
        {
            get { return agent.obstacleAvoidanceType; }
            set { agent.obstacleAvoidanceType = value; }
        }

        public float BaseOffset
        {
            get { return agent.baseOffset; }
            set { agent.baseOffset = value; }
        }

        public bool Enabled
        {
            get { return agent.enabled; }
            set { agent.enabled = value; }
        }

        public float radius
        {
            get { return agent.radius; }
            set { agent.radius = value; }
        }
    }
}