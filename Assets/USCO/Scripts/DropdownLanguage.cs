using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownLanguage : MonoBehaviour
{
    public TMPro.TMP_Dropdown Drop;
    public LocalizationManager localManager;

    void Awake()
    {
        lista();
    }

    private void Update()
    {
        if(Drop.captionText.text != "Default")
        {
            OnMyButtonChange(Drop.captionText.text);
        }
    }

    public void lista()
    {
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"Assets/Language");
        System.IO.FileInfo[] fileNames = di.GetFiles("*.txt");

        foreach (System.IO.FileInfo fi in fileNames)
        {
            TMPro.TMP_Dropdown.OptionData m_NewData = new TMPro.TMP_Dropdown.OptionData();
            name = fi.Name;
            name = name.Replace("mission_", "");
            name = name.Replace(".txt", "");
            m_NewData.text = name;
            Drop.options.Add(m_NewData);
        }
    }

    public void OnMyButtonChange(string text)
    {
        LocalizationManager.ChangeLanguage();
        LocalizationManager.FileRead(text);
        localManager.GetSongsFromFolder(text);
    }
}
