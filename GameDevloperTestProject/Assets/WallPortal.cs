using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPortal : MonoBehaviour
{
    public Transform roomCenter;
    public Vector2 warpPoint;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Warp(other.transform);
        }
    }

    private void Warp(Transform playerTransform)
    {
        Camera.main.transform.position = new Vector3(roomCenter.position.x, roomCenter.position.y, Camera.main.transform.position.z);
        playerTransform.position = warpPoint;
    }
}
