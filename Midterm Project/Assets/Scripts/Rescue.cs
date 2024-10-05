using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescue : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            score();
        }
    }

    void score()
    {
        Debug.Log("score!");
        Destroy(gameObject);
    }
}
