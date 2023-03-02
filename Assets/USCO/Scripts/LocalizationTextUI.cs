using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizationTextUI : MonoBehaviour
{
    public string LocalizationKey;

    void Update()
    {
        Localize();
    }

    private void Localize()
    {
        GetComponent<TextMeshProUGUI>().text = LocalizationManager.Localize(LocalizationKey);
    }
}