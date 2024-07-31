using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firepos;
    public float TimeBtwfire=0.2f;
    public float bulletForce=10;
    public GameObject muzzle;
    private float timeBtwfire;
    private Transform playerTransform;
    public AudioSource aus;
    public AudioClip bullet_player_sound;
    public AudioClip rebullet_player_sound;
    public float delayBetweenSounds = 0.5f;


    void Update()
    {
        
        timeBtwfire -= Time.deltaTime;
        if (Input.GetMouseButton(0)&&timeBtwfire<0)
        {
            FireBullet();
            if (aus && bullet_player_sound != null && rebullet_player_sound!=null)
            {
                StartCoroutine( PlaySoundsWithDelay(aus, bullet_player_sound, rebullet_player_sound));
            }

        }
        RotateGun();
    }
    IEnumerator PlaySoundsWithDelay(AudioSource aus, AudioClip sound1, AudioClip sound2)
    {
        aus.PlayOneShot(sound1);
        yield return new WaitForSeconds(delayBetweenSounds);
        aus.PlayOneShot(sound2);
    }
    void RotateGun()
    {
        Vector3 mousepotion= Camera.main.ScreenToWorldPoint( Input.mousePosition );
        Vector2 lookdir=mousepotion-transform.position;
        float angle=Mathf.Atan2(lookdir.y,lookdir.x)*Mathf.Rad2Deg;
        Quaternion rotation= Quaternion.Euler(0,0,angle);
        transform.rotation = rotation;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Đặt hướng scale mặc định
        Vector3 defaultScale = new Vector3(0.4f, 0.43f, 0);

        // Nếu player quay sang trái, đảo ngược hướng scale
        if (playerTransform.localScale.x < 0)
        {
            defaultScale.x *= -1;
        }

        // Lấy góc quay của súng (chuyển đổi về góc dương 0 đến 360)
        float gunAngle = (transform.eulerAngles.z + 360) % 360;

        // Nếu góc quay của súng nằm trong khoảng 90 đến 270 độ, đảo ngược hướng scale.y
        if (gunAngle > 90 && gunAngle < 270)
        {
            defaultScale.y *= -1;
        }

        // Áp dụng hướng scale đã tính toán
        transform.localScale = defaultScale;

    }
    void FireBullet()
    {
        timeBtwfire = TimeBtwfire;

        GameObject bulletTemp=Instantiate(bullet,firepos.position,Quaternion.identity);
        //effect
        Instantiate(muzzle, firepos.position,transform.rotation,transform);
        Rigidbody2D rb= bulletTemp.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }
}
