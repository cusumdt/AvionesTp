using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private Transform[] SpawnPoint;

    private static GameManager instance;
    private int enemiesDead;
    private int score;
    private int LevelEnemiesDead;
    private bool inGame;
    private float GameOTimer;

    private UIManager UI;

    private const int Level1Enemies = 2;
    private const int Level2Enemies = 3;
    private const int Level3Enemies = 4;
    private const float TimeInGO=5;
    private const int AddScore = 1000;

    public bool SceneLoading;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        enemiesDead = 0;
        inGame = true;
        SceneLoading = true;
        GameOTimer = 0;
        instance = this;
        DontDestroyOnLoad(gameObject);
       
    }

    private void Update()
    {
        if(!InGame)
        {
            GameOver();
        }
        else
        {
            if(SceneManager.GetActiveScene().name!="Menu" && SceneManager.GetActiveScene().name != "EndGame")
            {
                Cursor.lockState=CursorLockMode.Confined;
                Cursor.visible = false;
            }
            if (SceneLoading)
            {
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Level1":

                        UI.SetSceneLoading(true);
                        for (int i = 0; i < Level1Enemies; i++)
                        {
                            Instantiate(EnemyPrefab, SpawnPoint[i].position, Quaternion.identity);
                            LevelEnemiesDead = 0;
                        }
                        SceneLoading = false;

                    break;
                    case "Level2":

                        UI.SetSceneLoading(true);
                        for (int i = 0; i < Level2Enemies; i++)
                        {
                            Instantiate(EnemyPrefab, SpawnPoint[i].position, Quaternion.identity);
                            LevelEnemiesDead = 0;
                        }
                        SceneLoading = false;
                        break;

                    case "Level3":

                        UI.SetSceneLoading(true);
                        for (int i = 0; i < Level3Enemies; i++)
                        {
                            Instantiate(EnemyPrefab, SpawnPoint[i].position, Quaternion.identity);
                            LevelEnemiesDead = 0;
                        }
                        SceneLoading = false;

                    break;
                }
            }
            else
            {
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Level1":
                        if (LevelEnemiesDead == Level1Enemies)
                        {
                            SceneManager.LoadScene("Level2");
                            SceneLoading = true;
                        }
                        break;
                    case "Level2":
                        if (LevelEnemiesDead == Level2Enemies)
                        {
                            SceneManager.LoadScene("Level3");
                            SceneLoading = true;
                        }
                        break;

                    case "Level3":
                        if (LevelEnemiesDead == Level3Enemies)
                        {
                            SceneManager.LoadScene("EndGame");
                            SceneLoading = true;
                        }
                        break;
                }
            }
        }
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    public void GameOver()
    {
        inGame = false;
        GameOTimer += Time.deltaTime;
        if(GameOTimer>TimeInGO)
        {
            inGame = true;
            GameOTimer = 0;
            SceneManager.LoadScene("EndGame");
        }
    }

    public void AddEnemyDead()
    {
        enemiesDead++;
        LevelEnemiesDead++;
        score += AddScore;
    }

    public int EnemiesDead
    {
        get { return enemiesDead; }
        set { enemiesDead = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public bool InGame
    {
        get { return inGame; }
    }

    public void SetUIManager(UIManager ui)
    {
        UI = ui;
    }
}
