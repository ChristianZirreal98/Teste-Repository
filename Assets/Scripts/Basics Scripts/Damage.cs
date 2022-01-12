using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] string tag_bullet_enemy;
    [SerializeField] string tag_ship_enemy;
    [SerializeField]Life life;
    [SerializeField] GameObject explosion_prefab;
    float timer_damage;
    GameObject objct_collision;
    private void Start()
    {
        life = GetComponent<Life>();
    }

    private void Update()
    {
        if (!GameManager.instance.Player_Is_Not_Death)
        {
            if (objct_collision != null && timer_damage <= 0)
            {
                Get_Damage(objct_collision);


            }
            else if (timer_damage > 0)
            {
                timer_damage -= Time.deltaTime;
            }
        }
    }

    void Get_Damage(GameObject objct)
    {
        if (objct.CompareTag(tag_ship_enemy))
        {
            if (life.Get_is_player)
            {
                life.Take_Damage(objct.GetComponent<EnemyIA>().Get_attack_damage);
            }
            else
            {
                life.Take_Damage(objct.GetComponent<PlayerAttacker>().Get_Attack_Player_value);
            }

            timer_damage = 1;
        }
        else if (objct.CompareTag(tag_bullet_enemy))
        {
            life.Take_Damage(objct.GetComponent<BulletController>().Get_bullet_damage);

            Instantiate(explosion_prefab, objct.transform.position,transform.rotation);

            Destroy(objct);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        objct_collision = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        objct_collision = null;
    }
}
