using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject progressControllerObject;
    private ProgressController progressController;
    private void Awake()
    {
        progressController = progressControllerObject.GetComponent<ProgressController>();
    }

    public void OnClick()
    {
        progressController.Restart();
    }
}
