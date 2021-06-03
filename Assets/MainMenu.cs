using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void PlayGame()
    {
        DestroyInstances();
        SceneManager.LoadSceneAsync("GameScene");

        List<Player> playersList = new List<Player>();

        for (int i = 0; i < 3; i++)
        {
            TMP_InputField nick = GetInput(i);

            if (nick.text.Length != 0)
            {
                if (i == 0)
                {
                    nick.text = "King " + nick.text;
                    playersList.Add(new Player(i, nick.text, PlayerType.King));
                }
                if (i == 1)
                {
                    nick.text = "Builder " + nick.text;
                    playersList.Add(new Player(i, nick.text, PlayerType.Builder));
                }
                if (i == 2)
                {
                    nick.text = "Worker " + nick.text;
                    playersList.Add(new Player(i, nick.text, PlayerType.Worker));
                }
            }
            else return;
        }

        gameManager.startGame(playersList);
    }

    private static TMP_InputField GetInput(int __i)
    {
        TMP_InputField nick = null;
        if (__i == 0)
        {
            nick = GameObject.Find("/MainMenu/NewGame/King/InputField King").GetComponent<TMP_InputField>();
        }
        if (__i == 1)
        {
            nick = GameObject.Find("/MainMenu/NewGame/Builder/InputField Builder").GetComponent<TMP_InputField>();
        }
        if (__i == 2)
        {
            nick = GameObject.Find("/MainMenu/NewGame/Worker/InputField Worker").GetComponent<TMP_InputField>();
        }
        return nick;
    }

    private static void DestroyInstances()
    {
        if (MainGameUI.Instance != null)
        {
            GameObject MainGameUI = GameObject.Find("MainGameUI");
            Destroy(MainGameUI);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
