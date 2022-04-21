using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    private float bulletForce = 20f;

    private float timeBetweenShots = .35f;

    private float nextShotTimer = 0.0f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || (Time.time > nextShotTimer && Input.GetButton("Fire1")))
            Shoot();
    }

    private void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("GunSound");
        nextShotTimer = Time.time + timeBetweenShots;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
