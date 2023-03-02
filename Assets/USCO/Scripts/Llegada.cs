using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llegada : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject PanelTimer;
    [SerializeField] private GameObject PanelInfo;
    public SaveManager save;
    public AudioSource audioLlegada;
    public AudioSource audioAmbiente;

    private void Start()
    {
        audioLlegada.mute = true;
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Player")
        {
            audioLlegada.mute = false;
            audioLlegada.Play();
            audioAmbiente.Pause();
            PanelTimer.SetActive(false);
            PanelInfo.SetActive(false);
            GameManager.Instance.UpdatePause();
            GameManager.Instance.UpdateEnd();
            save.SaveAppAsync();
        }
    }
}
