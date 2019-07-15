using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class AIObjects
{
    //------->
    // Declare our variables
    public string AIGroupName { get { return m_aiGroupName; } }
    public GameObject objectPrefab { get { return m_prefab; } }
    public int maxAI { get { return m_maxAI; } }
    public int spawnRate { get { return m_spawnRate; } }
    public int spawnAmount { get { return m_maxSpawnAmount; } }
    public bool randomiseStats { get { return m_randomiseStats; } }

    //Serialize the private variables
    [Header("AI Group Stats")]
    [SerializeField]
    private string m_aiGroupName;
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    [Range(0f, 30f)]
    private int m_maxAI;
    [SerializeField]
    [Range(0f, 20f)]
    private int m_spawnRate;
    [Range(0f, 10f)]
    private int m_maxSpawnAmount;
    [SerializeField]
    private bool m_randomiseStats;
}

public class AISpawner : MonoBehaviour {

    //------->
    // Declare our variables
    

    // Note: using list because we don't know the size of it, array would need to set size first
    public List<Transform> Waypoints = new List<Transform>();

    // Create an array from new class
    [Header ("AI Groups Settings")]
    public AIObjects[] AIObject = new AIObjects[5];

	// Use this for initialization
	void Start ()
    {
        GetWaypoints();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Method for putting random values in the AI group setting
    void RandomiseGroups()
    {
        //randomise
        for(int i = 0; i < AIObject.Count(); i++)
        {
            if(AIObject[i].randomiseStats)
            {

            }
        }
    }

    void GetWaypoints()
    {
        //list using standard library
        //look through nested children
        Transform[] wpList = this.transform.GetComponentsInChildren<Transform>(); //note: 'this' is not required any more

        for(int i = 0; i < wpList.Length; i++)
        {
            if(wpList[i].tag == "waypoint")
            {
                //add to the list
                Waypoints.Add(wpList[i]);
            }

        }
    }
}
