using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    private GameObject rescueHandler;
    private GameObject gameHandler;

    private void Start() {
        rescueHandler = GameObject.FindWithTag("RescueHandler");
        gameHandler = GameObject.FindWithTag("GameHandler");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            score();
        }
    }

    void score()
    {
        RescueHandler rescueHandlerScript = rescueHandler.GetComponent<RescueHandler>();
        GameHandler gameHandlerScript = gameHandler.GetComponent<GameHandler>();
        gameHandlerScript.playerGetScore(rescueHandlerScript.victimInBoat);
        Debug.Log("get " + rescueHandlerScript.victimInBoat + " score!");
        rescueHandlerScript.victimInBoat = 0;
    }
}
