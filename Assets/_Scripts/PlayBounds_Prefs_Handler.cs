using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class PlayBounds_Prefs_Handler : MonoBehaviour
{
    public static PlayBounds_Prefs_Handler instance;

    private PlayBoundsPrefs prefs = new PlayBoundsPrefs();
    private PlayBoundsPrefs.PlayBoundPrefsApp prefsGame;
    private string _filePath = "";
    private string _fileName = "";
    private bool bLoadCurrentGame = true;

    public void Awake()
    {
        instance = this;
    }

    public void SetFilePath(string path, string fileName)
    {
        _filePath = path;
        _fileName = fileName;
    }

    public List<int> GetGames()
    {
        return prefs.GetGames();
    }

    public bool StartWithSteamVR
    {
        get { return prefs.global.StartWithSteamVR; }
        set
        {
            Debug.Log("Start with Steam VR " + value);
            prefs.global.StartWithSteamVR = value;
            /*
            if (prefs.global.StartWithSteamVR != value)
            {
                prefs.global.StartWithSteamVR = value;
                //PlayBounds_Director.instance.SetManifestAutoLaunch(value);
                try
                {
                    PlayBounds_Director.instance.SetApplicationLaunchOnStart(value);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e + e.StackTrace);
                }
            }
            */
            Save();
        }
    }

    public string language
    {
        get { return prefs.global.language; }
        set
        {
            Debug.Log("Language " + value);
            prefs.global.language = value;
            LanguageManager.instance.SelectLanguage(value);
            Save();
        }
    }

    public bool gameUseGlobal
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.useGlobal;
            }
            else
            {
                return true;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.useGlobal = value;
                Save();
            }
        }
    }

    public bool globalIsEnabled
    {
        get { return prefs.global.settings.enabled; }
        set
        {
            prefs.global.settings.enabled = value;
            Save();
        }
    }

    public bool gameIsEnabled
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.enabled;
            }
            else
            {
                return globalIsEnabled;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.enabled = value;
                Save();
            }
        }
    }

    public bool globalEnabledInDashboard
    {
        get { return prefs.global.settings.enabledInDashboard; }
        set
        {
            prefs.global.settings.enabledInDashboard = value;
            Save();
        }
    }

    public bool gameIsEnabledInDashboard
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.enabledInDashboard;
            }
            else
            {
                return globalEnabledInDashboard;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.enabledInDashboard = value;
                Save();
            }
        }
    }

    public bool globalPlayAlertSound
    {
        get { return prefs.global.settings.playAlertSound; }
        set
        {
            prefs.global.settings.playAlertSound = value;
            Save();
        }
    }

    public bool gamePlayAlertSound
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.playAlertSound;
            }
            else
            {
                return globalPlayAlertSound;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.playAlertSound = value;
                Save();
            }
        }
    }

    public bool globalDoNotActivateIfFacingDown
    {
        get { return prefs.global.settings.doNotActivateIfFacingDown; }
        set
        {
            prefs.global.settings.doNotActivateIfFacingDown = value;
            Save();
        }
    }

    public bool gameDoNotActivateIfFacingDown
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.doNotActivateIfFacingDown;
            }
            else
            {
                return globalDoNotActivateIfFacingDown;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.doNotActivateIfFacingDown = value;
                Save();
            }
        }
    }

    public PlayBoundsPrefs.PlayBoundPrefsSettings.BoundsBoxVisibility globalBoundsBoxVisibility
    {
        get { return prefs.global.settings.boundsBoxVisibility; }
        set
        {
            prefs.global.settings.boundsBoxVisibility = value;
            Save();
        }
    }

    public PlayBoundsPrefs.PlayBoundPrefsSettings.BoundsBoxVisibility gameBoundsBoxVisibility
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.boundsBoxVisibility;
            }
            else
            {
                return globalBoundsBoxVisibility;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.boundsBoxVisibility = value;
                Save();
            }
        }
    }

    /*public bool HideMainWindow 
    {
        get 
        {
            return prefs.HideMainWindow;
        }
        set 
        {
            prefs.HideMainWindow = value;
            Save();
        }
    }*/

    public float globalColorRed
    {
        get { return prefs.global.settings.colorRed; }
        set
        {
            prefs.global.settings.colorRed = value;
            Save();
        }
    }

    public float gameColorRed
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.colorRed;
            }
            else
            {
                return globalColorRed;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.colorRed = value;
                Save();
            }
        }
    }

    public float globalColorGreen
    {
        get { return prefs.global.settings.colorGreen; }
        set
        {
            prefs.global.settings.colorGreen = value;
            Save();
        }
    }

    public float gameColorGreen
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.colorGreen;
            }
            else
            {
                return globalColorGreen;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.colorGreen = value;
                Save();
            }
        }
    }

    public float globalColorBlue
    {
        get { return prefs.global.settings.colorBlue; }
        set
        {
            prefs.global.settings.colorBlue = value;
            Save();
        }
    }

    public float gameColorBlue
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.colorBlue;
            }
            else
            {
                return globalColorBlue;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.colorBlue = value;
                Save();
            }
        }
    }

    public float globalBoundsSize
    {
        get { return prefs.global.settings.boundsSize; }
        set
        {
            prefs.global.settings.boundsSize = value;
            Save();
        }
    }

    public float gameBoundsSize
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.boundsSize;
            }
            else
            {
                return globalBoundsSize;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.boundsSize = value;
                Save();
            }
        }
    }

    public float globalTimeFadeIn
    {
        get { return prefs.global.settings.timeFadeIn; }
        set
        {
            prefs.global.settings.timeFadeIn = value;
            Save();
        }
    }

    public float gameTimeFadeIn
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.timeFadeIn;
            }
            else
            {
                return globalTimeFadeIn;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.timeFadeIn = value;
                Save();
            }
        }
    }

    public float globalTimeFadeOut
    {
        get { return prefs.global.settings.timeFadeOut; }
        set
        {
            prefs.global.settings.timeFadeOut = value;
            Save();
        }
    }

    public float gameTimeFadeOut
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.timeFadeOut;
            }
            else
            {
                return globalTimeFadeOut;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.timeFadeOut = value;
                Save();
            }
        }
    }

    public float globalTimeTextFlash
    {
        get { return prefs.global.settings.timeTextFlash; }
        set
        {
            prefs.global.settings.timeTextFlash = value;
            Save();
        }
    }

    public float gameTimeTextFlash
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.timeTextFlash;
            }
            else
            {
                return globalTimeTextFlash;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.timeTextFlash = value;
                Save();
            }
        }
    }

    public string globalTextReturn
    {
        get { return prefs.global.settings.textReturn; }
        set
        {
            if (!value.Equals(globalTextReturn))
            {
                prefs.global.settings.textReturn = value;
                Save();
            }
        }
    }

    public string gameTextReturn
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.textReturn;
            }
            else
            {
                return globalTextReturn;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.textReturn = value;
                Save();
            }
        }
    }

    public string globalTextStepLeft
    {
        get { return prefs.global.settings.textStepLeft; }
        set
        {
            if (!value.Equals(globalTextStepLeft))
            {
                prefs.global.settings.textStepLeft = value;
                Save();
            }
        }
    }

    public string gameTextStepLeft
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.textStepLeft;
            }
            else
            {
                return globalTextStepLeft;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.textStepLeft = value;
                Save();
            }
        }
    }

    public string globalTextStepRight
    {
        get { return prefs.global.settings.textStepRight; }
        set
        {
            if (!value.Equals(globalTextStepRight))
            {
                prefs.global.settings.textStepRight = value;
                Save();
            }
        }
    }

    public string gameTextStepRight
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.textStepRight;
            }
            else
            {
                return globalTextStepRight;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.textStepRight = value;
                Save();
            }
        }
    }

    public string globalTextStepBack
    {
        get { return prefs.global.settings.textStepBack; }
        set
        {
            if (!value.Equals(globalTextStepBack))
            {
                prefs.global.settings.textStepBack = value;
                Save();
            }
        }
    }

    public string gameTextStepBack
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.textStepBack;
            }
            else
            {
                return globalTextStepBack;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.textStepBack = value;
                Save();
            }
        }
    }

    public string globalTextStepForward
    {
        get { return prefs.global.settings.textStepForward; }
        set
        {
            if (!value.Equals(globalTextStepForward))
            {
                prefs.global.settings.textStepForward = value;
                Save();
            }
        }
    }

    public string gameTextStepForward
    {
        get
        {
            if (prefsGame != null)
            {
                return prefsGame.settings.textStepForward;
            }
            else
            {
                return globalTextStepForward;
            }
        }
        set
        {
            if (prefsGame != null)
            {
                prefsGame.settings.textStepForward = value;
                Save();
            }
        }
    }


    public float GetBoundsSize()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameBoundsSize;
        }
        else
        {
            return globalBoundsSize;
        }
    }

    public float GetTimeFadeIn()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameTimeFadeIn;
        }
        else
        {
            return globalTimeFadeIn;
        }
    }

    public float GetTimeFadeOut()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameTimeFadeOut;
        }
        else
        {
            return globalTimeFadeOut;
        }
    }

    public float GetTimeTextFlash()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameTimeTextFlash;
        }
        else
        {
            return globalTimeTextFlash;
        }
    }

    public bool IsEnabled()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameIsEnabled;
        }
        else
        {
            return globalIsEnabled;
        }
    }

    public bool IsEnabledInDashboard()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameIsEnabledInDashboard;
        }
        else
        {
            return globalEnabledInDashboard;
        }
    }

    public float GetColorRed()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameColorRed;
        }
        else
        {
            return globalColorRed;
        }
    }

    public float GetColorGreen()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameColorGreen;
        }
        else
        {
            return globalColorGreen;
        }
    }

    public float GetColorBlue()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameColorBlue;
        }
        else
        {
            return globalColorBlue;
        }
    }

    public Color GetColor()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return new Color(gameColorRed / 255f, gameColorGreen / 255f, gameColorBlue / 255f);
        }
        else
        {
            return new Color(globalColorRed / 255f, globalColorGreen / 255f, globalColorBlue / 255f);
        }
    }

    public void SetColor(Color color)
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            gameColorRed = color.r * 255f;
            gameColorGreen = color.g * 255f;
            gameColorBlue = color.b * 255f;
        }
        else
        {
            globalColorRed = color.r * 255f;
            globalColorGreen = color.g * 255f;
            globalColorBlue = color.b * 255f;
        }
    }

    public string GetTextReturn()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameTextReturn;
        }
        else
        {
            return globalTextReturn;
        }
    }

    public string GetTextStepLeft()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameTextStepLeft;
        }
        else
        {
            return globalTextStepLeft;
        }
    }

    public string GetTextStepRight()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameTextStepRight;
        }
        else
        {
            return globalTextStepRight;
        }
    }

    public string GetTextStepBack()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameTextStepBack;
        }
        else
        {
            return globalTextStepBack;
        }
    }

    public string GetTextStepForward()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameTextStepForward;
        }
        else
        {
            return globalTextStepForward;
        }
    }

    public bool MustPlayAlertSound()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gamePlayAlertSound;
        }
        else
        {
            return globalPlayAlertSound;
        }
    }

    public bool GetDoNotActivateIfFacingDown()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameDoNotActivateIfFacingDown;
        }
        else
        {
            return globalDoNotActivateIfFacingDown;
        }
    }

    public PlayBoundsPrefs.PlayBoundPrefsSettings.BoundsBoxVisibility GetBoundsBoxVisibility()
    {
        if (prefsGame != null && !prefsGame.useGlobal)
        {
            return gameBoundsBoxVisibility;
        }
        else
        {
            return globalBoundsBoxVisibility;
        }
    }


    public bool Save(bool skipSteam = false, PlayBoundsPrefs overrideP = null)
    {
        try
        {
            PlayBoundsPrefs p;

            if (overrideP != null)
                p = overrideP;
            else
                p = prefs;

            p.lastEditTime = (Int32) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            string text = JsonUtility.ToJson(prefs, true);
            string fullP = _filePath + _fileName;

            Debug.Log("Writing Local Prefs!");
            File.WriteAllText(fullP, text);

            try
            {
                if (!skipSteam)
                    SteamSave();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error when saving to Steam.");
            }

            return File.Exists(fullP);
        }
        catch (System.Exception e)
        {
            return false;
        }
    }

    public bool SteamSave()
    {
        if (SteamManager.Initialized && SteamRemoteStorage.IsCloudEnabledForAccount())
        {
            string text = JsonUtility.ToJson(prefs, true);

            var bytes = System.Text.Encoding.ASCII.GetBytes(text);
            var byteCount = System.Text.Encoding.ASCII.GetByteCount(text);

            Debug.Log("Writing Prefs to SteamCloud!");
            return SteamRemoteStorage.FileWrite(_fileName, bytes, byteCount);
        }
        else
            return false;
    }

    public bool Load()
    {
        Debug.Log("Loading prefs...");

        PlayBoundsPrefs fileP = FileLoad();
        PlayBoundsPrefs steamP = null; // SteamLoad();

        bool res = false;

        bool skipSteam = false;
        PlayBoundsPrefs p = new PlayBoundsPrefs();

        if (fileP != null && steamP != null)
        {
            if (fileP.lastEditTime >= steamP.lastEditTime)
                p = fileP;
            else
            {
                p = steamP;
                skipSteam = true;
            }

            res = true;
        }
        else if (fileP == null && steamP != null)
        {
            p = steamP;
            skipSteam = true;
            res = true;
        }
        else if (fileP != null && steamP == null)
        {
            p = fileP;
            res = true;
        }

        prefs = p;
        //Save(skipSteam);

        return res;
    }

    public PlayBoundsPrefs SteamLoad()
    {
        if (SteamManager.Initialized && SteamRemoteStorage.IsCloudEnabledForAccount())
        {
            if (SteamRemoteStorage.FileExists(_fileName))
            {
                string text = "";
                var byteCount = SteamRemoteStorage.GetFileSize(_fileName);
                var bytes = new byte[byteCount];

                Debug.Log("Reading Prefs from SteamCloud!");
                var fileC = SteamRemoteStorage.FileRead(_fileName, bytes, byteCount);

                if (fileC > 0)
                    text = System.Text.Encoding.ASCII.GetString(bytes);

                var o = (PlayBoundsPrefs) JsonUtility.FromJson(text, typeof(PlayBoundsPrefs));

                if (o != null)
                    return o;
            }
        }

        return null;
    }

    public PlayBoundsPrefs FileLoad()
    {
        string fullP = _filePath + _fileName;

        if (!File.Exists(fullP))
            return null;

        Debug.Log("Reading Local Prefs!");
        string text = File.ReadAllText(fullP);

        return (PlayBoundsPrefs) JsonUtility.FromJson(text, typeof(PlayBoundsPrefs));
    }

    public void ResetGlobal()
    {
        prefs.global.settings = new PlayBoundsPrefs.PlayBoundPrefsSettings();
        Save();
        Load();
    }

    public void ResetGame()
    {
        prefsGame.settings = new PlayBoundsPrefs.PlayBoundPrefsSettings();
        Save();
        Load();
    }

    public void Update()
    {
        if (prefsGame == null || (bLoadCurrentGame && prefsGame.appId != PlayBoundsManager.instance.GetRunningAppId()))
        {
            prefsGame = prefs.GetCurrentGame();
        }
    }

    public void LoadGame(int appId)
    {
        if (appId == -1)
        {
            prefsGame = prefs.GetCurrentGame();
            bLoadCurrentGame = true;
        }
        else
        {
            prefsGame = prefs.GetGame(appId);
            bLoadCurrentGame = false;
        }
    }

    public string GetText(bool bIsGlobal, string textName)
    {
        string text = "";
        switch (textName)
        {
            case "Return":
                if (!bIsGlobal)
                {
                    text = gameTextReturn;
                }
                else
                {
                    text = globalTextReturn;
                }

                break;

            case "Left":
                if (!bIsGlobal)
                {
                    text = gameTextStepLeft;
                }
                else
                {
                    text = globalTextStepLeft;
                }

                break;

            case "Right":
                if (!bIsGlobal)
                {
                    text = gameTextStepRight;
                }
                else
                {
                    text = globalTextStepRight;
                }

                break;

            case "Back":
                if (!bIsGlobal)
                {
                    text = gameTextStepBack;
                }
                else
                {
                    text = globalTextStepBack;
                }

                break;

            case "Forward":
                if (!bIsGlobal)
                {
                    text = gameTextStepForward;
                }
                else
                {
                    text = globalTextStepForward;
                }

                break;
        }

        return text;
    }

    public void SetText(bool bIsGlobal, string textName, string text)
    {
        switch (textName)
        {
            case "Return":
                if (!bIsGlobal)
                {
                    gameTextReturn = text;
                }
                else
                {
                    globalTextReturn = text;
                }

                break;

            case "Left":
                if (!bIsGlobal)
                {
                    gameTextStepLeft = text;
                }
                else
                {
                    globalTextStepLeft = text;
                }

                break;

            case "Right":
                if (!bIsGlobal)
                {
                    gameTextStepRight = text;
                }
                else
                {
                    globalTextStepRight = text;
                }

                break;

            case "Back":
                if (!bIsGlobal)
                {
                    gameTextStepBack = text;
                }
                else
                {
                    globalTextStepBack = text;
                }

                break;

            case "Forward":
                if (!bIsGlobal)
                {
                    gameTextStepForward = text;
                }
                else
                {
                    globalTextStepForward = text;
                }

                break;
        }
    }
}


