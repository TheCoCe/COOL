using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private float currentTime;
    [SerializeField] private int pointsPerSecondReduce;
    [SerializeField] private int maxPoints;

    // Use this for initialization
    void Awake()
    {
        currentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
    }

    public string GetTime()
    {
        int minutes = (int)(currentTime / 60);
        int seconds = (int)(currentTime - minutes * 60);

        string minuteString = minutes.ToString();
        string secondString = seconds.ToString();

        if (minutes < 10)
            minuteString = "0" + minuteString;
        if (seconds < 10)
            secondString = "0" + secondString;

        return minuteString + ":" + secondString;
    }

    public int GetTimeScore()
    {
        return Mathf.Clamp(maxPoints - (int)(currentTime * pointsPerSecondReduce), 0, maxPoints);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 150, 50, 100, 50), "Time: " + GetTime() + "\nDEBUG: " + GetTimeScore().ToString());
    }
}
