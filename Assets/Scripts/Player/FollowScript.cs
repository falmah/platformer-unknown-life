using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    public float delay;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = player.position + offset;
        Vector3 delayPos = Vector3.Lerp(transform.position, targetPos, delay * Time.deltaTime);
        transform.position = delayPos;
    }
}
