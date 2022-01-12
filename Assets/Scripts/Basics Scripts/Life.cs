using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] int max_life;
    [SerializeField] bool is_player;
    [SerializeField] float timer_death;
    [SerializeField] List<GameObject> prefab_people;
    [SerializeField] List<Transform> spawn_points;
    [SerializeField] int max_people_spawn;
    [SerializeField] int min_people_spawn;
    [SerializeField] GameObject explosion_prefab;
    [SerializeField] GameObject ship;
    [SerializeField] GameObject canvas;
    int life;
    [SerializeField]float timer;
        
    [SerializeField] Slider bar_slider;
    [SerializeField] List<Sprite> ship_sprites_stats;
    SpriteRenderer sr;

    private void Start()
    {
        bar_slider.value = bar_slider.maxValue = life = max_life;
       
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        bar_slider.value = life;

        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                GameManager.instance.Get_End_Game = true;

            }
        }

        if (life >= (max_life - ((int)max_life / 4)))
        {
            sr.sprite = ship_sprites_stats[0];
        }
        else if (life >= max_life/2 && life < (max_life - ((int)max_life / 4)))
        {
            sr.sprite = ship_sprites_stats[1];
        }
        else
        {
            sr.sprite = ship_sprites_stats[2];
        }
      
    }

    public void Take_Damage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Dead(true);
        }
    }

    public void Dead(bool is_destroyed_for_player)
    {
        int random = Random.Range(min_people_spawn, max_people_spawn);

        ship.GetComponent<SpriteRenderer>().enabled = false;
        canvas.SetActive(false);

        for (int i = 0; i < random; i++)
        {
            int random_spawn_point = Random.Range(0, spawn_points.Count - 1);

            GameObject peoples = Instantiate(prefab_people[Random.Range(0, prefab_people.Count - 1)], spawn_points[random_spawn_point].position,spawn_points[random_spawn_point].rotation);

            int random_time = Random.Range(2, 6);

            Destroy(peoples, random_time);
        }

        Instantiate(explosion_prefab, transform.position,transform.rotation);

        if (is_player)
        {
            timer = timer_death;
            GameManager.instance.Player_Is_Not_Death = true;
        }
        else
        {

            if (is_destroyed_for_player)
            {
                GameManager.instance.Set_Enemy_Killed++;
            }

            Destroy(this.gameObject);

        }


    }

    public bool Get_is_player { get { return is_player; } }
}
