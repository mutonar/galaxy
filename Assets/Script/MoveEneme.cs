using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEneme : MonoBehaviour, EnemyInterface, EnemyInterfaceClone
{
    AudioSource m_MyAudioSource; // от куда будет звук
    AudioClip soundFire; // воспроизведение Звука выстрела
    float speedEnemy = 0.15f;
    public  Vector3 targetPos;
    Vector3 startPos; // Где появились
    private Vector3 tmpV; // тестовое для проверки
    public bool stop;
    float timedest = 15.0f; // время жизни
    float timeLife; // текучее время жизни
    float timeStart;
    public int cloneIter = 5; // какая итерация наследников будет

    // Start is called before the first frame update
    void Start()
    {
        m_MyAudioSource = Camera.main.GetComponent<AudioSource>(); // воспроизводим звук в камеру
        soundFire = (AudioClip)Resources.Load("sound/519992__casstway__strangecollision", typeof(AudioClip)); // Звук выстрела

        startPos = transform.position;
        timeStart = Time.time;
        //Debug.Log(GameObject.Find("test"));
        targetPos = GameObject.Find("player").transform.position; // находим игрока и его позицию
        targetPos = targetPos + (-startPos);
        StartCoroutine(moveBulet(0.03f)); // Запуск каротина (летят )
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPos != Vector3.zero) // null на вектора нет
        {

            transform.position += (targetPos - transform.position) * speedEnemy * Time.deltaTime;
        }
        else Destroy(this.gameObject); // нет цели уничтожаем

        if (tmpV != Vector3.zero)
        {
            //Debug.Log(tmpV);
            transform.position += (tmpV - transform.position) * speedEnemy * Time.deltaTime; // почему блядь это меняет а targetPos нет?
        }
    }

    IEnumerator moveBulet(float waitTime)
    {
        while (!stop)
        {
            if (Time.time > timeStart + timedest)
            {
                 Destroy(this.gameObject); // уничтжаем после времени timedest
            }

            yield return new WaitForSeconds(waitTime); //  каждые waitTime секунды проверка
        }

    }

    // --- реализованный метод интерфейса ---
    public void dead()
    {
        if (cloneIter > 0)
        {
            --cloneIter;
            for (int i = 0; i < 2; i++) // два наследника будет так как от выстрела на 2 разивание(может не быть наследников)
            {
                // создаем кастомный куб
                GameObject cube = Instantiate(gameObject, transform.position, Quaternion.identity);
                cube.transform.localScale = cube.transform.localScale / 2; // делаем себя в два раза меньше 
                //cube.GetComponent<CircleCollider2D>().enabled = true; // почему то надо включать(почему он сейчас включается)
                MoveEneme mv = cube.GetComponent<MoveEneme>(); // почему то надо включать
                //mv.enabled = false;
                int[] data = null;
                int angleOffset = 45;
                //mv.setTargetPoint(Quaternion.AngleAxis(-angleOffset, Vector3.forward) * targetPos);
                //mv.setTargetPoint(new Vector3(6,6,6)); //  почему не меняет вектор на прямую
               // Debug.Log(mv.getCurrentPoint());
                if (i % 2 == 0) mv.setTargetPoint(Quaternion.AngleAxis(-angleOffset, Vector3.forward) * targetPos);
                else mv.setTargetPoint(Quaternion.AngleAxis(angleOffset, Vector3.forward) * targetPos);

                // пока передаю только итерацию клонирования и углы смещения
            }
        }
        Destroy(this.gameObject); // Удаляем объект 
        //float sizeW = cube.GetComponent<Renderer>().bounds.size.y; // получить размер нашего объекта (Высоты)
    }

    public void setChildData(int[] data) // не работает передача
    {
        //Debug.Log("before " + targetPos);
        if(data[0] != null) cloneIter = data[0];
        if (data[1] != null) targetPos = Quaternion.AngleAxis(data[1], Vector3.up) * targetPos; // Повернуть угол на вектор
        //Debug.Log("After " + targetPos);
    }

    public void setTargetPoint(Vector3 tmpV)
    {
        //Debug.Log("before " + targetPos);
        this.tmpV = tmpV;
        //Debug.Log("After " + targetPos);
    }

    public Vector3 getCurrentPoint()
    {
        return tmpV;
    }

    public void touchUser()
    {
        m_MyAudioSource.PlayOneShot(soundFire);
        Destroy(this.gameObject); // Удаляем объект 
    }
}
