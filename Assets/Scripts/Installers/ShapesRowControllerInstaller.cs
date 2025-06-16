using UnityEngine;
using Zenject;

public class ShapesRowControllerInstaller : MonoInstaller
{
    [SerializeField]
    private ShapesRowController _rowController;

    public override void InstallBindings()
    {
        Container.BindInstance(_rowController).AsSingle();
    }
}