using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;
[ExecuteInEditMode]
[AddComponentMenu("Event/RightButtonEvent")]
public class RightButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [System.Serializable] public class RightButton : UnityEvent { }
    public RightButton onRightDown;
    public RightButton onRightUp;
    private bool isOver = false;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            onRightDown.Invoke();
        }
        if (Input.GetMouseButtonUp(1))
        {
            onRightUp.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }
}