using UnityEngine;
using Zenject;

public class ObjectsPoolInstaller : MonoInstaller
{
    [SerializeField]
    private ObjectsPool _objectsPool;

    public override void InstallBindings()
    {
        Container.BindInstance(_objectsPool).AsSingle();
    }
}