using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    //public int numberOfEnemies;
    private int spawnedenemies;
    public float spawntimer;
    public float respawntimer;
    private float temp1;
    private float temp2;

    private int zone=0;
    private int gameobjCounter=0;
    
    public static GameObject[] AllGameobjects;
    public GameObject characterGameObject;
    public GameObject genericgameobj;

    public GameObject initializergameObj;
    public GameObject goodBacteria;
    public GameObject badBacteria;
    public GameObject virus;
    public GameObject rbc;
    public GameObject wbc;


    public static List<GameObject> EnemyType = new List<GameObject>();

    public static List<GameObject> enemyType0 = new List<GameObject>();
    public static List<GameObject> enemyType1 = new List<GameObject>();
    public static List<GameObject> enemyType2 = new List<GameObject>();
    public static List<GameObject> enemyType3 = new List<GameObject>();
    public static List<GameObject> enemyType4 = new List<GameObject>();
    public static List<GameObject> enemyType5 = new List<GameObject>();
    //public static List<GameObject> enemyType6 = new List<GameObject>();

    public static List<List<GameObject>> listOfenemies = new List< List< GameObject > > ();

    public static Dictionary<string, int> Health = new Dictionary<string, int>();
    public static Dictionary<string, int> ZoneTracker = new Dictionary<string, int>();

    List<GameObject> spawnNPC = new List<GameObject>();

    private bool[,] enemyMatrix = new bool[6, 6];

    private bool initialized;

    // Use this for initialization
    void Start()
    {
        Health.Add("nanobot", 20);

        temp1 = spawntimer;
        temp2 = respawntimer;
        initialized = false;
        spawnedenemies = 0;
        EnemyType.Add(goodBacteria);
        EnemyType.Add(badBacteria);
        EnemyType.Add(virus);
        EnemyType.Add(rbc);
        EnemyType.Add(wbc);

        enemyType0.Add(initializergameObj);
        InitializeListofEnemyList();

    }

    void Update()
    {
        spawntimer = spawntimer - Time.deltaTime;
        respawntimer = respawntimer - Time.deltaTime;

        if (spawntimer > 0)
        {
            CreateEnemies();
        }

        if (respawntimer < 0)
        {
            spawntimer = temp1;
            respawntimer = temp2;
        }

        

    }


    void InitializeListofEnemyList()
    {
        listOfenemies.Add(enemyType0);
        listOfenemies.Add(enemyType1);
        listOfenemies.Add(enemyType2);
        listOfenemies.Add(enemyType3);
        listOfenemies.Add(enemyType4);
        listOfenemies.Add(enemyType5);
       // listOfenemies.Add(enemyType6);
    }


    void CreateEnemies()
    {

            int enemytype = Random.Range(0, 5);
            //bool[] enemylist = GetEnemyList(enemytype);

            listOfenemies[enemytype].Add(CreateEnemy(EnemyType[enemytype]));
        spawnedenemies++;

    }

    GameObject CreateEnemy(GameObject enemyName)
    {
        float x = Random.Range(-60, 60);
        float z = Random.Range(-60, 60);
        characterGameObject = (GameObject)Instantiate(Resources.Load(enemyName.gameObject.name),new Vector3(x,0,z),new Quaternion(0.0f,0.0f,0.0f,0.0f));

        characterGameObject.name = enemyName.gameObject.name+ gameobjCounter;
        //Debug.Log(characterGameObject.name );

        if (characterGameObject.transform.position.x > -61.0f && characterGameObject.transform.position.x < 0.0f && characterGameObject.transform.position.z > -61.0f && characterGameObject.transform.position.z < 0.0f)
            zone = 1;
        if (characterGameObject.transform.position.x > 0.0f && characterGameObject.transform.position.x < 61.0f && characterGameObject.transform.position.z > -61.0f && characterGameObject.transform.position.z < 0.0f)
            zone = 2;
        if (characterGameObject.transform.position.x > -61.0f && characterGameObject.transform.position.x < 0.0f && characterGameObject.transform.position.z > 0.0f && characterGameObject.transform.position.z < 61.0f)
            zone = 3;
        if (characterGameObject.transform.position.x > 0.0f && characterGameObject.transform.position.x < 61.0f && characterGameObject.transform.position.z > 0.0f && characterGameObject.transform.position.z < 61.0f)
            zone = 4;

        ZoneTracker.Add(characterGameObject.gameObject.name, zone);

        Health.Add(characterGameObject.name, 100);
        gameobjCounter++;

        return characterGameObject;

    }

    //bool[] GetEnemyList(int charactertype)
    //{
    //    bool[] matrixRow = new bool[5];
    //    for (int i = 0; i < 5; i++)
    //        matrixRow[i] = enemyMatrix[charactertype, i];

    //    return matrixRow;
    //}


}
