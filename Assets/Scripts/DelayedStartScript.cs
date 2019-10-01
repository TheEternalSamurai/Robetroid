using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayedStartScript : MonoBehaviour
{
    public int countdown = 3;
    public Text countdownText;

    void Start()
    {
        countdownText.text = countdown.ToString("0");
        StartCoroutine("Countdown");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Countdown()
    {
        Time.timeScale = 0f;
        float countdownTime = Time.realtimeSinceStartup + countdown;

        while (Time.realtimeSinceStartup < countdownTime)
        {
            int countdownLeft = (int)(countdownTime - Time.realtimeSinceStartup + 1);
            countdownText.text = (countdownLeft).ToString("0");
            yield return 0;
        }
        Destroy(countdownText.gameObject);
        Destroy(gameObject);
        Time.timeScale = 1f;
    }
}
