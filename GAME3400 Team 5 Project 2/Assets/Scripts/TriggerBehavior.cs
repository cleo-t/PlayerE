using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehavior : MonoBehaviour
{

    public float moveAmount = 3.3f;
    public AudioClip closeSound;

    private bool triggered = false;

    private Transform rb;
    private Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
       
        rb = transform.parent;
        //audioSource = gameObject.GetComponentInParent<AudioSource>();
        moveVector = new Vector3(rb.transform.position.x, (rb.transform.position.y - moveAmount), rb.transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other) {
        if (!triggered && other.gameObject.tag.Equals("Player")) {
            rb.transform.position = moveVector;
            AudioSource.PlayClipAtPoint(closeSound, gameObject.transform.position);
            LightingTransition.instance.brightness = 0;
            Destroy(gameObject);
        }
    }

}
