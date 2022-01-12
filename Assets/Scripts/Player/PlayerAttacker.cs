using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] int attack_damage_value;
    [SerializeField] Transform front_attack_spawner;
    [SerializeField] List<Transform> side_attack_spawner;
    [SerializeField] int timer_front_attack;
    [SerializeField] int timer_side_attack;
    [SerializeField] GameObject bullet_prefab;
    [SerializeField] int bullet_force;
    float timer1;
    float timer2;

    private void Update()
    {
        if (!GameManager.instance.Player_Is_Not_Death)
        {

            if (timer1 <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(1))
                {
                    Front_Attack();

                    timer1 = timer_front_attack;
                }
            }
            else
            {
                timer1 -= Time.deltaTime;
            }

            if (timer2 <= 0)
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(2))
                {
                    Side_Aattack();

                    timer2 = timer_side_attack;
                }
            }
            else
            {
                timer2 -= Time.deltaTime;
            }
        }
    }

    void Front_Attack()
    {
        GameObject bullet = Instantiate(bullet_prefab, front_attack_spawner.position,front_attack_spawner.rotation);

        Rigidbody2D rb_bullet = bullet.GetComponent<Rigidbody2D>();

        BulletController bullet_controller = bullet.GetComponent<BulletController>();

        rb_bullet.AddForce(front_attack_spawner.up * bullet_force, ForceMode2D.Impulse);

        bullet_controller.Get_bullet_damage = attack_damage_value;

        bullet_controller.Set_Tag_Bullet = "Player_Bullet";

        Destroy(bullet, 10);
    }
    void Side_Aattack()
    {
        for (int i = 0; i < side_attack_spawner.Count; i++)
        { 
            GameObject bullet =  Instantiate(bullet_prefab, side_attack_spawner[i].position, side_attack_spawner[i].rotation);

            Rigidbody2D rb_bullet = bullet.GetComponent<Rigidbody2D>();

            BulletController bullet_controller = bullet.GetComponent<BulletController>();

            rb_bullet.AddForce(side_attack_spawner[i].up * bullet_force, ForceMode2D.Impulse);

            bullet_controller.Get_bullet_damage = attack_damage_value;

            bullet_controller.Set_Tag_Bullet = "Player_Bullet";

            Destroy(bullet, 10);
        }
    }

    public int Get_Attack_Player_value { get { return attack_damage_value; } }
}
