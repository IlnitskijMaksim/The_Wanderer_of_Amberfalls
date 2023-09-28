using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : ToWeapon
{
    public GameObject bullet;
    private float timeFire;
    public float buletSpeed_1;

    public override void Shoot()
    {
        if (timeFire <= 0)
        {
            // ѕолучите позицию курсора мыши в мировых координатах
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Ќаправьте пулю в направлении позиции курсора мыши
            Vector3 shootDirection = (mousePosition - firePoint.position).normalized;

            GameObject gObject = Instantiate(bullet, firePoint.position, firePoint.rotation);
            if (gObject != null)
            {
                gObject.GetComponent<ArrowBullet>().tw = this;
                gObject.GetComponent<ArrowBullet>().buletSpeed = buletSpeed_1;
            }
            timeFire = fireRate;
        }
    }

    public void Start()
    {
        timeFire = fireRate;
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }

        timeFire -= Time.deltaTime;

    }
}
