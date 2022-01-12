using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA: MonoBehaviour
{
    [SerializeField] int attack_value_damage;
    [SerializeField] int speed;
    [SerializeField] bool is_ranged;
    [SerializeField] int timer_attack_ranged;
    [SerializeField] Transform fire_point;
    [SerializeField] GameObject bullet_prefab;
    [SerializeField] float distance_range_shoot;
    [SerializeField] int bullet_force;
    Vector2 movement;
    Rigidbody2D rb;
    float timer_attack;
    Transform player_transform;

    private void Start()
    {
        player_transform = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        float distance = Vector3.Distance(transform.position, player_transform.position);

        var direction = player_transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rb.rotation = angle;

        direction.Normalize();

        movement = direction;


        if (is_ranged && !GameManager.instance.Player_Is_Not_Death)
        {
            if (distance <= distance_range_shoot)
            {
                Enemy_Attack_Range();

            }
        }


       

    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.Player_Is_Not_Death)
        {
            rb.MovePosition(rb.position + (movement * speed * Time.deltaTime));
        }
    }

    void Enemy_Attack_Range()
    {
        if (timer_attack <= 0)
        {
            GameObject bullet = Instantiate(bullet_prefab, fire_point.position, fire_point.rotation);

            Rigidbody2D rb_bullet = bullet.GetComponent<Rigidbody2D>();

            BulletController bullet_controller = bullet.GetComponent<BulletController>();

            rb_bullet.AddForce(fire_point.up * bullet_force, ForceMode2D.Impulse);

            bullet_controller.Get_bullet_damage = attack_value_damage;

            bullet_controller.Set_Tag_Bullet = "Enemy_Bullet";

            timer_attack = timer_attack_ranged;

            Destroy(bullet, 10);
        }
        else
        {
            timer_attack -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!is_ranged && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Life>().Take_Damage(attack_value_damage);

            this.gameObject.GetComponent<Life>().Dead(false);

        }
    }
    public int Get_attack_damage { get { return attack_value_damage; } }
}
