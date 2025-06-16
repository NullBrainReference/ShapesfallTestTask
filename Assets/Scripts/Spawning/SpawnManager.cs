using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class SpawnManager : MonoBehaviour
{
    //Can be replaced with multiple spawners for better spread control
    [SerializeField]
    private Spawner _spawner;

    [SerializeField]
    private List<FigureBase> _figures;

    [Inject]
    private ObjectsPool _objectsPool;

    public void StartSpawning(List<FigureBase> figures, bool destroyUnused = false)
    {
        _objectsPool.ReleaseFigures();

        _figures = figures;

        StartCoroutine(SpawnCoroutine(destroyUnused));
    }

    private IEnumerator SpawnCoroutine(bool destroyUnused = false)
    {
        foreach (var figure in _figures)
        {
            _spawner.Spawn(figure);

            yield return new WaitForSeconds(0.25f);
        }

        if (destroyUnused)
            _objectsPool.DestroyUnused();
    }
}
