using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private Button Button;

    void Awake()
    {
        if (Button == null)
        { 
            Button = GetComponent<Button>();
        }
        Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if(GameManager.Instance)
        {
            GameManager.Instance.Score = 0;
            GameManager.Instance.EnemiesDead = 0;
            GameManager.Instance.SceneLoading = true;
        }
        SceneManager.LoadScene("Level1");
    }
}
