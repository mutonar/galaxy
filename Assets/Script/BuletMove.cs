using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuletMove : MonoBehaviour
{
    float speedBulet = 20.8f;
    public bool stop;
    float timedest = 15.0f; // время удвления
    float timeLife; // текучее время жизни
    float timeStart;
    PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").gameObject.GetComponent<PlayerControl>(); // вытащим скрипт из игрока(не жрет ли это ресурсы)
        timeStart = Time.time;
        StartCoroutine(moveBulet(0.03f)); // Запуск каротина (летят )
    }

    // Update is called once per frame
    void Update()
    {

    }



    IEnumerator moveBulet(float waitTime)
    {
        while (!stop)
        {

            transform.position += transform.right * speedBulet * Time.deltaTime;

            if (Time.time > timeStart + timedest) Destroy(this.gameObject); // уничтжаем после времени timedest

            yield return new WaitForSeconds(waitTime); //  каждые waitTime секунды проверка
        }

    }


    // --- это сработает если триггер выставлен на колайдере (только он тут срабатывает)---
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);

        // Находим монетку так называемую  
        if (other.gameObject.name.IndexOf("Enemy") > -1)
        {
            // ++sumCoin;
            // infoCoin.text = "score " + sumCoin.ToString();
            // particlCoin.Play();
            // m_MyAudioSource.PlayOneShot(soundCoin);
            //Destroy(other.gameObject); // Удаляем объект 
            EnemyInterface enemyDead = other.GetComponent<EnemyInterface>(); // EnemyInterface
            // тут надо определять тип
            enemyDead.dead();
            player.addScore(10);
            Destroy(this.gameObject); // Удаляем пулю 
            //Destroy(other.transform.parent.gameObject); // и его родителя

        }
    }



}
