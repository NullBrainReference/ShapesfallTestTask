using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "IconsCollectionInstaller", menuName = "Installers/IconsCollectionInstaller")]
public class IconsCollectionInstaller : ScriptableObjectInstaller<IconsCollectionInstaller>
{
    [SerializeField]
    private IconsCollection _iconsCollection;

    public override void InstallBindings()
    {
        Container.BindInstance(_iconsCollection).AsSingle();
    }
}