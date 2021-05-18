using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class RespaunEnemy : MonoBehaviour
{ 
    public GameObject[] enemies;
    Vector3 spawnValues = new Vector3(6, 6, 0); // на каком расстоянии в идеале от краев экрана
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;
    int numE = 0;

    int randEnemy;

    Vector2 bottomLeft;
    Vector2 bottomRight;
    Vector2 topLeft;
    Vector2 topRight;

    // Start is called before the first frame update
    void Start()
    {
        float width = Camera.main.pixelWidth; // что бы тут огранияения работали нужно камеру на 2Д сменить(просто подсчет так же)
        float height = Camera.main.pixelHeight;
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(width, 0));
        topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, height));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(width, height));

        StartCoroutine(waitSpawner());


    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator waitSpawner() {

        yield return new WaitForSeconds(startWait);
        while (!stop) {
            
            randEnemy = Random.Range(0, enemies.Length); // от нуля до количества врагов
            Vector3 topField = new Vector3 (Random.Range (bottomLeft.x - 1, bottomRight.x +1), Random.Range(topRight.y, topRight.y + 1), 0);
            Vector3 bottomField = new Vector3 (Random.Range (bottomLeft.x - 1, bottomRight.x + 1), Random.Range(bottomRight.y, bottomRight.y - 1), 0);
            Vector3 leftField = new Vector3 (Random.Range (bottomLeft.x - 1, bottomLeft.x), Random.Range(topLeft.y - 1, bottomRight.y + 1), 0);
            Vector3 rightField = new Vector3 (Random.Range (bottomRight.x, bottomRight.x + 1), Random.Range(topRight.y + 1, bottomRight.y - 1), 0);
            
            
            
            Vector3 spawnPosition = Vector3.zero;
            int casedField = Random.RandomRange(1,4); 
            switch (casedField)
            {
                case 1:
                    spawnPosition = topField;
                    break;
                case 2:
                    spawnPosition = bottomField;
                    break;
                case 3:
                    spawnPosition = leftField;
                    break;
                case 4:
                    spawnPosition = rightField;
                    break;
                default:
                    spawnPosition = new Vector3(Random.Range(bottomLeft.x - 1, bottomRight.x + 1), Random.Range(bottomRight.y - 1, topRight.y + 1), 0);
                    break;
            }

            var enemy = Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            enemy.name = "Enemy " + numE;
            ++numE;
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
