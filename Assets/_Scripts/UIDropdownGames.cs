using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDropdownGames : MonoBehaviour {

    private bool bSelected = false;

    public Text label;
    public Text text;

    private string[] elements;
    private int[] elementsAppId;
    private int selectedIndex;

	// Use this for initialization
	void Awake () {
        //CreateListOfGames();
	}

    // Update is called once per frame
    void Update() {
        CreateListOfGames();
        if (bSelected && UIWindowDropdown.instance.IsDone()) {
            selectedIndex = UIWindowDropdown.instance.GetIndex();
            PlayBounds_Prefs_Handler.instance.LoadGame(elementsAppId[selectedIndex]);
            GetComponentInParent<PlayBounds_Menu_Redux>().SetUIValues();
            bSelected = false;
        }
        if (elements.Length > 0) {
            selectedIndex = Mathf.Clamp(selectedIndex, 0, elements.Length);
            text.text = elements[selectedIndex];
        }
    }

    private void CreateListOfGames() {
        List<string> elementsList = new List<string>();
        List<int> elementsAppIdList = new List<int>();

        elementsList.Add("Current game: "+(PlayBoundsManager.instance.IsRunningAnApp() ? PlayBoundsManager.instance.GetRunningAppIdName() : "<none>"));
        elementsAppIdList.Add(-1);

        elementsAppIdList.AddRange(PlayBounds_Prefs_Handler.instance.GetGames());
        for (int index = 1; index < elementsAppIdList.Count; index++) {
            elementsList.Add(PlayBoundsManager.instance.GetGameName(elementsAppIdList[index]));
        }

        elements = elementsList.ToArray();
        elementsAppId = elementsAppIdList.ToArray();
    }

    public void OnClick() {
        UIWindowDropdown.instance.Show("Game specific settings", elements, selectedIndex);
        bSelected = true;
    }
}
