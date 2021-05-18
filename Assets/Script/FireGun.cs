using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour, GunInterface
{
    AudioSource m_MyAudioSource; // от куда будет звук
    AudioClip soundFire; // воспроизведение Звука выстрела
    public GameObject bulet;
    public GameObject Gun;
    private float nextFire = 0;
    private bool accesFire = false;
    private float fireRate = 3;

    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.name = "FireGun";
        m_MyAudioSource = Camera.main.GetComponent<AudioSource>(); // воспроизводим звук в камеру
        soundFire = (AudioClip)Resources.Load("sound/Laser/344276__nsstudios__laser3", typeof(AudioClip)); // Звук выстрела
       
    }


    void Update()
    {
        if (accesFire && Time.time > nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            shoot();
        }
    }

    private void shoot()
    {
        var b = Instantiate(bulet, Gun.transform.position, gameObject.transform.rotation);
        m_MyAudioSource.PlayOneShot(soundFire);
        accesFire = false;

    }

    // --- реализованный метод интерфейса  ---
    public void fire() 
    {
        accesFire = true;
    }
}