[System.Serializable]
public class PlayBoundsPrefs
{
    public Int32 lastEditTime = 0;


    public PlayBoundPrefsGlobal global;
    public List<PlayBoundPrefsApp> games;

    public PlayBoundPrefsApp GetCurrentGame()
    {
        PlayBoundPrefsApp prefsGame;
        int appId = PlayBoundsManager.instance.GetRunningAppId();
        if (games == null)
        {
            games = new List<PlayBoundPrefsApp>();
        }

        int index = 0;
        while (index < games.Count && games[index].appId != appId)
        {
            index++;
        }

        if (index < games.Count)
        {
            prefsGame = games[index];
        }
        else
        {
            //Add new
            prefsGame = new PlayBoundPrefsApp();
            prefsGame.appId = appId;
            prefsGame.settings = new PlayBoundPrefsSettings();
            games.Add(prefsGame);
        }

        return prefsGame;
    }

    public PlayBoundPrefsApp GetGame(int appId)
    {
        PlayBoundPrefsApp prefsGame;
        if (games == null)
        {
            games = new List<PlayBoundPrefsApp>();
        }

        int index = 0;
        while (index < games.Count && games[index].appId != appId)
        {
            index++;
        }

        if (index < games.Count)
        {
            prefsGame = games[index];
        }
        else
        {
            //Add new
            prefsGame = new PlayBoundPrefsApp();
            prefsGame.appId = appId;
            prefsGame.settings = new PlayBoundPrefsSettings();
            games.Add(prefsGame);
        }

        return prefsGame;
    }

