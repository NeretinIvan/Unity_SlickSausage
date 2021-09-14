using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sausage : MonoBehaviour
{
    [Tooltip("Y coordinate, below which considered as fail")]
    public float deathHeightY;
    [HideInInspector()] public bool chargingAllowed;
    [HideInInspector()] public bool gameOver;
    private Rigidbody rigidbody;
    private int collisionCount;
    [SerializeField] private GameObject progressControllerObject;
    private ProgressController progressController;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        progressController = progressControllerObject.GetComponent<ProgressController>();
        Initialize();
    }

    public void Initialize()
    {
        chargingAllowed = false;
        gameOver = false;
        collisionCount = 0;
    }

    public void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
        if (collisionCount >= 0)
        {
            chargingAllowed = true;
        }       
    }

    public void OnCollisionExit(Collision collision)
    {
        collisionCount--;
        if (collisionCount <= 0)
        {
            chargingAllowed = false;
        }       
    }

    private void Update()
    {
        if ((transform.position.y <= deathHeightY) && (!gameOver))
        {            
            gameOver = true;
            chargingAllowed = false;
            SausageFailed();
        }
    }

    public void FinishReached()
    {
        if (!gameOver)
        {         
            gameOver = true;
            chargingAllowed = false;
            progressController.AdmitVictory();
        }        
    }

    public void SausageFailed()
    {
        progressController.AdmitFail();
    }

    public void ChargeSausage(Vector3 direction, float force)
    {
        if (!chargingAllowed) return;
        direction = Vector3.Normalize(direction);
        rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }
}
