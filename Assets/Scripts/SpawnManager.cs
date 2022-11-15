using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] lanePoints = new GameObject[3];
    [SerializeField] GameObject[] obstacles;
    [SerializeField] GameObject[] multipleObstacles;
    [SerializeField] float intervalBetweenSpawn = 2.3f;
    float timer = 0;
    float subtrahend = 0.02f;
    ObjectPool obstaclePool;
    ObjectPool multipleObstaclePool;
    // Start is called before the first frame update
    void Awake()
    {
        obstaclePool = GameObject.FindGameObjectWithTag("ObsPool").GetComponent<ObjectPool>();
        multipleObstaclePool = GameObject.FindGameObjectWithTag("MultObsPool").GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > intervalBetweenSpawn)
        {
            // spawn an object on random lane
            int laneIndex = Random.Range(0, lanePoints.Length+1);
           
            Debug.Log("LANE INDEX " + laneIndex);
            // decrease interval time
            DecreaseIntervalTime();
            // if lane index is more than lane points array, than spawn random 
            //multiple obstacle right in  the centre
            if(laneIndex == lanePoints.Length)
            {
                Debug.Log("Spawning MULTIPLE");
                //int obstacleIndex = Random.Range(0, multipleObstaclePool.transform.childCount);
                GameObject obstacle = multipleObstaclePool.GetObjectFromPool();
                Debug.Log("MULTIPLE OBSTACLE NAME " + obstacle.name);
                obstacle.SetActive(true);
                obstacle.transform.position = new Vector3(lanePoints[1].transform.position.x, 
                    obstacle.transform.position.y, lanePoints[1].transform.position.z);
                obstacle.transform.rotation = Quaternion.identity;
               
                
                PlayerManager.instance.multipleObjects.Add(1);
                if(!PlayerManager.instance.showSlider) 
                {
                    //Debug.Log("IM WORKING");
                    PlayerManager.instance.showSlider = true; 
                }
            }
            else
            {
                int obstacleIndex = Random.Range(0, obstaclePool.transform.childCount);
                Debug.Log("Spawning simple obstacle");
                
                GameObject randomObstacle = obstaclePool.GetRandomObjectFromPool(obstacleIndex);
                if(randomObstacle == null) return;
                
                randomObstacle.transform.position = new Vector3(lanePoints[laneIndex].transform.position.x,
                randomObstacle.transform.position.y,lanePoints[laneIndex].transform.position.z );
                randomObstacle.transform.rotation = Quaternion.identity;
                randomObstacle.SetActive(true);
                
            }
           
            
            timer = 0;
        }
    }

    private void DecreaseIntervalTime()
    {
        if (intervalBetweenSpawn == 1) return;
        
        intervalBetweenSpawn -= subtrahend;
        intervalBetweenSpawn = Mathf.Clamp(intervalBetweenSpawn, 1, 2.3f);
        //Debug.Log($"NEW INTERVAL TIME {intervalBetweenSpawn}");
    }
}
