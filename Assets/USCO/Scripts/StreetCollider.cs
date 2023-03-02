using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetCollider : MonoBehaviour
{
    private async void OnTriggerEnter(Collider obj)
    {
        SaveManager save = FindObjectOfType<SaveManager>();

        if (obj.tag == "Player")
        {
            DateTime fecha_hora = DateTime.Now;
            await save.IdTipoErrorAsync("CPI");
            save.IncreaseError(LocalizationManager.Localize("[MapUiError]"), fecha_hora);
        }
    }
}
