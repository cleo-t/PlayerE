using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehavior : MonoBehaviour
{
    private bool triggered = false;

    private Transform rb;
    private Vector3 moveAmount;
    private AudioSource audioSource;
    



    // Start is called before the first frame update
    void Start()
    {
       
        rb = transform.parent;
        audioSource = gameObject.GetComponentInParent<AudioSource>();
        moveAmount = new Vector3(rb.transform.position.x, (rb.transform.position.y - 3.3f), rb.transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Here1");
        if (!triggered) {
            rb.transform.position = moveAmount;
            audioSource.Play();
        }

        Destroy(gameObject);
    }

}
