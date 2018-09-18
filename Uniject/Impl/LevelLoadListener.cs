using UnityEngine;

public class LevelLoadListener : MonoBehaviour
{
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnLevelWasLoaded()
    {
        UnityInjector.levelScope = new object();
    }
}