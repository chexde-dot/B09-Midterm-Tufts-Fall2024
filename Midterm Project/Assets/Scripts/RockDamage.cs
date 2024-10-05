using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDamage : MonoBehaviour
{
    GameHandler gameHandlerObj;
    public int damage = 1;
    //public Transform backToStart; //uncomment this line for "auto-death," to zap the Player back to start

    void Start()
    {
        if (GameObject.FindWithTag("GameHandler") != null)
        {
            gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("damage script felt something");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("damage script felt player");
            gameHandlerObj.playerGetHit(damage);
            //other.transform.position = new Vector3(backToStart.position.x, backToStart.position.y, backToStart.position.z);
        }
    }
}
