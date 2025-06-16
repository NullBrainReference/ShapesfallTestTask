using UnityEngine;

public interface IFigureAttribute<T>
{
    public bool Match(T value);
}
