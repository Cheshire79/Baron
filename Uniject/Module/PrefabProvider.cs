using System;
using Ninject.Activation;
using Uniject;

/// <summary>
///     Denotes a parameter should be loaded as a Resource from a specified path.
///     Suitable for prefabs, audio clips etc.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public class Resource : Attribute
{
    public Resource(string path)
    {
        Path = path;
    }

    public string Path { get; private set; }
}

/// <summary>
///     A <c>Provider</c> that instantiates Unity prefabs wrapped as <c>TestableGameObject</c>.
/// </summary>
public class PrefabProvider : Provider<TestableGameObject>
{
    private readonly IResourceLoader _loader;

    public PrefabProvider(IResourceLoader loader)
    {
        _loader = loader;
    }

    protected override TestableGameObject CreateInstance(IContext context)
    {
        var attrib = (Resource) context.Request.Target.GetCustomAttributes(typeof (Resource), false)[0];
        return _loader.instantiate(attrib.Path);
    }
}