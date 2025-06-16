using UnityEngine;

[System.Serializable]
public class FigureBase
{
    [SerializeField]
    protected ShapeType shapeType;

    public virtual string Key { get => shapeType.ToString(); }
    public ShapeType ShapeType { get => shapeType; }

    public static FigureBase GetFigureByShape(ShapeType shape)
    {
        switch (shape)
        {
            case ShapeType.Anvil:
                return new Anvil();
            case ShapeType.Ice:
                return new Ice();
        }

        return null;
    }
}
