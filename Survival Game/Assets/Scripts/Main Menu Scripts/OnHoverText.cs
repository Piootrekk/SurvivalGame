using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        gameObject.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
        gameObject.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
    }
}
