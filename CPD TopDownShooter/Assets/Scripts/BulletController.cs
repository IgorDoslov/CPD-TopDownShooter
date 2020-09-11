using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damageToGive;


    void Update()
    {
        //Move bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
    }

    //If collision with enemy deal damage to enemy and destory bullet
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Giant" ||
            collision.gameObject.tag == "HealthZombie" || collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
            Destroy(gameObject);
        }
    }
}
