using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
public class TraceDrawer : MonoBehaviour
{
    [SerializeField] private GameObject tracingObject;
    [SerializeField] private GameObject staticLevelObjectsRoot;
    private GameObject simulatingObject;
    private Rigidbody tracingObjectRigidbody;

    private Scene currentScene;
    private PhysicsScene currentPhysicsScene;
    private Scene predictionScene;
    private PhysicsScene predictionPhysicsScene;
    private LineRenderer traceLine;

    void Awake()
    {
        traceLine = GetComponent<LineRenderer>();
        tracingObjectRigidbody = tracingObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("predictionScene", parameters);
        predictionPhysicsScene = predictionScene.GetPhysicsScene();
        Physics.autoSimulation = false;

        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene();

        ClearTrace();

        /////Enable if need to predict additional traectory 
        /*
        foreach(Transform child in staticLevelObjectsRoot.transform)
        {
            GameObject staticLevelObject = Instantiate(child.gameObject);
            SceneManager.MoveGameObjectToScene(staticLevelObject, predictionScene);
            staticLevelObject.transform.position = child.position;
        }
        */
    }

    //Don't remove this
    void FixedUpdate()
    {
        if (currentPhysicsScene.IsValid())
        {
            currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }
    
    public void MakePrediction(Vector3 direction, float force, int maxIterations, float stepKoef)
    {
        if (simulatingObject == null)
        {
            simulatingObject = Instantiate(tracingObject);
            SceneManager.MoveGameObjectToScene(simulatingObject, predictionScene);
        }

        simulatingObject.transform.position = tracingObject.transform.position;
        simulatingObject.GetComponent<Rigidbody>().velocity = tracingObjectRigidbody.velocity;
        Sausage sausageComponent = simulatingObject.GetComponent<Sausage>();
        sausageComponent.chargingAllowed = true;
        sausageComponent.ChargeSausage(direction, force);
        
        traceLine.positionCount = maxIterations;
        for (int i = 0; i < maxIterations; i++)
        {
            predictionPhysicsScene.Simulate(Time.fixedDeltaTime * stepKoef);
            traceLine.SetPosition(i, simulatingObject.transform.position);
        }

        Destroy(simulatingObject);
    }

    public void ClearTrace()
    {
        traceLine.positionCount = 0;
    }
}
