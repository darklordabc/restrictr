using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIColorHue : MonoBehaviour {

    private Button button;

    public void Start() {
        button = GetComponent<Button>();
    }

    public void Update() {
        
    }

    public void SetMousePosition(Vector2 position) {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), position, Camera.main, out localPoint)) {
            float angle = Vector2.SignedAngle(Vector2.up, localPoint);
            transform.localEulerAngles = new Vector3(0f, 0f, angle);
        }
    }

    public float GetValue() {
        return Mathf.Clamp01(1f-transform.localEulerAngles.z/360f);
    }

    public void SetValue(float value) {
        transform.localEulerAngles = new Vector3(0f, 0f, (1f-value) * 360f);
    }
}
