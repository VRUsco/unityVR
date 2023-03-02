using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject PanelTimer;
    [SerializeField] private GameObject PanelInfo;

    [SerializeField] public TMP_Text StreetUI;
    [SerializeField] public TMP_Text CareerUI;

    public static bool isPaused = false;
    public static bool isEnd = false;

    public InputActionProperty gripAnimationAction;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Update()
    {
        float griValue = gripAnimationAction.action.ReadValue<float>();
        if (isEnd) {
            if (griValue != 0)
            {
                if (isPaused)
                {
                    ChangeScene("Home");
                    Time.timeScale = 1f;
                }
            }
                
        }
    }

    public bool isGamePaused()
    {
        return isPaused;
    }

    public void UpdatePause()
    {

        isPaused = !isPaused;
        if (isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void UpdateEnd()
    {

        isEnd = !isEnd;
    }

    public static void ChangeScene(string Scene)
    {
        isPaused = false;
        isEnd = false;
        CargaNivel.NivelCarga(Scene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}