using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float timeMultiplier = 10f;

    private DateTime currentTime;

    [SerializeField]
    private Text timeText;
    [SerializeField]  
    private Text dateText;
    [SerializeField]
    private Light sunLight;
    [SerializeField]
    private float sunRiseHour = 6f;
    [SerializeField]
    private float sunSetHour = 22f;

    private TimeSpan sunRiseTime;
    private TimeSpan sunSetTime;



    void Start()
    {
        currentTime = new DateTime(1500, 1, 1, 8, 0, 0);

        sunRiseTime = new TimeSpan((int)sunRiseHour, 0, 0);
        sunSetTime = new TimeSpan((int)sunSetHour, 0, 0);
    }

    private void FixedUpdate()
    {
        UpdateTimeofDay();
        RotateSun();
    }

    private void UpdateTimeofDay()
    {
        currentTime = currentTime.AddMinutes(Time.deltaTime * timeMultiplier);
        timeText.text = currentTime.ToString("HH:mm");
        dateText.text = currentTime.ToString("dd/MM/yyyy");
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;
        if(difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    private void RotateSun()
    {
        float sunLigthRotation;
        if (currentTime.TimeOfDay > sunRiseTime && currentTime.TimeOfDay < sunSetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunRiseTime, sunSetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunRiseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
            sunLigthRotation = Mathf.Lerp(0f, 180f, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunSetTime, sunRiseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunSetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLigthRotation = Mathf.Lerp(180f, 360f, (float)percentage);
        }

        sunLight.transform.localRotation = Quaternion.AngleAxis(sunLigthRotation, Vector3.right);
    }
}
