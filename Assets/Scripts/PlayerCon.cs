using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float maxVelocity = 10f;
    //public GameObject deathParticales;

    private Rigidbody rig;
    private Transform cam; // << Camera ADDED

    private Vector3 spawnPoint;

    // Use this for initialization
    void Start()
    {
        rig = GetComponent<Rigidbody>();

        cam = Camera.main.transform; //<< Camera ADDED

        // Record starting position
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(inputH, 0, inputV);

        // Rotate input to face direction of Camera (flat on surface)
        inputDir = Quaternion.AngleAxis(cam.eulerAngles.y, Vector3.up) * inputDir;

        //OldCode: forces to move and Cant use with physics
        // Copy position
        Vector3 position = transform.position;
        // Offset to the new position
        position += inputDir * movementSpeed * Time.deltaTime;
        // Apply new position to rigidbody
        rig.MovePosition(position);
    }

    public void Reset()
    {
        
        // Reset position of the player to start position
        transform.position = spawnPoint;
        // Reset the Velocity
        rig.velocity = Vector3.zero;
    }
}

