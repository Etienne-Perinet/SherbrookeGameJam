using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    private AudioManager audioManager;

    private float bulletForce = 20f;

    private float timeBetweenShots = .35f;

    private float nextShotTimer = 0.0f;

    private void Awake() 
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || (Time.time > nextShotTimer && Input.GetButton("Fire1")))
            Shoot();
    }

    private void Shoot()
    {
        audioManager.Play("GunSound");
        nextShotTimer = Time.time + timeBetweenShots;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
