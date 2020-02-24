using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIColorHueWheel : MonoBehaviour, IPointerClickHandler {

    public UIColorHue uiColorHue;

    public void Start() {
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
        uiColorHue.SetMousePosition(eventData.position);
    }
}
