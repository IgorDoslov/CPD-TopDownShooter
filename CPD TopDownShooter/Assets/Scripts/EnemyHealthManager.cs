using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyHealthManager : MonoBehaviour
{
    public int health;
    public int currentHealth;
    public int scoreValue = 1;
    public GameObject ItemToDrop;
    public GameObject blood;
    public GameObject blood2;
    public GameObject hitBlood;
   // public GameObject alterColourOfThis;

   // public float flashLength;
    private float flashCounter;

    private Renderer rend;
    private Color storedColor;

    void Start()
    {
        //set initial health
        currentHealth = health;
        //rend = GetComponent<Renderer>();
       // alterColourOfThis.GetComponent<Renderer>();
        //storedColor = rend.material.GetColor("_Color");
       // storedColor = alterColourOfThis.material.GetColor("_Color");

    }

    void Update()
    {
      //  if(flashCounter > 0)
      //  {
         //   flashCounter -= Time.deltaTime;
         //   if(flashCounter <= 0)
         //   {
                // rend.material.SetColor("_Color", storedColor);
               // alterColourOfThis.material.SetColor("_Color", storedColor);

            //}
       // }

        //destory object if health <0
        if (currentHealth <= 0)
        {
            if (gameObject.tag == "HealthZombie" || gameObject.tag == "Boss")
            {
                var obj = GameObject.Instantiate(ItemToDrop);
                obj.transform.position = transform.position;           
            }
            Instantiate(blood, transform.position, Quaternion.identity);
            Instantiate(blood2, transform.position, Quaternion.identity);
            ScoreManager.score += scoreValue;
            Destroy(gameObject);
        }
    }

    //damage Enemy Object
    public void HurtEnemy(int damage)
    {
        Instantiate(hitBlood, transform.position, Quaternion.identity);
        currentHealth -= damage;
       // flashCounter = flashLength;
        // rend.material.SetColor("_Color", Color.white);
       // alterColourOfThis.material.SetColor("_Color", Color.white);
        
    }
}
