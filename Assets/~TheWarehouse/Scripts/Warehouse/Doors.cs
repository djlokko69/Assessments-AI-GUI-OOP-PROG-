using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TheWarehouse
{
    public class Doors : MonoBehaviour
    {
        public UnityEvent oTrig; // OnTrigger Event

        // Use this for initialization
        void Start()
        {

        }
        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player") { oTrig.Invoke(); }
        }
    }
}
