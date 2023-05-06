using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //public GameObject enemy;

    public Transform shootPoint;

    public Rigidbody2D bulletPrefab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get mouse click position
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit2D.collider != null)
            {
                
                Debug.Log("Hit Point = " + hit2D.point);
                
                //fire bullet using project velocity
                Vector2 projectilevelocity = CalculateProjectileVelocity(shootPoint.position, hit2D.point, 1f);
                Rigidbody2D fire = Instantiate(bulletPrefab, shootPoint.position, quaternion.identity);
                fire.velocity = projectilevelocity;

            }
            
        }
        
    }// end update

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float t)
    {
        //find distance between 2 points
        Vector2 distance = target - origin;
        
        //find X
        Vector2 distanceX = distance;
        distanceX.y = 0f;
        float X = distanceX.magnitude;
        
        //find Y
        float Y = distance.y;
        
        //find velocity = Vx, Vy
        float velocityX = X / t;
        float velocityY = Y / t + 0.5f * Mathf.Abs(Physics2D.gravity.y) * t;
        
        //merge velocity (Vx, Vy) with direction
        Vector2 result = distance.normalized;
        result *= velocityX;
        result.y = velocityY;
        
        return result;


    }
}
