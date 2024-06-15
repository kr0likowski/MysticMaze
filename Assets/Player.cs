using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // Handle player death
            Debug.Log("Player is dead!");
        }
        Debug.Log("Player Health: " + currentHealth);
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
