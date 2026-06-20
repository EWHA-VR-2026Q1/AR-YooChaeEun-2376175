using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_GPS : MonoBehaviour
{
    public double latitude;
    public double longitude;
    public double altitude;
    public double horizontalAccuracy;
    public double verticalAccuracy;
    public double timestamp;

    int waitTime = 20;
    bool keepAlive = true;

    [Range(10, 150)]
    public int fontSize = 30;

    public Color color = Color.black;
    public float width = 1000;
    public float height = 1000;

    string debugMessage = "";
    int counter = 0;

    void Start()
    {
        Get_GPS();
    }

    public void Get_GPS()
    {
        StartCoroutine(IGet_GPS());
    }

    public void Stop_GPS()
    {
        Input.location.Stop();
    }

    IEnumerator IGet_GPS()
    {
        debugMessage = "";

        // 권한 체크
        if (!Input.location.isEnabledByUser)
        {
            debugMessage =
                "GPS permission check failed.\n" +
                "Trying to start GPS anyway...";
            print(debugMessage);
        }

        float desiredAccuracyInMeters = 10f;
        float updateDistanceInMeters = 10f;

        debugMessage = "Starting GPS...";
        print(debugMessage);

        Input.location.Start(
            desiredAccuracyInMeters,
            updateDistanceInMeters
        );

        int maxWait = waitTime;

        while (
            Input.location.status ==
            LocationServiceStatus.Initializing
            && maxWait > 0
        )
        {
            debugMessage =
                "Initializing GPS... " +
                maxWait;

            print(debugMessage);

            yield return new WaitForSeconds(1);

            maxWait--;
        }

        if (maxWait < 1)
        {
            debugMessage =
                "GPS Timed Out";

            print(debugMessage);

            yield break;
        }

        if (
            Input.location.status ==
            LocationServiceStatus.Failed
        )
        {
            debugMessage =
                "GPS Failed";

            print(debugMessage);

            yield break;
        }

        debugMessage =
            "GPS Connected!\n" +
            "Latitude : " + Input.location.lastData.latitude +
            "\nLongitude : " + Input.location.lastData.longitude;

        print(debugMessage);

        while (keepAlive)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;
            horizontalAccuracy =
                Input.location.lastData.horizontalAccuracy;
            verticalAccuracy =
                Input.location.lastData.verticalAccuracy;
            timestamp =
                Input.location.lastData.timestamp;

            counter++;

            debugMessage =
                "Latitude = " + latitude +
                "\nLongitude = " + longitude +
                "\nAltitude = " + altitude +
                "\nHorizontal Accuracy = " + horizontalAccuracy +
                "\nVertical Accuracy = " + verticalAccuracy +
                "\nTimeStamp = " + timestamp +
                "\nCounter = " + counter;

            print(debugMessage);

            yield return new WaitForSeconds(5f);
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        style.fontSize = fontSize;
        style.normal.textColor = color;
        style.alignment = TextAnchor.UpperLeft;

        GUI.Label(
            new Rect(0, 0, width, height),
            debugMessage,
            style
        );
    }
}