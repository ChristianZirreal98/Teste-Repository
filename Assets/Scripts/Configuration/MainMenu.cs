using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Panel_loading;
    [SerializeField] TMP_Text text_time_gameplay;
    [SerializeField] TMP_Text text_spawm_time;
    [SerializeField] int Max_seconds_value;
    [SerializeField] int time_loading;
    int time_gameplay;
    int spawn_time ;
    [SerializeField]float timer;

    private void Start()
    {
        Time.timeScale = 1;

        time_gameplay = PlayerPrefs.GetInt("time_gameplay");

        spawn_time = PlayerPrefs.GetInt("spawn_time");

        if (spawn_time <= 0)
        {
            spawn_time = 1;

            PlayerPrefs.SetInt("spawn_time", spawn_time);
        }
        if (time_gameplay <= 0)
        {
            time_gameplay = 1;


            PlayerPrefs.SetInt("time_gameplay", time_gameplay);

        }
    }
    private void Update()
    {
        text_spawm_time.text = spawn_time + " seconds ";
        text_time_gameplay.text = time_gameplay + " minutes ";


        if (timer > 0)
        {
            Panel_loading.SetActive(true);
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void Play()
    {
     
        timer = time_loading;
    }

    public void Spawn_Time_New_Value(int value)
    {
        spawn_time += value;

        if (spawn_time > Max_seconds_value)
        {
            spawn_time = 1;
        }
        else if (spawn_time <= 0)
        {
            spawn_time = Max_seconds_value;
        }

        PlayerPrefs.SetInt("spawn_time",spawn_time);

    }

    public void Time_Gameplay_New_Value(int value)
    {
        time_gameplay += value;

        if (time_gameplay > 3)
        {
            time_gameplay = 1;
        }
        else if (time_gameplay <= 0 )
        {
            time_gameplay = 3;
        }

        PlayerPrefs.SetInt("time_gameplay", time_gameplay);
    }
}
