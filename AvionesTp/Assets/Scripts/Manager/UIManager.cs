using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Text EnemiesDeadText;
    [SerializeField]
    private Text AltitudeText;
    [SerializeField]
    private Text HealthText;
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private GameObject GameOverText;
    [SerializeField]
    private GameObject Crosshair;
    [SerializeField]
    private GameObject Level;

    private SpaceShip PlayerShip;
    private int Fuel;
    private int EnemiesDead;
    private int Altitude;
    private int Health;
    private int Score;
    private bool SceneLoading;
    private float LoadingTimer;

    private const int LevelScreenTime = 2;

    private void Awake()
    {
        PlayerShip = Player.GetComponent<SpaceShip>();
        SceneLoading = true;
        LoadingTimer = 0;
    }

    private void Start()
    {
        GameManager.Instance.SetUIManager(this);
    }

    private void Update()
    {
      
        int ActualAltitude = (int)Player.transform.position.y;
        int ActualHealth = PlayerShip.GetHealth();
        int ActualEnemiesDead = GameManager.Instance.EnemiesDead;
        int ActualScore = GameManager.Instance.Score;

   
        if (ActualAltitude != Altitude)
        {
            Altitude = ActualAltitude;
            AltitudeText.text = "Altitude: " + Altitude;
        }
        if (ActualHealth != Health)
        {
            Health = ActualHealth;
            HealthText.text = "Health: " + Health;
        }
        if (ActualEnemiesDead != EnemiesDead)
        {
            EnemiesDead = ActualEnemiesDead;
            EnemiesDeadText.text = "Enemies Dead: " + EnemiesDead;
        }
        if (ActualScore != Score)
        {
            Score = ActualScore;
            ScoreText.text = "Score: " + Score;
        }

        if(!GameManager.Instance.InGame)
        {
            if(!GameOverText.activeSelf)
            {
                GameOverText.SetActive(true);
                Crosshair.SetActive(false);
            }
        }
        else
        {
            if (!Crosshair.activeSelf)
            {
                Crosshair.SetActive(true);
                GameOverText.SetActive(false);
            }
        }

        if(SceneLoading)
        {
            LoadingTimer += Time.deltaTime;
            if(LoadingTimer<LevelScreenTime)
            {
                Level.SetActive(true);
                Crosshair.SetActive(false);
            }
            else
            {
                Level.SetActive(false);
                Crosshair.SetActive(true);
                LoadingTimer = 0;
                SceneLoading = false;
            }
            
        }
    }

    public void SetSceneLoading(bool s)
    {
        SceneLoading = s;
    }
}
