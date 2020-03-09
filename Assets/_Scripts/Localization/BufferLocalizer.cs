using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferLocalizer : MonoBehaviour {

    public string id;

    [HideInInspector]
    public string localizedValue;

    private void Awake()
    {

    }

    public void Start()
    {
        LanguageManager.instance.onLanguageChange.AddListener(UpdateText);

        UpdateText();
    }

    public virtual void UpdateText()
    {
        localizedValue = LanguageManager.instance.GetTranslation(id);
    }
}
