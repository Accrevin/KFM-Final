using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureDistance : MonoBehaviour
{
    [SerializeField] GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_target.transform.position, transform.position);
        //Debug.Log($"Distance: {distance}");
        if (distance <= 25.0f)
        {
            Debug.Log("Hot");
        }

        else if (distance > 25.0f && distance <= 50.0f)
        {
            Debug.Log("Warm");
        }

        else
        {
            Debug.Log("Cold");
        }
    }
}
