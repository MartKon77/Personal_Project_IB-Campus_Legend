using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D Rb;
    [SerializeField] Transform MouseTracker;

    [SerializeField] bool onHold;
    [SerializeField] bool isControlled;
    void Start()
    {
        Rb.bodyType = RigidbodyType2D.Dynamic;
        onHold = false;
        isControlled = false;
    }

    void Update()
    {
        if(isControlled /* && Vector3.Distance(Rb.transform.position, something.transform.position) <= 2.1 */)
        {
            transform.position = MouseTracker.position;
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                Rb.velocity = Vector3.zero;
                isControlled = false;
            }
        }
        if(onHold)
        {
            Rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else if(!onHold && Rb.bodyType != RigidbodyType2D.Dynamic)
        {
            Rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Mouse Tracker" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isControlled = true;
            onHold = false;
        }
        if (col.tag == "Hold" && Input.GetKeyUp(KeyCode.Mouse0))
        {
            onHold = true;
        }
    }
}
