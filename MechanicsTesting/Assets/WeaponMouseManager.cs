using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponMouseManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"I am hovering over {this.name}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print($"I am no longer hovering over {this.name}");
    }
}
