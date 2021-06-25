using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image imageBackground;
    private Image Controller;
    private Vector2 touchPosition;

    private void Awake()
    {
        imageBackground = GetComponent<Image>();
        Controller = transform.GetChild(0).GetComponent<Image>();
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Touch Begin: " + eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(imageBackground.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {

            touchPosition.x = (touchPosition.x / imageBackground.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / imageBackground.rectTransform.sizeDelta.y);

            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

            touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;

            Controller.rectTransform.anchoredPosition = new Vector2(
                touchPosition.x * imageBackground.rectTransform.sizeDelta.x/2 ,
                touchPosition.y * imageBackground.rectTransform.sizeDelta.y/2 );
            
            Debug.Log("Touch & Drag :" + eventData);
        }
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Touch Ended : " + eventData);
        Controller.rectTransform.anchoredPosition = Vector2.zero;
        touchPosition = Vector2.zero;
    }
    public float Horizontal()
    {
        return touchPosition.x;
    }
    public float Vertical()
    {
        return touchPosition.y;
    }
}
