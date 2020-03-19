using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class UIPlayAreaRectangle : MonoBehaviour
{
    public RectTransform rectArea;
    public RectTransform rectBounds;
    public RectTransform camera;
    public Text textAreaX;
    public Text textAreaZ;
    public Text textBoundsX;
    public Text textBoundsZ;

    private static float sizeX = 0f;
    private static float sizeZ = 0f;


    private const float SCALE_JUMP_THRESHOLD = 5f;


    // Update is called once per frame
    void Update()
    {
        //setting the size of play area rectangle
        if (GetPlayAreaSizes(ref sizeX, ref sizeZ))
        {
            float playScale = Mathf.Max(sizeX, sizeZ) > SCALE_JUMP_THRESHOLD ? 28f : 56f;
            textAreaX.text = sizeX.ToString("F1");
            textAreaZ.text = sizeZ.ToString("F1");
            rectArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp(sizeX * playScale, 0f, 280f));
            rectArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Clamp(sizeZ * playScale, 0f, 280f));
        }
        else
        {
            textAreaX.text = "";
            textAreaZ.text = "";
            rectArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0f);
            rectArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
        }
        
        
        //setting the restrictive rectangle bounds
        float x = 1f;
        float z = 1f;

        if (PlayBounds_Prefs_Handler.instance.GetCustomArea())
        {
            Vector2 boundsSize = PlayBounds_Prefs_Handler.instance.GetBoundsCustomSize();
            x = boundsSize.x;
            z = boundsSize.y;
        }
        else
        {
            float boundsSize = PlayBounds_Prefs_Handler.instance.GetBoundsSize();
            x = z = boundsSize;
        }

        textBoundsX.text = x.ToString("F1");
        textBoundsZ.text = z.ToString("F1");

        float scale = Mathf.Max(x, z) > SCALE_JUMP_THRESHOLD ? 28f : 56f;

        rectBounds.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp(x * scale, 0f, 280f));
        rectBounds.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Clamp(z * scale, 0f, 280f));

        Vector3 localPosition = PlayBoundsManager.instance.tHMD.localPosition;

        camera.localPosition = new Vector3(localPosition.x * 100f, localPosition.z * 100f, 0f);
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
        }
        catch (System.Exception e)
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
        }
        catch
        {
            sizeX = 1f;
            sizeZ = 1f;

            return false;
        }
    }
}