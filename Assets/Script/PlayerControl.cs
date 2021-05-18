using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update

    FireGun weapon;
    Rigidbody2D rb; // что будет двигаться
    public static Vector3 direction = new Vector3(0, 2, 0); // Само направление импульса (Будем менять его при попадании как и смену камеры)
    float acceleration = 7.5f;  // сила взаимодействия
    int lifePlayer = 100;
    int scorePlayer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 
        weapon = GetComponent<FireGun>(); //  
    }

    // Update is called once per frame
    void Update()
    {
        // ///////////////////// перемещение персонажа кнопками //////////////////////////// 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Translate(new Vector3(-0.1f, 0, 0));
            transform.Rotate( 0.0f, 0.0f, 2.0f, Space.Self);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0.0f, 0.0f, -2.0f, Space.Self);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //direction = transform.rotation.eulerAngles; // вычеслить поворот в векторе
            //rb.AddForce(direction.normalized * acceleration); // все условия прыжка то и даем ускорение
            rb.AddForce(transform.right * acceleration);
            //Debug.Log("acceleration: " + transform.forward.ToString());
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(-transform.right * acceleration / 2);
        }



        if (Input.GetButton("Jump"))
        {
            weapon.fire();
           
        }
       
    }

    // --- это сработает если триггер выставлен на колайдере (только он тут срабатывает)---
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.name.IndexOf("Enemy") > -1)
        {
            // ++sumCoin;
            // infoCoin.text = "score " + sumCoin.ToString();
            // particlCoin.Play();
            // m_MyAudioSource.PlayOneShot(soundCoin);
            //Destroy(other.gameObject); // Удаляем объект 
            EnemyInterface enemyDead = other.GetComponent<EnemyInterface>(); // EnemyInterface
            enemyDead.touchUser();
            lifePlayer = lifePlayer - 10;

            //Destroy(other.transform.parent.gameObject); // и его родителя

        }
    }

   // --- Возвращаем жизни пользователя. ---
    public int getLifePlayer() {

        return lifePlayer;
    }

    // --- Возвращаем очки пользователя. ---
    public int getScorePlayer()
    {

        return scorePlayer;
    }

    // --- Установить очки ---
    internal void addScore(int v)
    {
        scorePlayer += v;
    }


}
