using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public BufferLocalizer bufferLocalizer;
    private bool bShowTooltip;

	// Use this for initialization
	void Update () {
        if (bShowTooltip) {
            UITooltip.instance.ShowTooltip(bufferLocalizer.localizedValue, Input.mousePosition);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        bShowTooltip = true;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData) {
        bShowTooltip = false;
    }
}
