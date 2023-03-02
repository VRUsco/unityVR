using System;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public Light pointLightGreen;

    private async void OnTriggerEnter(Collider obj)
    {
        SaveManager save = FindObjectOfType<SaveManager>();

        if (obj.tag == "PlayerController" && pointLightGreen.enabled == true)
        {
            DateTime fecha_hora = DateTime.Now;
            await save.IdTipoErrorAsync("CPPI");
            save.IncreaseError(LocalizationManager.Localize("[MapUiErrorCrosswalk]"), fecha_hora);
        }
    }
}
