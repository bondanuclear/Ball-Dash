using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float sideDistance = 2.96f;
    [SerializeField] float switchingSpeed = 4f;
    [SerializeField] int laneIndex = 2;
    [SerializeField] List<Transform> lanePoints = new List<Transform>();
    [SerializeField] bool canSwitchLane = true;
    [SerializeField] Vector3 destination = Vector3.zero;
    float velocity;
    public bool gameIsOver = false;
    public bool GameIsOver {get {return gameIsOver;}}
    new Rigidbody rigidbody;
    [Header("Distance to fall")]
    [SerializeField] float fallDistance = -2f;
    private void Awake() {
        rigidbody = GetComponentInChildren<Rigidbody>();
        destination = Vector3.zero;
        //GameObject lanePointsParent = GameObject.FindWithTag("LanePoints");
        // foreach(Transform point in lanePointsParent.transform)
        // {
        //     lanePoints.Add(point);
        // }
    }
    private void Update() {
        rigidbody.AddTorque(Vector3.right * 2f);
        
        if(!gameIsOver)
        {
            // transform.position = Vector3.Lerp(transform.position, lanePoints[laneIndex].position,
            //  Time.deltaTime * switchingSpeed);
            transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, destination.x, ref velocity, Time.deltaTime * switchingSpeed), 
            transform.position.y, transform.position.z);
        }
        if(rigidbody.transform.position.y < fallDistance)
        {
            gameIsOver = true;
        }
    }
    public void MoveLine(bool gointRight)
    {
        //laneIndex += (gointRight)? 1 : -1;
        destination += (gointRight)? (Vector3.right * sideDistance) : (Vector3.left * sideDistance);
    }
    
}
