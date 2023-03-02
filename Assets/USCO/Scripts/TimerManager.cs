using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    [SerializeField] public float timeElapsed;

    public string time;

    private int minutes, seconds, miliseconds;

    string getTime(float n)
    {
        minutes = (int)(timeElapsed / 60f);
        seconds = (int)(timeElapsed - (60f * minutes));
        //miliseconds = (int)((timeElapsed - (int)timeElapsed) * 100f);
        time = string.Format("{0:00}:{1:00}", minutes, seconds);
        return time;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        timerText.text = getTime(timeElapsed);

        if (minutes > 9)
        {
            timerText.color = new Color(255,0,0,255);
        }
        //timerText.text = timeElapsed.ToString();
    }
}
