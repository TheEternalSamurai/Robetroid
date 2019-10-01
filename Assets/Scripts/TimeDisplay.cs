using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    private Text timeText;
    private float time;

    void Start()
    {
        timeText = gameObject.GetComponent<Text>();
        timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString("0.0");
    }

    void Update()
    {
        timeText.text = "Time: " + Time.timeSinceLevelLoad.ToString("0.0");
    }
}
