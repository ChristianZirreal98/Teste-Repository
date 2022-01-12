using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text score_result_text;
    [SerializeField] TMP_Text score_text;
    [SerializeField] TMP_Text timer_gameplay_text;
    [SerializeField] GameObject new_highscore_text;
    [SerializeField] GameObject score_result_panel;
    [SerializeField] List<GameObject> bts_objcts;
    float timer_gameplay;
    
    [SerializeField] GameObject panel_pause;
    [SerializeField]int enemy_killed;
    bool score_calculate;
    int score_result;
    bool player_not_death;
    float seconds;
    bool end_game;
    [SerializeField]bool is_pause;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        timer_gameplay = PlayerPrefs.GetInt("time_gameplay");

        if (timer_gameplay == 0)
        {
            timer_gameplay = 1;
        }
        
        end_game = false;
    }
    private void Update()
    {
        if (!end_game)
        {
            if (!is_pause)
            {
                Time.timeScale = 1;                

                score_text.text = "Score : " + enemy_killed;

                if (!GameManager.instance.Player_Is_Not_Death)
                {
                    Timer();

                }

                if (seconds >= 10)
                {
                    timer_gameplay_text.text = " Time : " + timer_gameplay + " : " + seconds.ToString("0");
                }

                else if (seconds <= 9)
                {
                    timer_gameplay_text.text = " Time : " + timer_gameplay + " : " + "0" + seconds.ToString("0");
                }
            }
            else
            {
                Time.timeScale = 0;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                is_pause = is_pause == true ? false : true;
            }

            panel_pause.SetActive(is_pause);
        }
        else
        {
            score_result_panel.SetActive(true);

            if (!score_calculate)
            {
                Score_Result();
            }

            if (score_result_panel.activeInHierarchy)
            {
                score_result_text.text = " Score : " + score_result;
            }
        }
    }
    void Score_Result()
    {     

        for (int i = 0; i < enemy_killed; i++)
        {
            score_result ++;
        }

        for (int i = 0; i < bts_objcts.Count; i++)
        {
            bts_objcts[i].SetActive(true);
        }

        if (PlayerPrefs.GetInt("highscores") == 0)
        {
            new_highscore_text.SetActive(true);

            PlayerPrefs.SetInt("highscores", score_result);
        }
        else
        {
            if (PlayerPrefs.GetInt("highscores") <= score_result)
            {
                new_highscore_text.SetActive(true);

                PlayerPrefs.SetInt("highscores", score_result);
            }
            else
            {
                new_highscore_text.SetActive(false);
            }

        }

        score_calculate = true;
    }
    public void Resume_Pause(bool b)
    {
        is_pause = b;
    }
    public void End_Game()
    {
        end_game = true;
    }
    void Timer()
    {
        if (seconds <= 0)
        {
            if (timer_gameplay > 0)
            {
                timer_gameplay--;
                seconds = 59;
            }
            else
            {
                end_game = true;

               
            }
        }
        else
        {
            seconds -= Time.deltaTime;
        }

    }
    public void SceneLoad(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
        Time.timeScale = 1;
    }
    public bool Player_Is_Not_Death { get { return player_not_death; } set { player_not_death = value; } }
    public bool Get_End_Game { get { return end_game; }  set { end_game = value; } }
    public bool Get_is_pause { get { return is_pause; } }
    public int Set_Enemy_Killed { get { return enemy_killed;} set { enemy_killed = value; } }
}
