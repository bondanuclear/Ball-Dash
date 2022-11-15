using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // in order to optimize the game, we have several different object pools, so
    // we'll have to refrain from using singleton pattern 
    //public static ObjectPool instance;
    [SerializeField] int poolSize = 10;
    [SerializeField] List<GameObject> pool = new List<GameObject>();

    [SerializeField] GameObject objectToSpawn = null;
    [SerializeField] GameObject[] objectsToSpawn = null;
    [Header("Pool for single object or multiple different")]
    // pool of single object or multiple different
    [SerializeField] bool singleObject = true;
    [SerializeField] int numberOfCopies = 4;
    private void Awake() {
    }
    // if we have different objects, create 4 of each object
    private void Start() {
        if(singleObject)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject spawn = Instantiate(objectToSpawn, transform);
                pool.Add(spawn);
                spawn.SetActive(false);
            }
        }
        else if(!singleObject)
        {
            for (int i = 0; i < objectsToSpawn.Length; i++)
            {
                for (int j = 0; j < numberOfCopies; j++)
                {
                    GameObject spawn = Instantiate(objectsToSpawn[i], transform);
                    spawn.SetActive(false);
                }
                pool.Add(objectsToSpawn[i]);
            }
        }
    }
    
    public GameObject GetObjectFromPool()
    {
        foreach(GameObject item in pool)
        {
            if(item.activeSelf == false)
            {
                Debug.Log("ITEM " + item.name);
                return item;
            }
        }
        return null;
    }
    public GameObject GetRandomObjectFromPool(int index)
    {
        GameObject randomObject = transform.GetChild(index).gameObject;
        if(randomObject.activeSelf == false) return randomObject;
        else
        {
            return null;
        }

    }
    public void DisableObject(GameObject objectToHide)
    {
        objectToHide.SetActive(false);
        //objectToHide.transform.position = transform.position;
    }
}
