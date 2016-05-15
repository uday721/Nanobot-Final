using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public int numberOfEnemies;
    public string[] EnemyType = new string[] { "goodBacteria", "badBacteria", "virus" , "rbc" , "wbc", "nanobot" };
    public static GameObject[] AllGameobjects;
    public GameObject characterGameObject;


    public static GameObject[] enemyType1;
    public static GameObject[] enemyType2;
    public static GameObject[] enemyType3;
    public static GameObject[] enemyType4;
    public static GameObject[] enemyType5;
    public static GameObject[] enemyType6;

    public static Dictionary<string, int> Health = new Dictionary<string, int>();

    List<GameObject> spawnNPC = new List<GameObject>();

    private bool[,] enemyMatrix = new bool[6, 6];

    // Use this for initialization
    void Start () {
        CreateEnemies();
        //InitializeEnemyMatrix();
        PrepareAllEnemies();

    }

    void PrepareAllEnemies()
    {
         enemyType1 = GameObject.FindGameObjectsWithTag("goodBacteria");
         enemyType2 = GameObject.FindGameObjectsWithTag("badBacteria");
         enemyType3 = GameObject.FindGameObjectsWithTag("virus");
         enemyType4 = GameObject.FindGameObjectsWithTag("rbc");
         enemyType5 = GameObject.FindGameObjectsWithTag("wbc");
         enemyType6 = GameObject.FindGameObjectsWithTag("nanobot");
    }


    //void InitializeEnemyMatrix()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        for (int j = 0; j < 5; j++)
    //        {
    //            enemyMatrix[i, j] = false;
    //        }
    //    }


    //    //enemies for good bacteria
    //    enemyMatrix[0, 2] = true;

    //    //enemies for bad bacteria
    //    enemyMatrix[1, 1] = true;

    //    //enemies forvirus
    //    enemyMatrix[2, 4] = true;

    //    //enemies for rbc
    //    enemyMatrix[3, 2] = true;

    //    //enemies forwbc
    //    enemyMatrix[4, 5] = true;

    //    //enemies fornanobot
    //    enemyMatrix[5, 2] = true;
    //}


    void CreateEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        { int enemytype= Random.Range(0,4);
            bool[] enemylist = GetEnemyList(enemytype);

            //Add new gameobject to list
            spawnNPC.Add(CreateEnemy(EnemyType[enemytype]));
        }
    }

    GameObject CreateEnemy(string enemyName)
    {
        characterGameObject = (GameObject)Instantiate(Resources.Load(enemyName));
        Health.Add(enemyName, 100);
        return characterGameObject;
    }

    bool[] GetEnemyList(int charactertype)
    {
        bool[] matrixRow = new bool[5];
        for (int i = 0; i < 5; i++)
            matrixRow[i] = enemyMatrix[charactertype,i];

        return matrixRow;
    }

	// Update is called once per frame
	void Update () {
	
	}

}
