using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public GameObject pointA;
    public GameObject pointB;
   [SerializeField] private Rigidbody2D rb;
   private Transform currentPoint;
   public float speed;

    private void Start()
    {
        currentPoint = pointB.transform;
    }

    private void Update()
    {
        
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position,currentPoint.position)< 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position,currentPoint.position)< 0.5f && currentPoint ==pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position,0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position,0.5f);
        Gizmos.DrawLine(pointA.transform.position,pointB.transform.position);
    }

    private void OnDestroy()
    {
        Destroy(enemyPrefab);
    }
}
