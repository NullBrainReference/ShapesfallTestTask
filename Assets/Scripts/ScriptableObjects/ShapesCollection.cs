using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ShapesCollection", menuName = "Scriptable Objects/ShapesCollection")]
public class ShapesCollection : ScriptableObject
{
    [SerializeField] private FigureController[] _prefabs;
    [SerializeField] private FigureController[] _uniquePrefabs;

    public Dictionary<ShapeType, FigureController> ShapeVariants;

    private void OnEnable()
    {
        Initialize();

        Debug.Log("ShapesCollection Enabled");
    }

    private void Initialize()
    {
        ShapeVariants = new Dictionary<ShapeType, FigureController>();
        foreach (var prefab in _prefabs)
        {
            ShapeVariants[prefab.Figure.ShapeType] = prefab;
        }
        foreach (var uniquePrefab in _uniquePrefabs)
        {
            ShapeVariants[uniquePrefab.Figure.ShapeType] = uniquePrefab;
        }
    }

    public void Refresh()
    {
        Initialize();

        Debug.Log("ShapesCollection Refreshed");
    }
}
