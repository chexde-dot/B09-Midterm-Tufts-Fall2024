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
    public List<Vector3> positions = new List<Vector3>();

    private int spawnNumber = 0;

    void Start()
    {

    }

    void Update()
    {
        if (spawnNumber < positions.Count)
        {
            generateVictim();
        }
    }

    void generateVictim()
    {
        GameObject newVictim = Instantiate(victim);
        float randomX = positions[spawnNumber].x;
        float randomZ = positions[spawnNumber].z;
        newVictim.transform.localPosition = new Vector3(randomX, -0.5f, randomZ);
        victimCount++;
        spawnNumber++;
    }
}
