using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    [SerializeField] List<Transform> spawn_points;
    [SerializeField] List<GameObject> enemys_prefabs;

    int time_spawn;
    float timer;

    private void Start()
    {
        time_spawn = PlayerPrefs.GetInt("spawn_time");


        if (time_spawn == 0)
        {
            time_spawn = 1;
        }

        timer = time_spawn;
    }

    private void Update()
    {
        if (!GameManager.instance.Player_Is_Not_Death)
        {
            if (timer <= 0)
            {
                int random_enemy = Random.Range(0, 10);
                int random_spawn = Random.Range(0, spawn_points.Count - 1);

                if (random_enemy >= 5)
                {
                    random_enemy = 1;
                }
                else
                {
                    random_enemy = 0;
                }

                Spawn(random_enemy, random_spawn);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void Spawn(int enemy_prefab_value_random, int spawn_point_value_random)
    {
        Instantiate(enemys_prefabs[enemy_prefab_value_random], spawn_points[spawn_point_value_random].position, spawn_points[spawn_point_value_random].rotation);

        timer = time_spawn;
    }
}
