using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Android;

public class GPSManager : MonoBehaviour
{
    private bool isGPSEnabled;
    private float latitude;
    private float longitude;
    public TextMeshProUGUI Latitude;
    public TextMeshProUGUI Longitude;
    public float targetLat;
    public float targetLong;
    public GameObject invokeAR;
    public float limit;

    DistanceCalculator distanceCalculator;

    IEnumerator Start()
    {
        
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("GPS is not enabled by the user.");
            yield break;
        }       
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
                yield break;
            }
        }        
        Input.location.Start();        
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }       
        if (maxWait <= 0)
        {
            Debug.LogError("Unable to initialize GPS service.");
            yield break;
        }        
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location.");
            yield break;
        }        
        isGPSEnabled = true;
        distanceCalculator = new DistanceCalculator();
    }

    void Update()
    {
        
        if (isGPSEnabled)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            Debug.Log("Latitude: " + latitude);
            Debug.Log("Longitude: " + longitude);
            Latitude.text = latitude.ToString();
            Longitude.text = longitude.ToString();
            //Here the current lat and long values along with target lat and long values are sent to DistanceCalculator script to check the distance between the two places.            
            float distance = distanceCalculator.CalculateDistance(latitude, longitude, targetLat, targetLong);
           // Debug.Log("Distance between current point and target point: " + distance.ToString("F2") + " meters");
                        
            if (distance < limit)
            {
                invokeAR.SetActive(true);
            }
        }
    }

    void OnDisable()
    {
       
        Input.location.Stop();
    }
}
