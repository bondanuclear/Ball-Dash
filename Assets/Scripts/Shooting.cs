using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] float coolDown = 5f;
    [SerializeField] bool canShoot = true;

    [SerializeField] Transform shootingPlace = null;
    ObjectPool bullets;
    private void Awake() {
        bullets = GameObject.FindGameObjectWithTag("Bullets").GetComponent<ObjectPool>();
    }
    public void Shoot()
    {
        if(!canShoot) return;
        //GameObject bullet = ObjectPool.instance.GetObjectFromPool();
        
        GameObject bullet = bullets.GetObjectFromPool();
        bullet.transform.position = shootingPlace.position;
        bullet.SetActive(true);
        StartCoroutine(StartCooldown());
    }
    private IEnumerator StartCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }
    
}
