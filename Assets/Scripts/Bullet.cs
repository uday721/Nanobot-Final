using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int bulletMoveSpeed = 200;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * bulletMoveSpeed);
        Destroy(gameObject, 2);
    }
}
