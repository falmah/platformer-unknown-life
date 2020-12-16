using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{

    private MovementController movement;
    public MonoBehaviour instance { get; set; }
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    public int leftcount;
    private int curentLeftCount = 0;
    private bool moveBack = false;


    public EnemyController(MonoBehaviour instance)
    {
        this.instance = instance;
    }

    float moveEnemy()
    {
        if (!moveBack)
        {
            if (curentLeftCount == leftcount)
                moveBack = true;

            if (curentLeftCount < leftcount)
            {
                ++curentLeftCount;
                return -1;
            }
                
        }
        else 
        {
            if (curentLeftCount == 0)
                moveBack = false;

            if (curentLeftCount > 0)
            {
                --curentLeftCount;
                return 1;
            }    
        }
        

        return 0;
    }

    public void KillHero(Collider2D other)
    {

            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.SetActive(false);
                var life = other.gameObject.GetComponent<LifeManagement>();
                life.lifeCount = 0;
            }
        
    }

    void Update()
    {
        movement.MovementFSM();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        movement = GetComponent<MovementController>();
        movement.instance = this;
        movement.directionCallback = moveEnemy;
    }



}
