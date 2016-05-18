using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CreateZones : MonoBehaviour {

    Mesh mesh;
    Vector3[] vertices ;
    Vector3[] worldvertices ;

    // Use this for initialization
    void Start () {

         mesh = GetComponent<MeshFilter>().mesh;
         vertices = mesh.vertices;
         worldvertices = new Vector3[vertices.Length];


    }

    void TrackVerices()
    {
        int i = 0;
        while (i < vertices.Length)
        {
            //vertices[i] += Vector3.up * Time.deltaTime;


            worldvertices[i] = this.gameObject.transform.localToWorldMatrix.MultiplyPoint3x4(vertices[i]);
            i++;
        }

        SetZoneDimentions(worldvertices, 4);
    }

    void SetZoneDimentions(Vector3[] worldvertices, int zoneDivision)
    {
        //Vector3[] worldvertices = new Vector3[vertices.Length];

    }


    // Update is called once per frame
    void Update () {




    }
}
