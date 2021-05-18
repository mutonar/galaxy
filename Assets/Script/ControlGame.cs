using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlGame : MonoBehaviour
{
    public Text infoLifeUser; // Информация о пользователе
    public Text infoScoreUser; // Информация о пользователе его очках
    PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").gameObject.GetComponent<PlayerControl>(); // вытащим скрипт из игрока

        foreach (Transform child in GameObject.Find("Canvas").transform)
        {
            if (child.name.IndexOf("TextScore") >= 0) 
            {
                infoScoreUser = child.gameObject.GetComponent<Text>();
            }
            if (child.name.IndexOf("TextLife") >= 0)  
            {
                infoLifeUser = child.gameObject.GetComponent<Text>();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
       int infoLife = player.getLifePlayer();
       int infoScore = player.getScorePlayer();
       infoLifeUser.text = infoLife.ToString();
       infoScoreUser.text = infoScore.ToString();
       if(infoLife < 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex ); // предыдущая схема




    }
}
