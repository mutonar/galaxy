using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitOBJToScreen: MonoBehaviour
{
    Vector2 bottomLeft;
    Vector2 bottomRight;
    Vector2 topLeft;
    Vector2 topRight;
    Rigidbody2D rb; // что будет двигаться
    float acceleration = 20.5f;  // сила взаимодействия
    bool enterLimits = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 
        float width = Camera.main.pixelWidth; // что бы тут огранияения работали нужно камеру на 2Д сменить
        float height = Camera.main.pixelHeight;
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(width, 0));
        topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, height));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(width, height));

    }

    void Update()
    {
        // пробую делать отскок от стены(теперь ограничение)
        if (transform.position.x > topRight.x || transform.position.x < -topRight.x || transform.position.y > topRight.y || transform.position.y < bottomRight.y)
        {
            //enterLimits = false;
            
            
            //Vector3 direction = transform.rotation.eulerAngles; // вычеслить поворот в векторе
            //Debug.Log(direction);
            //transform.Rotate(-transform.rotation); // Повернуть первоначально
            //transform.Rotate(0, 180, 0); // на 180
            //rb.AddForce(transform.right * acceleration); //усокрение 
            //transform.Rotate(0, 180, 0); // и обратно на 180
            

            // нормальный лимит но прилипляет к углам
            //rb.isKinematic = true; //не помогает включить
            rb.velocity = new Vector3(0, 0, 0);  // а вот это останавливает
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, topLeft.x, topRight.x), Mathf.Clamp(transform.position.y, bottomRight.y, topRight.y), transform.position.z); // останавливает
            //transform.position = new Vector3(Mathf.Clamp(transform.position.x, topRight.x, topLeft.x), Mathf.Clamp(transform.position.y, bottomRight.y, topRight.y), transform.position.z); // так перемещает на ругую сторону но как то по одно оси только

            //Vector3 direction = transform.rotation.eulerAngles; // вычеслить поворот в векторе
            //transform.Rotate(-direction); // Повернуть первоначально
            //rb.AddForce(direction * acceleration, ForceMode2D.Impulse);
            //Debug.Log(transform.rotation);
            //rb.AddForce(transform.right * acceleration);
        }

        // только когда внутри экранна
        if (transform.position.x < topRight.x & transform.position.x > -topRight.x & transform.position.y < topRight.y & transform.position.y > bottomRight.y)
        {
            enterLimits = true;
            //Debug.Log(enterLimits);
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(bottomRight, topRight);
    }

}
