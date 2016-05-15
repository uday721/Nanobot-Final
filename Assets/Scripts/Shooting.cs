using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    public int rotationOffSet = 0;
    public float fireRate = 0;
    public float Damage = 10;
    public LayerMask whatTOHit;
    public Transform bulletTrailPrefab;

    Transform firePoint;
    float timeTOFire = 0;

    // for initialization
    void Awake()
    {
        firePoint = transform.FindChild("firepoint");
        if (firePoint == null)
        {
            Debug.Log("No FiringPoint gameobject, create one ");
        }
    }


    // Update is called once per frame
    void Update()
    {

        ////------------------------ Code to move the turret with mouse ----------------------
        //Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //diff.Normalize();
        //float rotate = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotate + rotationOffSet);
        ////---------------------------- end of turret movement code ----------------------------

        ////------------------------- code to making the turret fire ---------------------------

        if (fireRate == 0)
        {

            if (Input.GetButtonDown("Fire1"))
            {

                shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeTOFire)
            {
                timeTOFire = Time.time + (1 / fireRate);
                shoot();
            }
        }
    }
    void shoot()
    {
        // mouse position w.r.t game
        //Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
        //Debug.Log("update ");
        //Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.z);
        //RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatTOHit);
        Effect();
        //Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100);
    }

    void Effect()
    {
        Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);
    }
}
