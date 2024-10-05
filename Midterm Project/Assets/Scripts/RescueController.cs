using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueController : MonoBehaviour
{
    public static int score = 0;
    public Vector3 direction = new Vector3(0, 0, 0);
    public Transform rescue;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.position - rescue.position;
        Debug.Log(direction);
    }


}
