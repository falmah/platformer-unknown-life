using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public MonoBehaviour instance { get; set; }
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    public EnemyController(MonoBehaviour instance)
    {
        this.instance = instance;
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
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }



}
