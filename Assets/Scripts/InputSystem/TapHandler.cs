using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TapHandler : MonoBehaviour
{
    private InputAction _tapAction;

    void Awake()
    {
        _tapAction = new InputAction(binding: "<Pointer>/press");
        _tapAction.AddBinding("<Mouse>/leftButton");
        _tapAction.performed += ctx => OnTap();
        _tapAction.Enable();
    }

    private void OnTap()
    {
        Debug.Log("OnTap() performed!");

        Vector2 screenPos = Pointer.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log($"Hit: {hit.collider.gameObject.name}");
            var clickable = hit.collider.GetComponentInParent<IClickable>();

            if (clickable != null)
            {
                Debug.Log("IClickable found!");
                clickable.OnClick();
            }
            else
            {
                Debug.Log("IClickable not found!");
            }
        }
    }
}
