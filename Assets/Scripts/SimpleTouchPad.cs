using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    private Vector2 origin;
    private Vector2 direction;

    void Awake()
    {
        direction = Vector2.zero;
    }
	public void OnPointerDown (PointerEventData data)
    {
        origin = data.position;
    }

    public void OnDrag(PointerEventData data)
    {
        Vector2 currentPosition = data.position;
        Vector2 directionRaw = currentPosition - origin;
        direction = directionRaw.normalized;
    }

    public void OnPointerUp(PointerEventData data)
    {
        direction = Vector2.zero;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }
}
