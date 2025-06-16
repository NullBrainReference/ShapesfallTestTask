using UnityEngine;

[System.Serializable]
public class Figure : FigureBase
{
    [SerializeField] private FigureIconType _iconType;
    [SerializeField] private Color _color;

    public Figure(ShapeType shapeType, FigureIconType iconType, Color color)
    {
        this.shapeType = shapeType;
        _iconType = iconType;
        _color = color;
    }

    public FigureIconType IconType => _iconType;
    public Color Color => _color;

    public Sprite Sprite { get; set; }

    public override string Key => ShapeType.ToString() + IconType.ToString() + Color.ToString();

    //public Figure GetFigureByShape(ShapeType shape)
    //{
    //
    //}
}
