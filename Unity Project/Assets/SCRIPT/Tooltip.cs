using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
     
    //The string of text that is displayed on a specific tooltip
    [TextArea]
     public string tip;

    //The offset of the tooltip
     public Vector2 offset;

    /// <summary>
    /// Is called when the mouse enters the bounds of an object that is meant to have a tooltip
    /// </summary>
    /// <param name="eventData">The data pertaining to the PointerEventData</param>
     public void OnPointerEnter(PointerEventData eventData)
     {
         GM.I.tooltip.ShowTooltip(tip, offset);
     }

    /// <summary>
    /// Is called when the mouse leaves the bounds of an object that is meant to have a tooltip
    /// </summary>
    /// <param name="eventData">The data pertaining to the PointerEventData</param>
     public void OnPointerExit(PointerEventData eventData)
     {
         GM.I.tooltip.HideTooltip();
     }

    /// <summary>
    /// Is called when the tooltip object is disabled
    /// </summary>
     private void OnDisable() {
         if(GM.I.tooltip != null)
         GM.I.tooltip.HideTooltip();
     }
}
