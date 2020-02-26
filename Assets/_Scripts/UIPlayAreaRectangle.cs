using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class UIPlayAreaRectangle : MonoBehaviour {

    public RectTransform rectArea;
    public RectTransform rectBounds;
    public RectTransform camera;
    public Text textAreaX;
    public Text textAreaZ;
    public Text textBoundsX;
    public Text textBoundsZ;

    private static float sizeX = 0f;
    private static float sizeZ = 0f;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

        if (GetPlayAreaSizes(ref sizeX, ref sizeZ)) {
            textAreaX.text = sizeX.ToString("F1");
            textAreaZ.text = sizeZ.ToString("F1");
            rectArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp(sizeX * 100f, 0f, 280f));
            rectArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Clamp(sizeZ * 100f, 0f, 280f));
        } else {
            textAreaX.text = "";
            textAreaZ.text = "";
            rectArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp(0 * 100f, 0f, 280f));
            rectArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Clamp(0 * 100f, 0f, 280f));
        }
        textBoundsX.text = PlayBounds_Prefs_Handler.instance.GetBoundsSize().ToString("F1");
        textBoundsZ.text = PlayBounds_Prefs_Handler.instance.GetBoundsSize().ToString("F1");
        rectBounds.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp(PlayBounds_Prefs_Handler.instance.GetBoundsSize() * 100f, 0f, 280f));
        rectBounds.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Clamp(PlayBounds_Prefs_Handler.instance.GetBoundsSize() * 100f, 0f, 280f));
        camera.localPosition = new Vector3(PlayBoundsManager.instance.tHMD.localPosition.x * 100f, PlayBoundsManager.instance.tHMD.localPosition.z * 100f, 0f);
        camera.localEulerAngles = -Vector3.forward * PlayBoundsManager.instance.tHMD.localEulerAngles.y;
    }

    public static float GetPlayAreaSize()
    {
        float playAreaSize = 10f;

        try
        {
            if (OpenVR.ChaperoneSetup.GetWorkingPlayAreaSize(ref sizeX, ref sizeZ))
            {
                playAreaSize = Mathf.Max(sizeX, sizeZ);
            }

            return playAreaSize;
        } catch (System.Exception e)
        {
            return 10f;
        }
    }

    public static bool GetPlayAreaSizes(ref float sizeX, ref float sizeZ)
    {
        try
        {
            sizeX = 1f;
            sizeZ = 1f;

            OpenVR.ChaperoneSetup.GetWorkingPlayAreaSize(ref sizeX, ref sizeZ);

            return true;
        } catch
        {
            sizeX = 1f;
            sizeZ = 1f;

            return false;
        }
    }

}
