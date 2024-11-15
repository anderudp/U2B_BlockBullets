using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    int maxAmmo = 50;
    float bulletSpeed = 30f;
    float shootCooldown = 0.1f;
    bool canShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }
        else if(transform.childCount == 0 && !canShoot)
        {
            canShoot = true;
        }
    }

    IEnumerator Shoot()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 shootDirection = Camera.main.ScreenToWorldPoint(mousePos);
        shootDirection -= transform.position;
        Vector2 shootForce = new Vector2(shootDirection.x, shootDirection.y).normalized * bulletSpeed;
        for (int i = 0; i < maxAmmo; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform);
            bullet.GetComponent<Rigidbody2D>().velocity = shootForce;
            yield return new WaitForSeconds(shootCooldown);
        }
    }
}
