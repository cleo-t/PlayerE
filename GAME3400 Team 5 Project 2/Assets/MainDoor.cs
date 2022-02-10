using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainDoor : MonoBehaviour
{
    //List<GameObject> torches;
    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TorchManager.instance.allTorchesLit) {
            Debug.Log("DONE");
            LightingTransition.instance.brightness = 0.5f;
            Destroy(gameObject);
        }
        
    }

}
