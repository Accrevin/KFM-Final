using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth; 
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void TakeDamage(int damage)
    {
        if (health > 0) 
        {
            health -= damage;
            Debug.Log($"Current damage: {health}");
        }
    }
}
