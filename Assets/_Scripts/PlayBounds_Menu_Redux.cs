using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayBounds_Menu_Redux : MonoBehaviour 
{
    public enum ActiveTab { Global, Game };

	public Camera menuRigCamera;

	[Space(10)]
	
	public PlayBounds_Prefs_Handler prefs;

	[Space(10)]

    public Toggle isEnabledToggle;
    public Toggle enabledInDashboardToggle;
    public Toggle startWithVRToggle;
    public Toggle useGlobalSettingsToggle;
    public Toggle playAlertSoundToggle;
    public Toggle doNotActivateIfFacingDownToggle;
    public Toggle hideMainWindowToggle;
    public UIDropdownBoundsBoxVisibility boundsBoxVisibilityDropdown;
    public UIDropdownGames gamesDropdown;
    public Slider boundsSizeSlider;
    public Slider fadeInTimeSlider;
    public Slider fadeOutTimeSlider;
    public Slider textFlashTimeSlider;
    public Slider colorRedSlider;
    public Slider colorGreenSlider;
    public Slider colorBlueSlider;
    public Text textReturnToCenter;
    public Text textStepLeft;
    public Text textStepRight;
    public Text textStepBack;
    public Text textStepForward;

    public UITab tabGlobal;
    public UITab tabGame;
    public Color colorTabNormalBack;
    public Color colorTabNormalText;
    public Color colorTabInactiveBack;
    public Color colorTabInactiveText;

    public ActiveTab activeTab;

    public static PlayBounds_Menu_Redux instance;

    public void Awake() {
        instance = this;
        activeTab = ActiveTab.Global;
        //SelectGlobalTab();
    }

    public void SteamStart()
	{

		if(!prefs.Load())			
			Debug.Log("Bad Settings Load!");

		SetUIValues();
	}

	public void SetUIValues()
    {
        boundsSizeSlider.maxValue = UIPlayAreaRectangle.GetPlayAreaSize();

        if (activeTab == ActiveTab.Global) {
            startWithVRToggle.isOn = prefs.StartWithSteamVR;

            isEnabledToggle.isOn = prefs.globalIsEnabled;
            enabledInDashboardToggle.isOn = prefs.globalEnabledInDashboard;
            playAlertSoundToggle.isOn = prefs.globalPlayAlertSound;
            doNotActivateIfFacingDownToggle.isOn = prefs.globalDoNotActivateIfFacingDown;
            boundsBoxVisibilityDropdown.selectedIndex = (int)(prefs.globalBoundsBoxVisibility);
            boundsSizeSlider.value = prefs.globalBoundsSize;
            fadeInTimeSlider.value = prefs.globalTimeFadeIn;
            fadeOutTimeSlider.value = prefs.globalTimeFadeOut;
            textFlashTimeSlider.value = prefs.globalTimeTextFlash;
            colorRedSlider.value = prefs.globalColorRed;
            colorGreenSlider.value = prefs.globalColorGreen;
            colorBlueSlider.value = prefs.globalColorBlue;
            textReturnToCenter.text = prefs.globalTextReturn;
            textStepLeft.text = prefs.globalTextStepLeft;
            textStepRight.text = prefs.globalTextStepRight;
            textStepBack.text = prefs.globalTextStepBack;
            textStepForward.text = prefs.globalTextStepForward;

        } else {
            useGlobalSettingsToggle.isOn = prefs.gameUseGlobal;

            isEnabledToggle.isOn = prefs.gameIsEnabled;
            enabledInDashboardToggle.isOn = prefs.gameIsEnabledInDashboard;
            playAlertSoundToggle.isOn = prefs.gamePlayAlertSound;
            doNotActivateIfFacingDownToggle.isOn = prefs.gameDoNotActivateIfFacingDown;
            boundsBoxVisibilityDropdown.selectedIndex = (int)(prefs.gameBoundsBoxVisibility);
            boundsSizeSlider.value = prefs.gameBoundsSize;
            fadeInTimeSlider.value = prefs.gameTimeFadeIn;
            fadeOutTimeSlider.value = prefs.gameTimeFadeOut;
            textFlashTimeSlider.value = prefs.gameTimeTextFlash;
            colorRedSlider.value = prefs.gameColorRed;
            colorGreenSlider.value = prefs.gameColorGreen;
            colorBlueSlider.value = prefs.gameColorBlue;
            textReturnToCenter.text = prefs.gameTextReturn;
            textStepLeft.text = prefs.gameTextStepLeft;
            textStepRight.text = prefs.gameTextStepRight;
            textStepBack.text = prefs.gameTextStepBack;
            textStepForward.text = prefs.gameTextStepForward;
        }

    }

    public void Update() {

        /*if (!PlayBoundsManager.instance.IsRunningAnApp()) {
            activeTab = ActiveTab.Global;
            tabGame.text.text = "[No game running]";
        } else {
            tabGame.text.text = PlayBoundsManager.instance.GetRunningAppIdName();
        }*/


        switch (activeTab) {
            case ActiveTab.Global:
                startWithVRToggle.gameObject.SetActive(true);
                useGlobalSettingsToggle.gameObject.SetActive(false);
                gamesDropdown.gameObject.SetActive(false);
                break;

            case ActiveTab.Game:
                startWithVRToggle.gameObject.SetActive(false);
                useGlobalSettingsToggle.gameObject.SetActive(true);
                gamesDropdown.gameObject.SetActive(true);
                break;
        }
    }

    public void ResetSettings()
	{
        if (activeTab== ActiveTab.Global) {
            prefs.ResetGlobal();
        } else {
            prefs.ResetGame();
        }
		SetUIValues();
    }

    public void SelectGlobalTab() {
        activeTab = ActiveTab.Global;

        tabGlobal.text.color = colorTabNormalText;
        tabGlobal.image.color = colorTabNormalBack;

        tabGame.text.color = colorTabInactiveText;
        tabGame.image.color = colorTabInactiveBack;

        SetUIValues();
    }

    public void SelectGameTab() {
        //if (!PlayBoundsManager.instance.IsRunningAnApp()) return;

        activeTab = ActiveTab.Game;

        tabGlobal.text.color = colorTabInactiveText;
        tabGlobal.image.color = colorTabInactiveBack;

        tabGame.text.color = colorTabNormalText;
        tabGame.image.color = colorTabNormalBack;
        
        SetUIValues();
    }

    public void SetStartWithVR() {
        if (activeTab == ActiveTab.Global) {
            prefs.StartWithSteamVR = startWithVRToggle.isOn;
        }
    }
    public void SetUseGlobal() {
        if (activeTab == ActiveTab.Game) {
            prefs.gameUseGlobal = useGlobalSettingsToggle.isOn;
        }
    }
    public void SetEnabled() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalIsEnabled = isEnabledToggle.isOn;
        } else {
            prefs.gameIsEnabled = isEnabledToggle.isOn;
        }
    }
    public void SetEnabledInDashboard() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalEnabledInDashboard = enabledInDashboardToggle.isOn;
        } else {
            prefs.gameIsEnabledInDashboard = enabledInDashboardToggle.isOn;
        }
    }
    public void SetMustPlayAlertSound() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalPlayAlertSound = playAlertSoundToggle.isOn;
        } else {
            prefs.gamePlayAlertSound = playAlertSoundToggle.isOn;
        }
    }
    public void SetDoNotActivateIfFacingDown() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalDoNotActivateIfFacingDown = doNotActivateIfFacingDownToggle.isOn;
        } else {
            prefs.gameDoNotActivateIfFacingDown = doNotActivateIfFacingDownToggle.isOn;
        }
    }
    public void SetBoundsBoxVisibility() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalBoundsBoxVisibility = (PlayBoundsPrefs.PlayBoundPrefsSettings.BoundsBoxVisibility)(boundsBoxVisibilityDropdown.selectedIndex);
        } else {
            prefs.gameBoundsBoxVisibility = (PlayBoundsPrefs.PlayBoundPrefsSettings.BoundsBoxVisibility)(boundsBoxVisibilityDropdown.selectedIndex);
        }
    }
    public void SetBoundsSize() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalBoundsSize = boundsSizeSlider.value;
        } else {
            prefs.gameBoundsSize = boundsSizeSlider.value;
        }
    }
    public void SetFadeInTime() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalTimeFadeIn = fadeInTimeSlider.value;
        } else {
            prefs.gameTimeFadeIn = fadeInTimeSlider.value;
        }
    }
    public void SetFadeOutTime() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalTimeFadeOut = fadeOutTimeSlider.value;
        } else {
            prefs.gameTimeFadeOut = fadeOutTimeSlider.value;
        }
    }
    public void SetTextFlashTime() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalTimeTextFlash = textFlashTimeSlider.value;
        } else {
            prefs.gameTimeTextFlash = textFlashTimeSlider.value;
        }
    }
    public void SetColorRed() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalColorRed = colorRedSlider.value;
        } else {
            prefs.gameColorRed = colorRedSlider.value;
        }
    }
    public void SetColorGreen() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalColorGreen = colorGreenSlider.value;
        } else {
            prefs.gameColorGreen = colorGreenSlider.value;
        }
    }
    public void SetColorBlue() {
        if (activeTab == ActiveTab.Global) {
            prefs.globalColorBlue = colorBlueSlider.value;
        } else {
            prefs.gameColorBlue = colorBlueSlider.value;
        }
    }
}