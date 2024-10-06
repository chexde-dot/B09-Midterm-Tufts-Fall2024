using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueHandler : MonoBehaviour
{
    public GameObject victim;
    public int maxVictimCount = 1;
    public int boatCapacity = 10;
    public int victimCount = 0; // the number of victim in the ocean
    public int victimInBoat = 0;    // the number of victim in the boat

    void Start()
    {

    }

    void Update()
    {
        if (victimCount < maxVictimCount)
        {
            generateVictim();
        }
    }

    void generateVictim()
    {
        GameObject newVictim = Instantiate(victim);
        float randomX = Random.Range(-10f, 10f);
        float randomZ = Random.Range(-10f, 10f);
        newVictim.transform.localPosition = new Vector3(randomX, -0.5f, randomZ);
        victimCount++;
    }
}
