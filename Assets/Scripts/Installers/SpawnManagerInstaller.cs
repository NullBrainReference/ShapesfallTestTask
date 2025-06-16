using UnityEngine;
using Zenject;

public class SpawnManagerInstaller : MonoInstaller
{
    [SerializeField]
    private SpawnManager _spawnManager;

    public override void InstallBindings()
    {
        Container.BindInstance(_spawnManager).AsSingle();
    }
}