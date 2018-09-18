using UnityEngine;

namespace Uniject
{
    public interface INavmeshAgent
    {
        bool Enabled { get; set; }
        float radius { get; set; }
        float BaseOffset { get; set; }
        ObstacleAvoidanceType obstacleAvoidanceType { get; set; }
        void setDestination(Vector3 dest);
        void Stop();
        void setSpeedMultiplier(float multiplier);
        void onPlacedOnNavmesh();
    }
}