    public List<int> GetGames()
    {
        List<int> gamesAppId = new List<int>();

        if (games != null)
        {
            int index = 0;
            while (index < games.Count)
            {
                if (games[index].appId != -1)
                {
                    gamesAppId.Add(games[index].appId);
                }

                index++;
            }
        }

        return gamesAppId;
    }

    [System.Serializable]
    public class PlayBoundPrefsSettings
    {
        public enum BoundsBoxVisibility
        {
            WhenVisionIsBlocked,
            WhenVisionIsBlocked5Seconds,
            AlwaysVisible
        };

        public bool enabled = true;
        public bool enabledInDashboard = false;
        public bool playAlertSound = true;
        public bool doNotActivateIfFacingDown = false;
        public BoundsBoxVisibility boundsBoxVisibility = BoundsBoxVisibility.WhenVisionIsBlocked;

        public float colorRed = 0;
        public float colorGreen = 0;
        public float colorBlue = 0;

        public float boundsSize = 1f;
        public float timeFadeIn = 0.2f;
        public float timeFadeOut = 0.5f;
        public float timeTextFlash = 0.5f;

        public string textReturn = "RETURN TO CENTER";
        public string textStepLeft = "STEP LEFT";
        public string textStepRight = "STEP RIGHT";
        public string textStepBack = "STEP BACKWARDS";
        public string textStepForward = "STEP FORWARDS";
    }

    [System.Serializable]
    public class PlayBoundPrefsApp
    {
        public int appId;
        public bool useGlobal = true;
        public PlayBoundPrefsSettings settings;
    }

    [System.Serializable]
    public class PlayBoundPrefsGlobal
    {
        public string language = "none";
        public bool StartWithSteamVR = false;
        public PlayBoundPrefsSettings settings;
    }
}