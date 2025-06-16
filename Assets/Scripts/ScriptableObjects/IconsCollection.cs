using UnityEngine;

[CreateAssetMenu(fileName = "IconsCollection", menuName = "Scriptable Objects/IconsCollection")]
public class IconsCollection : ScriptableObject
{
    [SerializeField]
    private Sprite _pigSprite;
    [SerializeField]
    private Sprite _dogSprite;
    [SerializeField]
    private Sprite _foxSprite;
    [SerializeField]
    private Sprite _sheepSprite;
    [SerializeField]
    private Sprite _bearSprite;

    public Sprite GetSprite(FigureIconType iconType)
    {
        switch (iconType)
        {
            case FigureIconType.Pig:
                return _pigSprite;

            case FigureIconType.Dog:
                return _dogSprite;

            case FigureIconType.Fox:
                return _foxSprite;

            case FigureIconType.Sheep:
                return _sheepSprite;

            case FigureIconType.Bear:
                return _bearSprite;
        }

        return null;
    }
}
