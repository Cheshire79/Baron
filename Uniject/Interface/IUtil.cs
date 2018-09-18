using System;
using UnityEngine;

namespace Uniject
{
    public interface IUtil
    {
        RuntimePlatform Platform { get; }
        string persistentDataPath { get; }
        DateTime currentTime { get; }
        T[] getAnyComponentsOfType<T>() where T : class;
        string loadedLevelName();

        string OperatingSystem { get; }
        string DeviceName { get; }
        DeviceType DeviceType { get; }
        string DeviceModel { get; }
        string DeviceId { get; }
    }
}