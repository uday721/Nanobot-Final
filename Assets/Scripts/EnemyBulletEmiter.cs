using UnityEngine;
using System.Collections;

public class EnemyBulletEmiter : MonoBehaviour {

    public int enemyBulletSpped=10;
    public GameObject bulletPrefab;

    public float spawninterval = 1;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        spawninterval -= Time.deltaTime;
           Quaternion target = transform.rotation;
        GameObject bullet;
        if (spawninterval < 0)
        {
            for (int i = 0; i < 3; i++)
            {
                bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + (i * 90))) as GameObject;
                Rigidbody rb = bullet.GetComponent<Rigidbody>();

                rb.AddForce(transform.forward * enemyBulletSpped);
            }
            spawninterval = 1;
        }
    }
}

