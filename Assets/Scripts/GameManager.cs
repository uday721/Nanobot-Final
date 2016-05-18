using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public int numberOfEnemies;
    public string[] EnemyType = new string[] { "goodBacteria", "badBacteria", "virus", "rbc", "wbc", "nanobot" };
    public static GameObject[] AllGameobjects;
    public GameObject characterGameObject;

    public GameObject initializergameObj;

    public static List<GameObject> enemyType0 = new List<GameObject>();
    public static List<GameObject> enemyType1 = new List<GameObject>();
    public static List<GameObject> enemyType2 = new List<GameObject>();
    public static List<GameObject> enemyType3 = new List<GameObject>();
    public static List<GameObject> enemyType4 = new List<GameObject>();
    public static List<GameObject> enemyType5 = new List<GameObject>();
    public static List<GameObject> enemyType6 = new List<GameObject>();

    public static List<List<GameObject>> listOfenemies = new List< List< GameObject > > ();

    public static Dictionary<string, int> Health = new Dictionary<string, int>();
    public static Dictionary<string, string> ZoneTracker = new Dictionary<string, string>();

    List<GameObject> spawnNPC = new List<GameObject>();

    private bool[,] enemyMatrix = new bool[6, 6];

    // Use this for initialization
    void Start()
    {
        enemyType0.Add(initializergameObj);
        InitializeListofEnemyList();

        CreateEnemies();
        PrepareAllEnemies();

    }

    void PrepareAllEnemies()
    {

    }

    void InitializeListofEnemyList()
    {
        listOfenemies.Add(enemyType0);
        listOfenemies.Add(enemyType1);
        listOfenemies.Add(enemyType2);
        listOfenemies.Add(enemyType3);
        listOfenemies.Add(enemyType4);
        listOfenemies.Add(enemyType5);
        listOfenemies.Add(enemyType6);
    }


    void CreateEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int enemytype = Random.Range(1, 6);
            bool[] enemylist = GetEnemyList(enemytype);

            listOfenemies[enemytype].Add(CreateEnemy(EnemyType[enemytype]));

        }
    }

    GameObject CreateEnemy(string enemyName)
    {
        float x = Random.Range(-60, 60);
        float z = Random.Range(-60, 60);
        characterGameObject = (GameObject)Instantiate(Resources.Load(enemyName),new Vector3(x,0,z),new Quaternion(0.0f,0.0f,0.0f,0.0f));
        Health.Add(enemyName, 100);
        return characterGameObject;
    }

    bool[] GetEnemyList(int charactertype)
    {
        bool[] matrixRow = new bool[5];
        for (int i = 0; i < 5; i++)
            matrixRow[i] = enemyMatrix[charactertype, i];

        return matrixRow;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
