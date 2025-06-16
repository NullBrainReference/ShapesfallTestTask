using UnityEngine;
using System.Collections.Generic;

public class ObjectsPool : MonoBehaviour
{
    private Dictionary<string, List<FigureController>> _figures;

    private void Awake()
    {
        _figures = new Dictionary<string, List<FigureController>>();
    }

    public void Add(FigureController figureController)
    {
        string key = figureController.Figure.Key;

        if (_figures.ContainsKey(key) == false)
            _figures.Add(key, new List<FigureController>());

        _figures[key].Add(figureController);

    }

    public void ReleaseFigures()
    {
        foreach (var figures in _figures.Values)
        {
            foreach (var figure in figures)
            {
                figure.OnRemove();
                figure.gameObject.SetActive(false);
            }
        }
    }

    public void DestroyUnused()
    {
        foreach (var figures in _figures.Values)
        {
            var toRemove = figures.FindAll(x => x.gameObject.activeInHierarchy == false);

            foreach (var figure in toRemove)
            {
                figure.OnRemove();

                figures.Remove(figure);

                Destroy(figure.gameObject);
            }
        }
    }

    public FigureController GetFigureController(FigureBase figure)
    {
        string key = figure.Key;

        if (_figures.ContainsKey(key))
            return _figures[key].Find(x => x.gameObject.activeInHierarchy == false);

        return null;
    }
}
