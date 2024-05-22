using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;

    private void Update()
    {
        //Move projectile towards enemy if enemy still exists
        //if (target != null)
        //{
        //Vector3 directionToTarget = target.position - transform.position;
        //Vector3 directionToTarget = transform.position - Vector3.forward;
        //transform.position += directionToTarget.normalized * speed * Time.deltaTime;
        transform.position += transform.forward * Time.deltaTime * speed;
        //}

        Destroy(gameObject, 5f);



    }
}
