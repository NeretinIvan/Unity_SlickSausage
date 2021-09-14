using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private GameObject sausage;
    [SerializeField] private Vector3 cameraShift;
    [SerializeField] [Min(0)] private float cameraSpeed;
    [Tooltip("Distance, on which camera speed will start greatly accelerate")]
    [SerializeField] [Min(0)] private float accelerationRange;
    [Tooltip("The strength of camera acceleration depending of distance")]
    [SerializeField] [Min(0)] private float accelerationMultiplayer;
    [Tooltip("The strength of camera acceleration stepped speed rising")]
    [SerializeField] [Min(0)] private float accelerationCurveKoef;

    [HideInInspector] public bool cameraIsMoving;
    private Vector3 startingDifference;

    void Start()
    {
        startingDifference = sausage.transform.position - transform.position + cameraShift;
        cameraIsMoving = true;
    }

    void Update()
    {
        if (sausage == null) return;
        if (!sausage.activeSelf) return;
        if (!cameraIsMoving) return;

        float newX = transform.position.x;
        float newY = transform.position.y;
        float newZ = transform.position.z;
        Vector3 destination = sausage.transform.position - startingDifference;
        Vector3 speedMultiplayer = CalculateSpeedMultiplayer(destination);
        speedMultiplayer = speedMultiplayer * Time.deltaTime;

        if (transform.position.x > destination.x)
        {
            newX = Mathf.Max(transform.position.x - cameraSpeed * speedMultiplayer.x, destination.x);
        }
        if (transform.position.x < destination.x)
        {
            newX = Mathf.Min(transform.position.x + cameraSpeed * speedMultiplayer.x, destination.x);
        }
        
        if (transform.position.y > destination.y)
        {
            newY = Mathf.Max(transform.position.y - cameraSpeed * speedMultiplayer.y, destination.y);
        }
        if (transform.position.y < destination.y)
        {
            newY = Mathf.Min(transform.position.y + cameraSpeed * speedMultiplayer.y, destination.y);
        }
        
        if (transform.position.z > destination.z)
        {
            newZ = Mathf.Max(transform.position.z - cameraSpeed * speedMultiplayer.z, destination.z);
        }
        if (transform.position.z < destination.z)
        {
            newZ = Mathf.Min(transform.position.z + cameraSpeed * speedMultiplayer.z, destination.z);
        }
        
        transform.position = new Vector3(newX, newY, newZ);
    }

    private Vector3 CalculateSpeedMultiplayer(Vector3 destination)
    {
        float Xdistance = Mathf.Abs(destination.x - transform.position.x);
        float Ydistance = Mathf.Abs(destination.y - transform.position.y);
        float Zdistance = Mathf.Abs(destination.z - transform.position.z);
        float Xmultiplayer = 1 + Mathf.Pow(Mathf.Max(Xdistance - accelerationRange, 0) * accelerationMultiplayer, accelerationCurveKoef);
        float Ymultiplayer = 1 + Mathf.Pow(Mathf.Max(Ydistance - accelerationRange, 0) * accelerationMultiplayer, accelerationCurveKoef);
        float Zmultiplayer = 1 + Mathf.Pow(Mathf.Max(Zdistance - accelerationRange, 0) * accelerationMultiplayer, accelerationCurveKoef);
        return new Vector3(Xmultiplayer, Ymultiplayer, Zmultiplayer);
    } 
    
}
