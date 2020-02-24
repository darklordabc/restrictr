using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepDirection : MonoBehaviour {

    public TextMeshPro tmpDirectionText;
    public Transform tDirectionArrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = new Vector3(-PlayBoundsManager.instance.tHMD.position.x, 0f, -PlayBoundsManager.instance.tHMD.transform.position.z).normalized;
        float angle = Vector3.SignedAngle(new Vector3(PlayBoundsManager.instance.tHMD.forward.x, 0f, PlayBoundsManager.instance.tHMD.forward.z), direction, Vector3.up);
        float dotForward = Vector3.Dot(PlayBoundsManager.instance.tHMD.forward, direction);
        float dotRight = Vector3.Dot(PlayBoundsManager.instance.tHMD.forward, Vector3.Cross(Vector3.up, direction));

        tDirectionArrow.localEulerAngles = Vector3.back * angle;

        if (Mathf.Abs(dotRight) > Mathf.Abs(dotForward)) { //X
            if (dotRight > 0f) {
                tmpDirectionText.text = PlayBounds_Prefs_Handler.instance.GetTextStepLeft();
            } else {
                tmpDirectionText.text = PlayBounds_Prefs_Handler.instance.GetTextStepRight();
            }
        } else { //Z
            if (dotForward < 0f) {
                tmpDirectionText.text = PlayBounds_Prefs_Handler.instance.GetTextStepBack();
            } else {
                tmpDirectionText.text = PlayBounds_Prefs_Handler.instance.GetTextStepForward();
            }
        }
    }
}
