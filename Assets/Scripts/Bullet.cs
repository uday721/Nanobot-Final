using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int bulletMoveSpeed = 200;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * bulletMoveSpeed);
        Destroy(gameObject, 4);

        //if (Time.deltaTime > 2)
        //{
        //    Destroy(gameObject);
        //}
    }


    void OnCollisionEnter(Collision other)
    {
        //if (
        //    (gameObject.name.Substring(0, 2) == "VBu" && other.gameObject.name.Substring(0, 2) == "wbc") ||
        //      (gameObject.name.Substring(0, 2) == "VBu" && other.gameObject.name.Substring(0, 2) == "rbc") ||
        //      (gameObject.name.Substring(0, 2) == "NBB" && other.gameObject.name.Substring(0, 2) == "rbc") ||
        //      (gameObject.name.Substring(0, 2) == "RBu" && other.gameObject.name.Substring(0, 2) == "vir") ||
        //      (gameObject.name.Substring(0, 2) == "NBB" && other.gameObject.name.Substring(0, 2) == "vir") ||
        //      (gameObject.name.Substring(0, 2) == "BBB" && other.gameObject.name.Substring(0, 2) == "gud") ||
        //      (gameObject.name.Substring(0, 2) == "NBB" && other.gameObject.name.Substring(0, 2) == "bad") ||
        //      (gameObject.name.Substring(0, 2) == "WBC" && other.gameObject.name.Substring(0, 2) == "bad")
        //    )
        //{
        //    //Debug.Log(other.gameObject.name);
        //    GameManager.Health[other.gameObject.name] = GameManager.Health[other.gameObject.name] - 10;
        //}



    }

}
