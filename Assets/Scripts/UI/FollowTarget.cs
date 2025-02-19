using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
    }

    private void FixedUpdate()
    {
        transform.position=Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime);
    }
}
