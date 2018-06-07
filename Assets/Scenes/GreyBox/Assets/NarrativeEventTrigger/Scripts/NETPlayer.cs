using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NETPlayer : MonoBehaviour
{

    public Text eventTitle;
    public Text eventDescription;
    public GameObject eventDisplayObject;

    // Use this for initialization
    void Start()
    {

        eventDisplayObject.SetActive(false);

    }


    void OnTriggerEnter(Collider obj)
    {
        if (obj.GetComponent<NETEventTrigger>())
        {
            eventDisplayObject.SetActive(true);
            eventTitle.text = obj.GetComponent<NETEventTrigger>().eventTitle;
            eventDescription.text = obj.GetComponent<NETEventTrigger>().eventDescription;
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.GetComponent<NETEventTrigger>())
        {
            eventDisplayObject.SetActive(false);
            eventTitle.text = "";
            eventDescription.text = "";
        }
    }
}
