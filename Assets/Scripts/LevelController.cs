using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Zenject;

public class LevelController : MonoBehaviour
{
    [SerializeField] //Only observation
    private List<FigureBase> _figures;
    [Space]
    [SerializeField]
    private ShapeType[] _shapes;
    [SerializeField]
    private Color[] _colors;
    [SerializeField]
    private FigureIconType[] _icons;
    [Space]
    [SerializeField]
    private ShapeType[] _uniqueShapes;
    [Space]
    [SerializeField]
    private int _maxForVariant;
    [SerializeField]
    private int _figuresCount;

    private List<FigureBase> _uniqueVariants;

    [Inject]
    private SpawnManager _spawnManager;

    public List<FigureBase> Figures => _figures;
    public int MaxForVariantCount => _maxForVariant * 3;

    private void Awake()
    {
        _uniqueVariants = new List<FigureBase>();
    }

    private void Start()
    {
        MakeUniqueFigures();
        CreateFiguresMix(_figuresCount, _maxForVariant);
        _spawnManager.StartSpawning(_figures);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Recreate()
    {
        if (Figures.Count <= 0)
            return;

        int count = _figures.Count;

        CreateFiguresMix(count, _maxForVariant);

        _spawnManager.StartSpawning(_figures, true);

        EventBus.Instance.Invoke(EventType.Recreate);
    }

    private int AddUniqueFigures(int count, int maxForVariant)
    {
        int addedCount = 0; 
        int figureCount = 3;
        if (count > 3)
            figureCount = Random.Range(1, maxForVariant + 1) * 3;

        var unspawnedVariants = new List<ShapeType>();
        foreach (var variant in _uniqueShapes)
            unspawnedVariants.Add(variant);

        for (int i = 0; i < count; i += figureCount)
        {
            if (unspawnedVariants.Count <= 0)
            {
                return addedCount;
            }

            int variantIndex = Random.Range(0, unspawnedVariants.Count);
            var shape = unspawnedVariants[variantIndex];

            unspawnedVariants.RemoveAt(variantIndex);

            for (int j = 0; j < figureCount; j++)
            {
                _figures.Add(FigureBase.GetFigureByShape(shape));
            }

            addedCount += figureCount;

            figureCount = Random.Range(1, maxForVariant + 1) * 3;
        }

        return addedCount;
    }

    public void CreateFiguresMix(int count, int maxForVariant)
    {
        _figures = new List<FigureBase>();

        int figureCount = Random.Range(1, maxForVariant + 1) * 3;

        int unitques = GetUniquesCount(count, 6);
        int uniquesAdded = AddUniqueFigures(unitques, maxForVariant);
        Debug.Log($"added {uniquesAdded} unique figures");

        count -= uniquesAdded;

        var unspawnedVariants = new List<FigureBase>();
        foreach (var variant in _uniqueVariants)
            unspawnedVariants.Add(variant);

        for (int i = 0; i < count; i += figureCount)
        {
            int variantIndex = Random.Range(0, unspawnedVariants.Count);
            var figure = unspawnedVariants[variantIndex];

            unspawnedVariants.RemoveAt(variantIndex);

            for (int j = 0; j < figureCount; j++)
            {
                _figures.Add(figure);
            }

            figureCount = Random.Range(1, maxForVariant + 1) * 3;
        }

        MixFigures();
    }

    private void MixFigures()
    {
        for (int i = _figures.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (_figures[i], _figures[j]) = (_figures[j], _figures[i]);
        }
    }

    private void MakeUniqueFigures()
    {
        foreach (var shape in _shapes)
        {
            foreach (var color in _colors)
            {
                foreach (var icon in _icons)
                {
                    _uniqueVariants.Add(new Figure(shape, icon, color));
                }
            }
        }
    }


    //TODO: Make better proportion geter
    private int GetUniquesCount(int total, int divideBy)
    {
        int result = total / 3;

        if (result <= divideBy - 1)
        {
            return Random.Range(0, 2) * 3;
        }

        if (total % 3 != 0)
            return Random.Range(1, 3) * 3;

        return total / divideBy;
    }
}
