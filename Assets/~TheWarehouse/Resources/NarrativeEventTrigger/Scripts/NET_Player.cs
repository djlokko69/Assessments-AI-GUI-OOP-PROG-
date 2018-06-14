using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NET_Player : MonoBehaviour {

    public Text eventTitle;
    public Text eventDescription;
    public GameObject eventDisplayObject;

	// Use this for initialization
	void Start () {

        eventDisplayObject.SetActive(false);
		
	}
	

    void OnTriggerEnter(Collider obj)
    {
        if (obj.GetComponent<NET_EventTrigger>())
        {
            eventDisplayObject.SetActive(true);
            eventTitle.text = obj.GetComponent<NET_EventTrigger>().eventTitle;
            eventDescription.text = obj.GetComponent<NET_EventTrigger>().eventDescription;
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.GetComponent<NET_EventTrigger>())
        {
            eventDisplayObject.SetActive(false);
            eventTitle.text = "";
            eventDescription.text = "";
        }
    }
}
