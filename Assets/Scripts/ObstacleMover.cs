using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] float obstacleSpeed;
    [SerializeField] bool isMultiple;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * obstacleSpeed * Time.deltaTime);
        if(transform.position.z < -10)
        {
            if(gameObject.tag == "MultipleObstacle")
            {
                // Debug.Log(transform.parent.gameObject.name + "NAME");
                gameObject.SetActive(false);
                //transform.parent.position = new Vector3(0, 0.043f,0);
                PlayerManager.instance.multipleObjects.RemoveAt(PlayerManager.instance.multipleObjects.Count-1);
                if(PlayerManager.instance.multipleObjects.Count < 1)
                    PlayerManager.instance.showSlider = false;
            }
            else
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision other) {
        Debug.Log("touched");
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over!");
            other.transform.GetComponentInParent<PlayerMovement>().gameIsOver = true;
        }
    }
}
