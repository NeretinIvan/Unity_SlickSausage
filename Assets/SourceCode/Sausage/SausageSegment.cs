using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageSegment : MonoBehaviour
{
    public GameObject sausageObject;
    private Sausage sausage;

    private void Awake()
    {
        sausage = sausageObject.GetComponent<Sausage>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        sausage.OnCollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        sausage.OnCollisionExit(collision);
    }
}
