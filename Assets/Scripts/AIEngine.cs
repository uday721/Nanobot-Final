using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIEngine : MonoBehaviour {


    public bool EnemygoodBacteria;
    public bool EnemybadBacteria;
    public bool EnemyVirus;
    public bool Enemyrbc;
    public bool Enemywbc;
    public bool EnemyNanobot;

    private GameObject closest;
    private float distance;

    public float movespeed = 10.0f;
    public float rotationspeed = 10.0f;


    public string[] EnemyType = new string[] { "goodBacteria", "badBacteria", "virus", "rbc", "wbc", "nanobot" };
    //public int Health = 100;
    public bool IsAI = false;
    int TresholdHealth = 50;

    //public bool goodBacteria = false, badBacteria = false, virus = false, rbc = false, wbc = false, nanobot = false;

    //for random movements
    float wall_left = -5.0f;
    float wall_right = 5.0f;
    float wall_top = -5.0f;
    float wall_bottom = 5.0f;
    Vector3 AI_Position;
    float MoveSpeed = 0.05f;
    //Get two Random values within a Range (Screen dimensions)
    float randomX;
    float randomY;
    float randomZ;

    private GameObject[] genericObjects;
    private static int[] stateMachine = { 0,1,2};
    private int currentState = stateMachine[0];
    Dictionary<string, int> health = GameManager.Health;

    void Start () {
       
        randomX = Random.Range(0, 10);
         randomY = Random.Range(0, 10);
        randomZ = Random.Range(0, 10);

        randomX = randomX + this.transform.position.x;
        randomY = randomY + this.transform.position.y;
        randomZ = randomZ + this.transform.position.z;

    }



    // Update is called once per frame
    void Update () {
        //  InteraxtWithNPC();
        AI_Position = this.transform.position;
        RandomMovements();
        #region commentedcode
        // transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //GameObject closest = FindClosestNPC();
        //Debug.Log(closest.name);

        // transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, step);



        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(closest.transform.position - transform.position), rotationspeed * Time.deltaTime);
        ////move towards the player
        //transform.position += transform.forward * Time.deltaTime * movespeed;

        #endregion
    }

    void FixedUpdate()
    {

    }


    void RandomMovements()
    {
        Vector3 randomXYZ = new Vector3(randomX, 0,randomZ);
        Vector3 Direction = randomXYZ - AI_Position;

        //Normalize the Direction to apply appropriatly
        Direction = Direction.normalized;

        if ( AI_Position.x > wall_left && AI_Position.x < wall_right
         &&
         AI_Position.z < wall_bottom && AI_Position.z > wall_top)
        {
            //Make AI move in the Direction (adjust speed to your needs)
            transform.position += randomXYZ * MoveSpeed;
        }
        else
        {
            //make your AI do something when its not within the boundaries
            //maybe generate a new direction?
        }
    }


    void InteraxtWithNPC()
    {
        
        if (EnemygoodBacteria)
        { 
            //{ genericObjects = GameManager.enemyType1;
            genericObjects.Concat(GameManager.enemyType1).ToArray();
        }

            else if (EnemybadBacteria)
            {
            //genericObjects =  GameManager.enemyType2;
            genericObjects.Concat(GameManager.enemyType2).ToArray();
        }

            else if (EnemyVirus)
            { //genericObjects = GameManager.enemyType3; 
            genericObjects.Concat(GameManager.enemyType3).ToArray();
        }

            else if (Enemyrbc)
            { //genericObjects = GameManager.enemyType4; 
            genericObjects.Concat(GameManager.enemyType4).ToArray();
        }

            else if (Enemywbc)
            { //genericObjects = GameManager.enemyType5; 
            genericObjects.Concat(GameManager.enemyType5).ToArray();
        }

            else if (EnemyNanobot)
            { //genericObjects = GameManager.enemyType6;
            genericObjects.Concat(GameManager.enemyType6).ToArray();
        }

         closest= FindClosestNPC(genericObjects);

        if (health[closest.gameObject.name]  > TresholdHealth)
        {
            if (health[this.gameObject.name] > TresholdHealth)
            {
                currentState = stateMachine[1];
            }
            else if (health[this.gameObject.name] < TresholdHealth)
            {
                currentState = stateMachine[2];
            }
        }

       else if (health[closest.gameObject.name] < TresholdHealth)
        {
            if (health[this.gameObject.name] > health[closest.gameObject.name])
            {
                currentState = stateMachine[1];
            }
            else if (health[this.gameObject.name] < health[closest.gameObject.name])
            {
                currentState = stateMachine[2];
            }
        }

        //Chase(closest);
        ActionBasedonState();
    }

    void ActionBasedonState()
    {
        if (currentState == 1)
        {
            Chase(closest);
        }
        else if (currentState == 2)
        {
            Run(closest);
        }

        else if (currentState == 0)
        {
            RandomMovements();
        }
        
    }

    GameObject FindClosestNPC(GameObject[] enemyList)
    {
        GameObject _closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = this.transform.position;
        foreach (GameObject go in genericObjects)
        {
                float diff = Vector3.Distance(go.transform.position, this.transform.position);
                float curDistance = diff;
                if (curDistance < distance && curDistance != 0 && NotInSameGameObject(go, this.gameObject))
                {
                _closest = go;
                    distance = curDistance;
                }

        }
        return _closest;
    }

     bool NotInSameGameObject(GameObject currentObj, GameObject referenceObj)
    {
        if (currentObj.transform.root.gameObject != referenceObj.transform.root.gameObject)
        {
            return true;
        }
        else return false;
    }


    void Chase(GameObject closest)
    {

        float step = movespeed * Time.deltaTime;
        float distance = Vector3.Distance(closest.transform.position, transform.position);

        if (distance > 8)
        {
            transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, step);
        }
    }

    void Run(GameObject closest)
    {

        float step = -movespeed * Time.deltaTime; 
        float distance = Vector3.Distance(closest.transform.position, transform.position);

        if (distance < 15)
        {
            transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, step);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colloid");

        if (collision.transform.gameObject.name == "Bullet")
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            //Instantiate(explosionPrefab, pos, rot);
            Destroy(collision.transform.gameObject);
        }
    }



}
