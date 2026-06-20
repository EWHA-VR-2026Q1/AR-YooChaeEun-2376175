using UnityEngine;
using UnityEngine.SceneManagement;

public class GPSLocationChecker : MonoBehaviour
{
    public EX_GPS gps;

    public double targetLatitude = 37.566827;
    public double targetLongitude = 126.978113;

    public float radius = 100f;

    private bool entered = false;

    void Update()
    {
        if (gps == null) return;

        float distance =
            Vector2.Distance(
                new Vector2((float)gps.latitude,
                            (float)gps.longitude),
                new Vector2((float)targetLatitude,
                            (float)targetLongitude)
            ) * 111000f;

        Debug.Log("거리 : " + distance);

        if (!entered && distance < radius)
        {
            entered = true;

            SceneManager.LoadScene("HW23_MobileData");
        }
    }
}