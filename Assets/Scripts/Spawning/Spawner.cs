using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [Inject]
    private ObjectsPool _objectsPool;
    [Inject]
    private IconsCollection _iconsCollection;
    [Inject]
    private ShapesCollection _shapesCollection;

    [Inject]
    private DiContainer _diContainer;

    [SerializeField]
    private float _offsetLeft;
    [SerializeField]
    private float _offsetRight;

    [SerializeField]
    private Transform _container;

    public void Spawn(FigureBase figure)
    {
        var controller = _objectsPool.GetFigureController(figure);

        //underdesign :(
        if (figure is Figure oneWithSprite)
            oneWithSprite.Sprite = _iconsCollection.GetSprite(oneWithSprite.IconType);
        //figure.Sprite = _iconsCollection.GetSprite(figure.IconType);

        if (controller == null) 
        {
            controller = _diContainer.InstantiatePrefabForComponent<FigureController>(
                _shapesCollection.ShapeVariants[figure.ShapeType],
                new Vector3(Random.Range(_offsetLeft, _offsetRight),transform.position.y, 0),
                transform.rotation,
                _container);

            controller.SetModel(figure);
            _objectsPool.Add(controller);
        }
        else
        {
            controller.transform.position =
                new Vector3(Random.Range(_offsetLeft, _offsetRight), transform.position.y, 0);
            controller.transform.rotation = transform.rotation;

            controller.SetModel(figure);
            controller.Simulate(true);
            controller.gameObject.SetActive(true);
        }
    }
}
