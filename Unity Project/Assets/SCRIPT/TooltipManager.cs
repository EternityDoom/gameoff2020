using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    //A camera that is used to display what is shown with the tooltip
    public Camera uiCamera;

    //The text object that will show the tooltip
    public Text tooltipText;

    //The background that will be placed underneath the tooltip
    public RectTransform background;

    //The initial offset that will be applied to the tooltip
    public Vector2 offset;

    //Any extra offset that will be applied after the initial offset is applied
    public Vector2 additionnalOffset;

    /// <summary>
    /// Is called to display the tooltip
    /// </summary>
    /// <param name="tooltipString">The text that will display on the tooltip</param>
    /// <param name="offset">The updated position of the tooltip</param>
    public void ShowTooltip(string tooltipString, Vector2 offset){
        additionnalOffset = offset;
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize*3f, tooltipText.preferredHeight + textPaddingSize*2f);
        background.sizeDelta = backgroundSize;

    }

    /// <summary>
    /// Updates last,to draw the tooltip to a specific location
    /// </summary>
    private void LateUpdate() {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        localPoint += offset + additionnalOffset;
        transform.localPosition = localPoint;
    }

    /// <summary>
    /// Is called when the tooltip needs to be hidden from display
    /// </summary>
    public void HideTooltip(){
        gameObject.SetActive(false);
    }
}
