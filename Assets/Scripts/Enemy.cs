using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    public string[] EnemyType = new string[] { "goodBacteria", "badBacteria", "virus", "rbc", "wbc", "nanobot" };
    public int Health = 100;

   public Character(string enemyName)
    {
        CreateEnemy(enemyName);
    }

    void CreateEnemy(string enemyName)
    {

    }

    // Use this for initialization
    void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}
}
