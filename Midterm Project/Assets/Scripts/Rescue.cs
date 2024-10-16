using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescue : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float height = 1f;
    public float respawnTime = 10f;

    private float upTime;
    private float downTime;
    private float maxY;
    private float startY;
    private float timer;
    private bool movingUp = true;
    private GameObject rescueHandler;
    private GameObject gameHandler;
    private GameObject rescueChild;
    private MeshRenderer rescueMeshRenderer;
    private BoxCollider boxCollider;

    private void Start() {
        rescueHandler = GameObject.FindWithTag("RescueHandler");
        gameHandler = GameObject.FindWithTag("GameHandler");
        startY = transform.position.y;
        maxY = startY + height;
        upTime = height / moveSpeed;
        downTime = height / moveSpeed;

        rescueChild = transform.Find("rescue1").gameObject;
        if (rescueChild != null)
        {
            rescueMeshRenderer = rescueChild.GetComponent<MeshRenderer>();
        }
        boxCollider = GetComponent<BoxCollider>();
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

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickVictim();
        }
    }

    void pickVictim()
    {
        RescueHandler rescueHandlerScript = rescueHandler.GetComponent<RescueHandler>();
        if (rescueHandlerScript.victimInBoat + 1 > rescueHandlerScript.boatCapacity) {
            return;
        }
        Debug.Log("pickVictim!");
        rescueHandlerScript.victimInBoat++;
        rescueHandlerScript.victimCount--;
        GameHandler gameHandlerScript = gameHandler.GetComponent<GameHandler>();
        gameHandlerScript.playerGetVictims(1);
        StartCoroutine(DisappearCoroutine());
    }

    private IEnumerator DisappearCoroutine()
    {
        if (rescueMeshRenderer != null)
            rescueMeshRenderer.enabled = false;
        if (boxCollider != null)
            boxCollider.enabled = false;
        yield return new WaitForSeconds(respawnTime);
        if (rescueMeshRenderer != null)
            rescueMeshRenderer.enabled = true;
        if (boxCollider != null)
            boxCollider.enabled = true;
    }
}
