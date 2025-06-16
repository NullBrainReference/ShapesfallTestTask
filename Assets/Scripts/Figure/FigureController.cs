using UnityEngine;
using DG.Tweening;
using Zenject;

public class FigureController : MonoBehaviour
{
    [SerializeField]
    private FigureBase _figure;

    [SerializeField]
    private ShapeController _shape;
    [SerializeField]
    private FigureIconController _icon;

    private Rigidbody2D _rb;

    [Inject]
    protected ShapesRowController _rowController;

    public FigureBase Figure => _figure;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnMouseDown()
    {
        _rowController.Occupy(this);

        Debug.Log("Mouse Down of Figure");
    }

    public virtual void SetModel(FigureBase model)
    {
        _figure = model;

        //underdesign :(
        //TODO: Fix it (make ControllerVariant: BaseController or IFigureController?)
        if (model is Figure figure)
        {
            _icon.SetSprite(figure.Sprite);
            _shape.SetColor(figure.Color);
        }
    }

    public void Simulate(bool simulate)
    {
        _rb.simulated = simulate;
        _shape.EnableCollider(simulate);
    }

    public virtual void OnRemove()
    {
        Simulate(false);
    }

    public void MoveTo(Transform anchor)
    {
        _rb.simulated = false;
        _shape.EnableCollider(false);

        float duration = 0.8f;

        var seq = DOTween.Sequence();

        seq.Append(transform.DOMove(anchor.position, duration));
        seq.Join(transform.DORotateQuaternion(anchor.rotation, duration));

        seq.OnComplete(() =>
        {
            _rowController.MatchFigures();
            seq.Kill();
        });
    }
}
