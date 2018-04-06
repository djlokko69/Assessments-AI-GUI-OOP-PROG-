using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform point;// Waypoint Grouping
    public float moSpeed = 5f;// Movement Speed 
    public float closeness = 1f;

    private Transform[] waypoints;
    private int currentIndex = 0;
    private Vector3 spawnP;// Spawn Point

    // Use this for initialization
    void Start()
    {
        spawnP = transform.position;

        int length = point.childCount;
        waypoints = new Transform[length];

        for (int i = 0; i < length; i++)
        {
            waypoints[i] = point.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform current = waypoints[currentIndex];

        Vector3 pos = transform.position; // pos = Position
        Vector3 dir = current.position - pos;// dir = Direction
        pos += dir.normalized * moSpeed * Time.deltaTime;

        transform.position = pos;

        transform.rotation = Quaternion.LookRotation(dir);

        float dist = Vector3.Distance(pos, current.position);// dist = Distance
        if(dist<=closeness)
        {
            currentIndex++;
        }
        if(currentIndex>=waypoints.Length)
        {
            currentIndex = 0;
        }
    }
}
