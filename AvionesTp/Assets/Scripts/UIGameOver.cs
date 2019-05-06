using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Text EnemiesDeadText;

    private int FinalScore;
    private int FinalEnemiesDead;
    private GameManager GM;

    void Awake()
    {
        if (!GameManager.Instance)
        {
            GM = new GameManager();
        }
        else
        {
            GM = GameManager.Instance;
        }
        FinalScore = GM.Score;
        FinalEnemiesDead = GM.EnemiesDead;
    }

    private void OnEnable()
    {
        if (ScoreText)
        { 
            ScoreText.text = "Score: " + FinalScore.ToString();
        }
        if (EnemiesDeadText)
        { 
            EnemiesDeadText.text = "Enemies Dead: " + FinalEnemiesDead;
        }
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
