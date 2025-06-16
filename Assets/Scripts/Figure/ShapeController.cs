using UnityEngine;

public class ShapeController : MonoBehaviour, IFigureAttribute<ShapeController>
{
    private Collider2D _collider;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private ShapeType _shapeType;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void EnableCollider(bool enable)
    {
        _collider.enabled = enable;
    }

    public bool Match(ShapeController value)
    {
        return _shapeType == value._shapeType;
    }
}
