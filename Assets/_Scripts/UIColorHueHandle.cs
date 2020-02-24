using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIColorHueHandle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private Button button;
    private UIColorHue uiColorHue;

    public void Start() {
        button = GetComponent<Button>();
        uiColorHue = transform.GetComponentInParent<UIColorHue>();
    }

    public void Update() {
        //
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) {
        uiColorHue.SetMousePosition(eventData.position);
    }

    void IDragHandler.OnDrag(PointerEventData eventData) {
        uiColorHue.SetMousePosition(eventData.position);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) {
        uiColorHue.SetMousePosition(eventData.position);
    }
}
