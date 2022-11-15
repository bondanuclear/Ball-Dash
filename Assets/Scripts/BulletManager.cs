using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] float speedOfBullet = 4f;
    [SerializeField] float distanceBeforeDisappearing = 40f;
    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * Time.deltaTime * speedOfBullet);
        
        if(transform.position.z >= distanceBeforeDisappearing)
        {
            //ObjectPool.instance.DisableObject(this.gameObject);
            this.gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("MultipleObstacle"))
        {
           // Debug.Log("Destroyed object: " + other.name);
            this.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
