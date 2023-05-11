using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{

    public enum ZombieGoal
    {
        PLAYER,
        FENCE
    }

    public enum GameGoal
    {
        KILL_ZOMBIES,
        WALK_TO_GOAL_STEPS,
        DEFEND_FENCE,
        TIMER_COUNTDOWN,
        GAME_OVER
    }






    public static GameplayController instance;

    [HideInInspector]
    public bool bullet_And_BulletFX_Created, rocket_Bullet_Created;


    [HideInInspector]
    public bool playerAlive, fenceDestroyed;

    public ZombieGoal zombieGoal = ZombieGoal.PLAYER;
    public GameGoal gameGoal = GameGoal.DEFEND_FENCE;

    public int zombie_Count = 20;
    public int timer_Count = 100;


    public int step_Count = 100;
    private int initial_Step_Count;


    private Text zombie_Counter_Text, Timer_Text, stepCounter_Text;
    private Image playerLife;

    [HideInInspector]
    public int coinCount;



    private Transform playerTarget;
    private Vector3 player_Previous_Position;

    public GameObject pausePanel, gameOverPanel, gameFailPanel;
    Text setCoins;

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        coinCount = 0;
        setCoins = GameObject.Find("ReceivedBonuses").GetComponent<Text>();
        playerAlive = true;

        if (gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            playerTarget = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
            player_Previous_Position = playerTarget.position;
            initial_Step_Count = step_Count;
            stepCounter_Text = GameObject.Find("Step Counter").GetComponent<Text>();
            stepCounter_Text.text = step_Count.ToString();
        }

        if (gameGoal == GameGoal.TIMER_COUNTDOWN || gameGoal == GameGoal.DEFEND_FENCE)
        {
            Timer_Text = GameObject.Find("Timer Counter").GetComponent<Text>();
            Timer_Text.text = timer_Count.ToString();
            InvokeRepeating("TimerCountdown", 0f, 1f);
        }


        if (gameGoal == GameGoal.KILL_ZOMBIES)
        {
            zombie_Counter_Text = GameObject.Find("Zombie Counter").GetComponent<Text>();
            zombie_Counter_Text.text = zombie_Count.ToString();
        }
        playerLife = GameObject.Find("Life Full").GetComponent<Image>();
    }


    void OnDisable()
    {
        instance = null;
    }

    void Update()
    {
        if (gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            CountPlayerMovement();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGameFunction();
        }
        UpdateCoins();
    }

    void UpdateCoins()
    {
        setCoins.text = coinCount.ToString();
    }

    void CountPlayerMovement()
    {
        Vector3 playerCurrentMovement = playerTarget.position;
        float dist = Vector3.Distance(new Vector3(playerCurrentMovement.x, 0f, 0f),
                                      new Vector3(player_Previous_Position.x, 0f, 0f));

        //  player moving forward
        if (playerCurrentMovement.x > player_Previous_Position.x)
        {
            if (dist > 1)
            {
                step_Count--;

                if (step_Count <= 0)
                {
                    GameOver();
                }
                player_Previous_Position = playerTarget.position;
            }

            // player moving backwards
        }
        else if (playerCurrentMovement.x < player_Previous_Position.x)
        {
            if (dist > 0.8)
            {
                step_Count++;

                if (step_Count >= initial_Step_Count)
                {
                    step_Count = initial_Step_Count;
                }

                player_Previous_Position = playerTarget.position;
            }
        }

        stepCounter_Text.text = step_Count.ToString();
    }


    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void TimerCountdown()
    {
        timer_Count--;
        Timer_Text.text = timer_Count.ToString();

        if (timer_Count <= 0)
        {
            CancelInvoke("TimerCountdown");
            GameOver();
        }
    }


    public void ZombieDied()
    {
        if (GameplayController.instance.gameGoal == GameGoal.KILL_ZOMBIES)
        {
            zombie_Count--;
            zombie_Counter_Text.text = zombie_Count.ToString();
        }

        if (zombie_Count <= 0)
        {
            GameOver();
        }
    }

    public void PlayerLifeCounter(float fillPercentage)
    {
        fillPercentage /= 100f;

        playerLife.fillAmount = fillPercentage;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PauseGameFunction()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PlayerDie()
    {
        gameFailPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TagManager.MAIN_MENU_NAME);
    }




}
