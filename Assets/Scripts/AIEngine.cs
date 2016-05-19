using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIEngine : MonoBehaviour
{


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

    //public Transform target;
    NavMeshAgent agent;

    float randomizeinterval = 3.0f;


    public string[] EnemyType = new string[] { "goodBacteria", "badBacteria", "virus", "rbc", "wbc", "nanobot" };
    public bool IsAI = false;
    int TresholdHealth = 50;

    private List<string> BulletObjects;
    public GameObject genericobjectInitializer;
    public static GameObject[] enemyType0;
    private float updategameobjTimer = 0;

    private static int[] stateMachine = { 0, 1, 2 };
    private int currentState = stateMachine[0];
    Dictionary<string, int> health = GameManager.Health;
    public static Dictionary<string, int> ZoneTracker = GameManager.ZoneTracker;

    bool isInteracting = false;
    int waypointchild = 0;
    List<GameObject> enemylist = new List<GameObject>();
    public int healthreducestep = 10;

    //to learn if it is a safe area
    public float healthcheckinterval = 3;

    int zone = 0;



    void Start()
    {
        StartCoroutine(CheckForCollision());

        updategameobjTimer = 20;
        EnemyArrayInitializer();
        agent = GetComponent<NavMeshAgent>();

    }

    IEnumerator CheckForCollision()
    {
        yield return null;
        //Debug.Log(zone);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Health[this.gameObject.name] < 0)
        {
            Destroy(gameObject);
            GameManager.Health.Remove(this.gameObject.name);
        }

        updategameobjTimer -= Time.deltaTime;
        if (updategameobjTimer < 0)
        {
            EnemyArrayInitializer();
            isInteracting = false;
            updategameobjTimer = 10;
        }

        if (!isInteracting)
        {
            RandomMovements();
        }

        //Debug.Log(updategameobjTimer);

        if (updategameobjTimer < 5 && !isInteracting && enemylist.Count != 0)
        {
            //Debug.Log("yes");
            InteraxtWithNPC();
        }




    }

    void FixedUpdate()
    {

    }


    void RandomMovements()
    {
        GameObject waypoints = GameObject.Find("RandomizeMovements"); ;// GameObject.FindGameObjectWithTag("RandomizeMovements");

        randomizeinterval -= Time.deltaTime;
        if (randomizeinterval < 0)
        {
            //change direction, reset timer
            waypointchild = Random.Range(0, 12);
            randomizeinterval = 3.0f;
        }
        //Debug.Log(randomizeinterval);
        //Debug.Log(waypoints.transform.GetChild(waypointchild).gameObject.name+ "  "+ randomizeinterval +"  "+ waypointchild);
        agent.SetDestination(waypoints.transform.GetChild(waypointchild).transform.position);
    }

    void EnemyArrayInitializer()
    {
        enemylist.Clear();
       // enemylist.Add(GameManager.enemyType0[0]);


        if (EnemygoodBacteria)
        {
            //BulletObjects.Add("");
            foreach (GameObject go in GameManager.listOfenemies[1])
            {
                    enemylist.Add(go);
            }
        }

        if (EnemybadBacteria)
        {
            foreach (GameObject go in GameManager.listOfenemies[2])
            {
                    enemylist.Add(go);
            }
        }

        if (EnemyVirus)
        {
            foreach (GameObject go in GameManager.listOfenemies[3])
            {
                enemylist.Add(go);
            }
           // enemylist.Add(GameObject.FindGameObjectWithTag("nanobot"));
        }

        if (Enemyrbc)
        {
            foreach (GameObject go in GameManager.listOfenemies[4])
            {
                enemylist.Add(go);
            }
        }

        if (Enemywbc)
        {
            foreach (GameObject go in GameManager.listOfenemies[5])
            {
                enemylist.Add(go);
            }
        }

        if (EnemyNanobot)
        {
                enemylist.Add(GameObject.Find("nanobot"));
            
        }

        if (enemylist.Count == 0)
        {
            isInteracting = false;
        }

    }

    void InteraxtWithNPC()
    {
        

        closest = FindClosestNPC(enemylist);

        if (closest == null)
        {
           // Debug.Log(gameObject.name);
        }

        if (closest != null)
        {
           // Debug.Log(closest.gameObject.name);

            if (health[closest.gameObject.name] > TresholdHealth)
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

        else { isInteracting = false; }
    }

    void ActionBasedonState()
    {
        if (currentState == 1)
        {
            Chase(closest);
            isInteracting = true;
        }
        else if (currentState == 2)
        {
            Run(closest);
            isInteracting = false;
        }

        else if (currentState == 0)
        {
            RandomMovements();
            isInteracting = false;
        }

    }

    GameObject FindClosestNPC(List<GameObject> enemyList)
    {
        GameObject _closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = this.transform.position;
        foreach (GameObject go in enemyList)
        {
            if (go)
            {
                float diff = Vector3.Distance(go.transform.position, this.transform.position);
                float curDistance = diff;
                if (curDistance < distance && curDistance != 0 && NotInSameGameObject(go, this.gameObject))
                {
                    _closest = go;
                    distance = curDistance;
                }
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
            //transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, step);
            agent.SetDestination(closest.transform.position);
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


    void UpdateZone()
    {
        if (gameObject.transform.position.x > -61.0f && gameObject.transform.position.x <0.0f && gameObject.transform.position.z >-61.0f && gameObject.transform.position.z <0.0f)
            zone = 1;
            if (gameObject.transform.position.x >0.0f && gameObject.transform.position.x <61.0f && gameObject.transform.position.z >-61.0f && gameObject.transform.position.z <0.0f)
            zone = 2;
        if (gameObject.transform.position.x >-61.0f && gameObject.transform.position.x <0.0f && gameObject.transform.position.z >0.0f && gameObject.transform.position.z <61.0f)
            zone = 3;
        if (gameObject.transform.position.x >0.0f && gameObject.transform.position.x <61.0f && gameObject.transform.position.z >0.0f && gameObject.transform.position.z <61.0f)
            zone = 4;

        if (zone!= GameManager.ZoneTracker[this.gameObject.name])
            {
            GameManager.ZoneTracker[this.gameObject.name] = zone;
            }
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (
            (gameObject.name.Substring(0, 2) == "wbc" && other.gameObject.name.Substring(0, 2) == "VBu") ||
            (gameObject.name.Substring(0, 2) == "rbc" && other.gameObject.name.Substring(0, 2) == "VBu") ||
            (gameObject.name.Substring(0, 2) == "rbc" && other.gameObject.name.Substring(0, 2) == "NBB") ||
            (gameObject.name.Substring(0, 2) == "vir" && other.gameObject.name.Substring(0, 2) == "RBu") ||
            (gameObject.name.Substring(0, 2) == "vir" && other.gameObject.name.Substring(0, 2) == "NBB") ||
            (gameObject.name.Substring(0, 2) == "gud" && other.gameObject.name.Substring(0, 2) == "BBB") ||
            (gameObject.name.Substring(0, 2) == "bad" && other.gameObject.name.Substring(0, 2) == "NBB")||
            (gameObject.name.Substring(0, 2) == "bad" && other.gameObject.name.Substring(0, 2) == "WBC")
            )
        {
            //Debug.Log("yes");
            GameManager.Health[this.gameObject.name] = GameManager.Health[this.gameObject.name] - 10;
        }


    }

    void OnCollisionEnter(Collision other)
    {
        if (gameObject.name.Length < 3 || other.gameObject.name.Length < 3)
        {
            Debug.Log(other.gameObject.name);
        }
        else if (
            (gameObject.name.Substring(0, 3) == "wbc" && other.gameObject.name.Substring(0, 3) == "VBu") ||
            (gameObject.name.Substring(0, 3) == "rbc" && other.gameObject.name.Substring(0, 3) == "VBu") ||
            (gameObject.name.Substring(0, 3) == "rbc" && other.gameObject.name.Substring(0, 3) == "NBB") ||
            (gameObject.name.Substring(0, 3) == "vir" && other.gameObject.name.Substring(0, 3) == "RBu") ||
            (gameObject.name.Substring(0, 3) == "vir" && other.gameObject.name.Substring(0, 3) == "NBB") ||
            (gameObject.name.Substring(0, 3) == "gud" && other.gameObject.name.Substring(0, 3) == "BBB") ||
            (gameObject.name.Substring(0, 3) == "bad" && other.gameObject.name.Substring(0, 3) == "NBB") ||
            (gameObject.name.Substring(0, 3) == "bad" && other.gameObject.name.Substring(0, 3) == "WBC")
            )
        {
            Debug.Log(other.gameObject.name);
            GameManager.Health[this.gameObject.name] = GameManager.Health[this.gameObject.name] - 25;
        }


        

    }



}
