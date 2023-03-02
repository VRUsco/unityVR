using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI TextContinue;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private AudioSource AudioPanel;

    public string key;
    public float textSpeed = 0.5f;

    public InputActionProperty pinchAnimationAction;

    public InputActionProperty gripAnimationAction;

    void Start()
    {
        StartDialogue();
    }

    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        float griValue = gripAnimationAction.action.ReadValue<float>();

        if (triggerValue != 0 || dialogueText.text == LocalizationManager.Localize(key))
        {
            StopAllCoroutines();
            dialogueText.text = LocalizationManager.Localize(key);
            TextContinue.text = LocalizationManager.Localize("[MapDialogueContinue]");
        }
        if (griValue != 0)
        {
            StopAllCoroutines();
            LimpiarDialogue();
            Time.timeScale = 1f;
        }
    }

    public void StartDialogue()
    {
        Time.timeScale = 0f;
        StartCoroutine(WriteLine());
        AudioPanel.PlayOneShot(LocalizationManager.LocalizeAudio(key));
    }
    public void StartDialogueMovement()
    {
        dialoguePanel.SetActive(true);
        StartCoroutine(WriteLine());
        Invoke("LimpiarDialogue", (500 * Time.deltaTime));
    }

    void LimpiarDialogue(){
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
    }

    IEnumerator WriteLine()
    {
        string Line = LocalizationManager.Localize(key);
        foreach (char letter in Line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    public void StartDialogueMovementCheckPoint(string keyLlegada)
    {
        dialoguePanel.SetActive(true);
        StartCoroutine(WriteLineCheckPoint(keyLlegada));
        AudioPanel.PlayOneShot(LocalizationManager.LocalizeAudio(keyLlegada));
        Invoke("LimpiarDialogue", (100 * Time.deltaTime));
    }
    IEnumerator WriteLineCheckPoint(string keyLlegada)
    {
        string Line = LocalizationManager.Localize(keyLlegada);
        foreach (char letter in Line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }
}
