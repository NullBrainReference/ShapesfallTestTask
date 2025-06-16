using UnityEngine;
using DG.Tweening;

public class ShapeRowSlot : MonoBehaviour
{
    private FigureController _figureController;

    private SpriteRenderer _backgroung;

    private float _defaultAlfa;

    public bool IsFree => _figureController == null;
    public FigureBase Figure => _figureController != null ? _figureController.Figure : null;
    public FigureController FigureController => _figureController;
    public Transform Anchor => transform;

    private void Awake()
    {
        _backgroung = GetComponent<SpriteRenderer>();

        _defaultAlfa = _backgroung.color.a;
    }

    private void Start()
    {
        EventBus.Instance.Subscribe(EventType.Recreate,
            new LocalEvent(() => Free(true)));
    }

    public void Occupy(FigureController figureController)
    {
        _figureController = figureController;

        _backgroung.DOFade(0, 0.1f);
    }

    public void Free(bool showBackground = false)
    {
        _figureController = null;

        if (showBackground)
            _backgroung.DOFade(_defaultAlfa, 0.1f);
    }

    public void FreeOnMatch()
    {
        _backgroung.DOFade(_defaultAlfa, 0.1f);

        _figureController.gameObject.SetActive(false);

        Free();
    }
}
