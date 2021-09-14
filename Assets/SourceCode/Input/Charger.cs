using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour
{
    [SerializeField] private InputHandler.InputType inputType;
    [SerializeField] private GameObject sausage;
    [SerializeField] [Min(0)] private float strengthKoef = 0.001f;
    [Tooltip("Amount of prediction iterations. This will increase length of trace at cost of performance")]
    [SerializeField] [Min(0)] private int maxTracePredictionIterations;
    [Tooltip("Increasing will make trace longer at cost of lane accuracy. Performance will not be decreased")]
    [SerializeField] [Min(1)] private float stepKoef;

    private InputHandler inputHandler;
    private Sausage sausageComponent;
    private TraceDrawer traceDrawer;
    private ChargeLineDrawer chargeLineDrawer;

    private bool charging = false;
    private Vector2 startingPoint;
    private float strength;
    private Vector3 direction;

    private void Awake()
    {
        if (inputType == InputHandler.InputType.touch)
        {
            inputHandler = new InputHandler_touch();
        }
        else
        {
            inputHandler = new InputHandler_mouse();
        }
        sausageComponent = sausage.GetComponent<Sausage>();
        traceDrawer = GetComponentInChildren<TraceDrawer>();
        chargeLineDrawer = GetComponentInChildren<ChargeLineDrawer>();
    }

    private void ChargingStarted(Vector2 touchPosition)
    {
        charging = true;
        startingPoint = touchPosition;
        strength = 0;
        direction = Vector3.zero;
        chargeLineDrawer.Activate();
    }

    private void ChargingEnded(Vector2 touchPosition)
    {
        charging = false;
        sausageComponent.ChargeSausage(direction, strength);
        traceDrawer.ClearTrace();
        chargeLineDrawer.Deactivate();
    }

    public void InterruptCharging()
    {
        charging = false;
        traceDrawer.ClearTrace();
        chargeLineDrawer.Deactivate();
    }

    private void OnCharging(Vector2 touchPosition)
    {
        strength = Vector2.Distance(startingPoint, touchPosition) * strengthKoef;
        direction = startingPoint - touchPosition;
        traceDrawer.MakePrediction(direction, strength, maxTracePredictionIterations, stepKoef);
        chargeLineDrawer.SetLinePositions(startingPoint, touchPosition);
    }

    private void CheckInput()
    {
        if (!sausageComponent.chargingAllowed)
        {
            InterruptCharging();
            return;
        }

        if (inputHandler.InputStarted())
        {
            ChargingStarted(inputHandler.GetInputPosition());
        }
        if (inputHandler.InputEnded())
        {
            ChargingEnded(inputHandler.GetInputPosition());
        }
    }

    void Update()
    {
        CheckInput();

        if (charging)
        {
            OnCharging(inputHandler.GetInputPosition());
        }
    }
}
