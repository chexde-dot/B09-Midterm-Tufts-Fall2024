using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueHandler : MonoBehaviour
{
    public GameObject prefab;
    public Transform container;
    public float moveSpeed = 2f;
    public float height = 1f;

    private float upTime;
    private float downTime;
    private float maxY;
    private float startY;
    private float timer;
    private bool movingUp = true;

    void Start()
    {
        if (container.childCount == 0)
        {
            generateRescue();
        }
        startY = transform.position.y;
        maxY = startY + height;
        upTime = height / moveSpeed;
        downTime = height / moveSpeed;
    }

    void Update()
    {
        if (container.childCount == 0)
        {
            generateRescue();
        }
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (movingUp)
        {
            float newY = Mathf.Lerp(startY, maxY, timer / upTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (timer >= upTime)
            {
                timer = 0;
                movingUp = false;
            }
        }
        else
        {
            float newY = Mathf.Lerp(maxY, startY, timer / downTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if (timer >= downTime)
            {
                timer = 0;
                movingUp = true;
            }
        }
    }

    void generateRescue()
    {
        GameObject newPrefab = Instantiate(prefab, container);
        newPrefab.transform.localPosition = new Vector3(0f, 3.20523f, 2.661115f);

        float randomX = Random.Range(-10f, 10f);
        float randomZ = Random.Range(-10f, 10f);
        container.position = new Vector3(randomX, container.position.y, randomZ);
    }
}
