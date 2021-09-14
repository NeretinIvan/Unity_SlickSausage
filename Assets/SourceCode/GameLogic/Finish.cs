using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Sausage>(out Sausage sausage))
        {
            sausage.FinishReached();
        }

        if (collision.gameObject.TryGetComponent<SausageSegment>(out SausageSegment sausageSegment))
        {
            sausageSegment.sausageObject.GetComponent<Sausage>().FinishReached();
        }
    }
}
