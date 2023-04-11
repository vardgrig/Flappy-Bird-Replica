
using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 1;
    public RuntimeAnimatorController flyAnim;
    private State state;

    private int angle;
    private int minAngle = -90;
    private int maxAngle = 20;
    private enum State
    {
        WaitingToStart,
        Playing,
        Death
    }
    void Start()
    {
        state = State.WaitingToStart;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                rb.bodyType = RigidbodyType2D.Static;
                GameManager.instance.state = GameManager.State.Start;
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Animator>().runtimeAnimatorController = flyAnim;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                    state = State.Playing;
                    GameManager.instance.state = GameManager.State.Playing;
                }
                break;
            case State.Playing:
                GameManager.instance.state = GameManager.State.Playing;
                rb.bodyType = RigidbodyType2D.Dynamic;
                if (Input.GetMouseButtonDown(0))
                {
                    transform.rotation = Quaternion.Euler(0, 0, maxAngle);
                    Jump();
                }
                RotateBird();
                break;
            case State.Death:
                GameManager.instance.state = GameManager.State.Dead;
                transform.rotation = Quaternion.Euler(0, 0, minAngle);
                GetComponent<Animator>().enabled = false;
                GameManager.instance.Spawner.GetComponent<PipeSpawner>().state = PipeSpawner.State.GameOver;
                GameManager.instance.Ground.GetComponent<Animator>().enabled = false;
                break;
        }
    }
    private void RotateBird()
    {
        if (rb.velocity.y > 0 && angle <= maxAngle)
        {
            angle += 4;
        }
        else if (rb.velocity.y < -1.3f && angle >= minAngle)
        {
            angle -= 3;
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(rb.velocity.x, speed);
        Play("Wing");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (state != State.Death)
            {
                Play("Hit");
                state = State.Death;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (state != State.Death)
            {
                Play("Hit");
                StartCoroutine(PlayAfterDeath());
                state = State.Death;
            }

        }
        if (collision.CompareTag("ScoreTrigger"))
        {
            Play("Point");
            GameManager.score++;
        }
    }
    private void Play(string name)
    {
        AudioManager.instance.Play(name);
    }
    private IEnumerator PlayAfterDeath()
    {
        yield return new WaitForSeconds(0.3f);
        AudioManager.instance.Play("Die");
    }
}