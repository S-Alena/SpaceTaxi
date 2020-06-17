using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverHandler : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public GameObject normal;

    public GameObject mouseOver;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        normal.SetActive(false);
        mouseOver.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        normal.SetActive(true);
        mouseOver.SetActive(false);
    }
}
