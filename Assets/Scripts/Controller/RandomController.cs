using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class RandomMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float waitTime = 2.0f;

    private float nextWaitTime = 0.0f;

    private Rigidbody rb;
    Animator animator;
    Vector2 moveDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Time.time > nextWaitTime)
        {
            nextWaitTime = Time.time + waitTime + Random.Range(0.0f, waitTime);
            MoveRandomly();
        }
    }

    private void MoveRandomly()
    {
        float horizontal = Random.Range(-1.0f, 1.0f);
        float vertical = Random.Range(-1.0f, 1.0f);
        

        moveDirection = new Vector2(horizontal,vertical).normalized;
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);
        moveDirection = new Vector2(horizontal, vertical).normalized;
        rb.velocity = moveDirection * speed;
    }
}