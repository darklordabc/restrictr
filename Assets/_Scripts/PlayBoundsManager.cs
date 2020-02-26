using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Valve.VR;
using Steamworks;
using Microsoft.Win32;

public class PlayBoundsManager : MonoBehaviour {

    public static PlayBoundsManager instance;

    public Transform tHMD;
    public Unity_Overlay overlayFloor;
    public Unity_Overlay overlayText;
    public Unity_Overlay overlayBounds;
    public SpriteRenderer spriteBounds;
    public TextMeshPro textReturn;
    //public float boundsSize = 2;
    //public float timeFadeIn = 0.2f;
    //public float timeFadeOut = 0.5f;

    private float animTimer;
    private float animTimerBoundsBox;
    private float lastAnimTimer;
    private PlayBounds_Prefs_Handler prefs;
    private bool bInBoundsLookingDown = false;
    private bool bVisionIsBlocked;

    private int runningAppId = -1;
    private string runningAppIdName;
    private float runningAppTime;
    private int runningAppVisionBlockTimes;
    private RegistryKey registryKeyRoot;

    private void Awake() {
        instance = this;
        registryKeyRoot = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Valve").OpenSubKey("Steam").OpenSubKey("Apps");
    }

    // Use this for initialization
    void Start () {
        prefs = GetComponent<PlayBounds_Prefs_Handler>();

    }
	
	// Update is called once per frame
	void Update () {
        if (System.Diagnostics.Process.GetProcessesByName("vrserver").Length == 0) {
            //Application.Quit();
            //Debug.Log("application quit");
        }

        float halfBounds = prefs.GetBoundsSize() / 2f;
		if ((Mathf.Abs(tHMD.position.z) > halfBounds || Mathf.Abs(tHMD.position.x) > halfBounds)) {
            if (!prefs.GetDoNotActivateIfFacingDown() || !bInBoundsLookingDown) {
                //Out of bounds
                if (!bVisionIsBlocked)
                    runningAppVisionBlockTimes++;

                animTimer = Mathf.Clamp01(animTimer + Time.deltaTime / prefs.GetTimeFadeIn());
                animTimerBoundsBox = Mathf.Clamp01(animTimerBoundsBox + Time.deltaTime / prefs.GetTimeFadeIn());
                bVisionIsBlocked = true;
            } else {
                animTimer = Mathf.Clamp01(animTimer - Time.deltaTime / prefs.GetTimeFadeOut());
                animTimerBoundsBox = Mathf.Max(animTimerBoundsBox + Time.deltaTime / prefs.GetTimeFadeIn(), 0f);
                bVisionIsBlocked = false;
            }
        } else {
            //Inside bounds
            animTimer = Mathf.Clamp01(animTimer - Time.deltaTime / prefs.GetTimeFadeOut());
            if (bVisionIsBlocked) {
                animTimerBoundsBox = 5f / prefs.GetTimeFadeOut();
            }
            animTimerBoundsBox = Mathf.Max(animTimerBoundsBox - Time.deltaTime / prefs.GetTimeFadeOut(), 0f);

            bInBoundsLookingDown = Vector3.Angle(tHMD.forward, Vector3.down) < 45f;
            bVisionIsBlocked = false;
        }

        try
        {
            if (!prefs.IsEnabled()
                || (OpenVR.Overlay.IsDashboardVisible()
                && !prefs.IsEnabledInDashboard()))
                animTimer = 0f;
        } catch (System.Exception e)
        {
            //OpenVR is not running
            animTimer = 0f;
        }




        overlayFloor.widthInMeters = prefs.GetBoundsSize();
        switch (prefs.GetBoundsBoxVisibility()) {
            case PlayBoundsPrefs.PlayBoundPrefsSettings.BoundsBoxVisibility.WhenVisionIsBlocked:
                overlayFloor.opacity = animTimer;
                break;
            case PlayBoundsPrefs.PlayBoundPrefsSettings.BoundsBoxVisibility.WhenVisionIsBlocked5Seconds:
                overlayFloor.opacity = Mathf.Clamp01(animTimerBoundsBox);
                break;
            case PlayBoundsPrefs.PlayBoundPrefsSettings.BoundsBoxVisibility.AlwaysVisible:
                overlayFloor.opacity = 1f;
                break;
        }
        overlayText.opacity = animTimer;
        overlayBounds.opacity = animTimer;
        spriteBounds.color = new Color(prefs.GetColorRed() / 255f, prefs.GetColorGreen() / 255f, prefs.GetColorBlue() / 255f);
        textReturn.text = prefs.GetTextReturn();

        //Disable/Enable overlays based on opacity
        overlayBounds.enabled = overlayBounds.opacity > 0f;
        overlayFloor.enabled = overlayFloor.opacity > 0f;
        overlayText.enabled = overlayText.opacity > 0f;


        //Audio effect
        if (prefs.MustPlayAlertSound() && lastAnimTimer == 0f && animTimer > 0f) {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Application.dataPath + "\\..\\Audio\\alert.wav");
            player.Play();
        }
        lastAnimTimer = animTimer;

        UpdateRunningApp();
    }

    public void UpdateRunningApp() {
        if (runningAppId == -1) { //None running
            runningAppId = FindRunningGame();
            runningAppIdName = GetGameName(runningAppId);

            if (runningAppId != -1) {
                Debug.Log("Running "+runningAppIdName+" ("+runningAppId.ToString()+")");
            }

        } else { //App already running, validate that it is still running
            if (!IsAppIdRunning(runningAppId.ToString())) {
                runningAppId = -1;
                runningAppIdName = null;
            }
        }

        if (runningAppId != -1) {
            runningAppTime += Time.deltaTime;
        } else {
            runningAppTime = 0f;
            runningAppVisionBlockTimes = 0;
        }
    }

    private int FindRunningGame() {
        string appId = null;

        //Search for Installed 
        string[] appIds = registryKeyRoot.GetSubKeyNames();
        int index = 0;
        bool bFoundRunning = false;
        while (index < appIds.Length && !bFoundRunning) {
            try {
                if (!appIds[index].Equals("689580") //Excluding TurnSignal
                    || !appIds[index].Equals("1170130") //Excluding Restrictr
                    ) { 
                    bFoundRunning = IsAppIdRunning(appIds[index]);
                }
            } catch (System.Exception e) {
            }
            index++;
        }

        if (bFoundRunning)
            appId = appIds[index-1];

        int resultAppId;
        if (int.TryParse(appId, out resultAppId)) {
            return resultAppId;
        } else {
            return -1;
        }
    }

    private bool IsAppIdRunning(string appId) {
        return ((int)(registryKeyRoot.OpenSubKey(appId).GetValue("Running")) == 1);
    }

    public string GetGameName(int appId) {
        if (appId == -1) return null;

        string appName;

        try
        {
            if (SteamAppList.GetAppName(new AppId_t((uint)appId), out appName, 256) == -1 && (appName == null || appName == ""))
            {
                appName = "Unknown (App Id: " + appId.ToString() + ")";
            };

        } catch (System.Exception e)
        {
            appName = "Unknown (App Id: "+appId.ToString()+")";
        }
        return appName;
    }

    public int GetRunningAppId() {
        return runningAppId;
    }
    public string GetRunningAppIdName() {
        return runningAppIdName;
    }
    public float GetRunningAppTime() {
        return runningAppTime;
    }
    public float GetRunningAppVisionBlockActivatedTimes() {
        return runningAppVisionBlockTimes;
    }

    
    public bool IsRunningAnApp() {
        return runningAppId != -1;
    }
}
