using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick
{
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;
    [SerializeField] private RectTransform joystickZone;

    protected override void Start()
    {
        MoveThreshold = moveThreshold;
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);

        // Ограничиваем перемещение джойстика в пределах зоны
        Vector2 joystickPosition = background.anchoredPosition;
        joystickPosition.x = Mathf.Clamp(joystickPosition.x, -joystickZone.rect.width / 2, joystickZone.rect.width / 2);
        joystickPosition.y = Mathf.Clamp(joystickPosition.y, -joystickZone.rect.height / 2, joystickZone.rect.height / 2);
        background.anchoredPosition = joystickPosition;
    }
}
