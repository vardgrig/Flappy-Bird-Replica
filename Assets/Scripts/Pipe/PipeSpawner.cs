using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float maxTime = 1;
    private float timer = 0;
    public GameObject pipe;
    public float height = 0.7f;
    public State state;
    public float minHeight = -0.65f;
    public float maxHeight = -0.65f;
    private float lastY;
    public enum State
    {
        Playing,
        GameOver
    }

    void Start()
    {
        state = State.Playing;
        CreatePipe();
        lastY = 0;
    }

    void Update()
    {
        switch (state)
        {
            case State.Playing:
                if (timer > maxTime)
                {
                    CreatePipes();
                    timer = 0;
                }
                timer += Time.deltaTime;
                break;
            case State.GameOver:
                CancelInvoke();
                for(int i = 0; i < transform.childCount; ++i)
                {
                    transform.GetChild(i).GetComponent<BackgroundRoller>().enabled = false;
                    transform.GetChild(i).GetComponentInChildren<BoxCollider2D>().enabled = false;
                }
                break;
        }
    }
    private void CreatePipe()
    {
        GameObject newPipe = Instantiate(pipe, transform);
        newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
    }
    private GameObject CreatePipes()
    {
        GameObject newPipe = Instantiate(pipe, transform);
        if (lastY+height > maxHeight)
            lastY = 0;
        newPipe.transform.position = transform.position + new Vector3(0, Random.Range(minHeight, lastY + height), 0);
        lastY = newPipe.transform.position.y;
        return newPipe;
    }
}
