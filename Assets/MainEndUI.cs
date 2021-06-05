using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class MainEndUI : MonoBehaviour
{
    private GameManager gameManager;
    public static MainEndUI Instance { get; set; }

    public Text Score;
    public bool isWin = false;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.MainEndUILoad();
    }

    public void End()
    {
        isWin = gameManager.isWin;
        if (isWin)
        {
            Score.text = "You and Your man survived!";
        }
        else Score.text = "You and Your man died!";
    }

    public void Update()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

    }
}

