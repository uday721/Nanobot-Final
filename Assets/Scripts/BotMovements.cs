using UnityEngine;
using System.Collections;

public class BotMovements : MonoBehaviour {
   public float speed = 5.0f;
    public float horizontalSpeed = 5.0F;
    public float verticalSpeed = 2.0F;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        float h = horizontalSpeed * Input.GetAxis("Mouse X");
       // float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(0, h*2 , 0);

    }
}
