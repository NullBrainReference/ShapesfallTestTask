using UnityEngine;

public class FigureIconController : MonoBehaviour, IFigureAttribute<FigureIconController>
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public bool Match(FigureIconController value)
    {
        return _spriteRenderer.sprite == value._spriteRenderer.sprite;
    }
}
