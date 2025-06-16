using UnityEngine;

[System.Serializable]
public class UnfreezEvent : ILocalEvent
{
    private IceFigureController _iceFigure;
    [SerializeField] //Debugging
    private int _destructionsNeeds;

    public UnfreezEvent(IceFigureController iceFigure, int target)
    {
        _destructionsNeeds = target;
        _iceFigure = iceFigure;
    }

    public void Invoke()
    {
        _destructionsNeeds--;

        if (_destructionsNeeds > 0)
            return;

        _iceFigure.Unfreeze();

        EventBus.Instance.Unsub(EventType.Destroyed, this);
    }

    public void Unsub()
    {
        EventBus.Instance.Unsub(EventType.Destroyed, this);
    }
}
