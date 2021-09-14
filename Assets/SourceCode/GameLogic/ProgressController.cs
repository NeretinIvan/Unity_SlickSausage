using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private GameObject sausage;
    [SerializeField] private GameObject spawnPoint;
    [Header("Interface")]
    [SerializeField] private GameObject startingScreen;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private GameObject victoryScreen;

    private CameraMoving cameraMoving;

    private void Awake()
    {
        Camera.main.gameObject.TryGetComponent<CameraMoving>(out cameraMoving);
        startingScreen.SetActive(true);
        failScreen.SetActive(false);
        victoryScreen.SetActive(false);
        sausage.SetActive(false);
    }

    public void AdmitVictory()
    {
        victoryScreen.SetActive(true);
        LockCamera();
    }

    public void AdmitFail()
    {
        failScreen.SetActive(true);
        LockCamera();
    }

    public void LockCamera()
    {
        if (cameraMoving != null)
        {
            cameraMoving.cameraIsMoving = false;
        }
    }

    public void UnlockCamera()
    {
        if (cameraMoving != null)
        {
            cameraMoving.cameraIsMoving = true;
        }
    }

    public void Restart()
    {
        sausage.SetActive(true);
        sausage.GetComponent<Sausage>().Initialize();
        sausage.transform.position = spawnPoint.transform.position;
        sausage.GetComponent<Rigidbody>().velocity = Vector3.zero;
        UnlockCamera();
        HideAllScreens();
    }

    public void HideAllScreens()
    {
        startingScreen.SetActive(false);
        failScreen.SetActive(false);
        victoryScreen.SetActive(false);
    }
}
