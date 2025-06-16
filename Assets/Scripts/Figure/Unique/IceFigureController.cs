using UnityEngine;
using DG.Tweening;
using Zenject;

public class IceFigureController : FigureController
{
    private bool _isFrozen = true;

    [SerializeField]
    private UnfreezEvent _unfreezEvent;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [Inject]
    private LevelController _levelController;


    public override void SetModel(FigureBase model)
    {
        base.SetModel(model);

        int destroyedTarget = 0;
        if (_levelController.Figures.Count <= _levelController.MaxForVariantCount)
        {
            Unfreeze();
        }
        else if (_levelController.Figures.Count > _levelController.MaxForVariantCount)
        {
            destroyedTarget = _levelController.Figures.Count - _levelController.MaxForVariantCount;

            _unfreezEvent = new UnfreezEvent(this, destroyedTarget);

            EventBus.Instance.Subscribe(EventType.Destroyed, _unfreezEvent);

            Freeze();
        }
    }

    private void Freeze()
    {
        _isFrozen = true;

        _spriteRenderer.DOFade(0.3f, 0.1f);
    }

    public void Unfreeze()
    {
        _isFrozen = false;
        _spriteRenderer.DOFade(1f, 0.1f);
    }

    public override void OnClick()
    {
        if (_isFrozen)
            return;

        base.OnClick();
    }

    public override void OnRemove()
    {
        base.OnRemove();

        _unfreezEvent.Unsub();
    }
}
