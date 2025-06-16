using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class ShapesRowController : MonoBehaviour
{
    //[SerializeField]
    //private Transform[] _anchors;

    [SerializeField]
    private ShapeRowSlot[] _slots;

    [Inject]
    private LevelController _levelController;

    private void Awake()
    {
        //_slots = new ShapeRowSlot[_anchors.Length]; //Was non monobehavior, mb yet needs model?
        //for (int i = 0; i < _slots.Length; i++)
        //    _slots[i] = new ShapeRowSlot(_anchors[i]);
    }

    public ShapeRowSlot GetFreeSlot()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsFree)
                return slot;
        }

        return null;
    }

    public void Occupy(FigureController figureController)
    {
        var slot = GetFreeSlot();

        if (slot == null)
            return;

        slot.Occupy(figureController);

        figureController.MoveTo(slot.Anchor);
    }

    private bool IsFilled()
    {
        foreach (ShapeRowSlot slot in _slots)
        {
            if (slot.IsFree)
                return false;
        }

        return true;
    }

    public void MatchFigures()
    {
        var matches = new Dictionary<string, List<ShapeRowSlot>>();

        foreach (var slot in _slots)
        {
            if (slot.IsFree)
                break;

            string key = slot.Figure.Key;

            if (!matches.ContainsKey(key))
                matches.Add(key, new List<ShapeRowSlot>());

            matches[key].Add(slot);
        }

        foreach (var slots in matches.Values)
        {
            if (slots.Count < 3)
                continue;

            foreach (var slot in slots)
            {
                _levelController.Figures.Remove(slot.Figure);

                EventBus.Instance.Invoke(EventType.Destroyed);

                slot.FigureController.OnRemove();
                slot.FreeOnMatch();
            }
        }

        ShiftLeft();

        if (_levelController.Figures.Count <= 0)
            EventBus.Instance.Invoke(EventType.Victory);
        else if (IsFilled())
            EventBus.Instance.Invoke(EventType.Defeat);
    }

    private void ShiftLeft()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if(_slots[i].IsFree)
            {
                int j = i + 1;
                while (j < _slots.Length && _slots[j].IsFree)
                {
                    j++;
                }

                if (j < _slots.Length)
                {
                    _slots[i].Occupy(_slots[j].FigureController);
                    _slots[j].Free(true);
                    _slots[i].FigureController.MoveTo(_slots[i].Anchor);
                }
            }
        }
    }
}
