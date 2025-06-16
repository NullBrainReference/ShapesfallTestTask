using UnityEngine;
using TMPro;

public class DefeatPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _winText;
    [SerializeField]
    private TextMeshProUGUI _loseText;

    [SerializeField]
    private GameObject _container;

    public void Start()
    {
        EventBus.Instance.Subscribe(EventType.Victory, new LocalEvent(() => Show(true)));
        EventBus.Instance.Subscribe(EventType.Defeat, new LocalEvent(() => Show(false)));
    }

    public void Show(bool victory)
    {
        _container.SetActive(true);

        _winText.gameObject.SetActive(victory);
        _loseText.gameObject.SetActive(!victory);
    }
}
