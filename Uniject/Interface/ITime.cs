namespace Uniject
{
    public interface ITime
    {
        float DeltaTime { get; }

        float realtimeSinceStartup { get; }
    }
}