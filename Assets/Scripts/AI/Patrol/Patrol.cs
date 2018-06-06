using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Patrol : MonoBehaviour
{
    #region Variables
    public Transform point;// Waypoint Grouping
    public float moSpeed = 5f;// Movement Speed 
    public float closeness = 1f;// Closeness to WayPoints

    private Transform[] waypoints;
    private int curIndex = 0; // Current Index
    private Vector3 spawnP;// Spawn Point

    public Halt halted;
    #endregion
    #region Start
    // Use this for initialization
    void Start()
    {
        halted.GetComponent<Halt>().enabled = true;

        //rig = GetComponent<Rigidbody>();
        
        spawnP = transform.position;

        int length = point.childCount;
        waypoints = new Transform[length];

        for (int i = 0; i < length; i++)
        {
            waypoints[i] = point.GetChild(i);
        }
    }
    #endregion
    #region Update
    // Update is called once per frame
    void Update()
    {
        
        
        if (halted != null)
        {
            Move();
        }
    }
    #endregion
    #region Move
    void Move()
    {
        Transform current = waypoints[curIndex];

        Vector3 pos = transform.position; // pos = Position
        Vector3 dir = current.position - pos;// dir = Direction
        pos += dir.normalized * moSpeed * Time.deltaTime;

        transform.position = pos;

        transform.rotation = Quaternion.LookRotation(dir);// Rotates AI GameObject

        float dist = Vector3.Distance(pos, current.position);// dist = Distance
        if (dist <= closeness)
        {
            curIndex++;
        }
        if (curIndex >= waypoints.Length)
        {
            curIndex = 0;
        }
        
    }
    #endregion
}
