using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ShapesCollectionInstaller", menuName = "Installers/ShapesCollectionInstaller")]
public class ShapesCollectionInstaller : ScriptableObjectInstaller<ShapesCollectionInstaller>
{
    [SerializeField]
    private ShapesCollection _shapesCollection;

    public override void InstallBindings()
    {
        Container.BindInstance(_shapesCollection).AsSingle();
    }
}