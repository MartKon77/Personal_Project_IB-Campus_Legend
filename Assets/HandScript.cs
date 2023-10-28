using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public Rigidbody2D Rb;
    [SerializeField] GameScript GameScript;
    Transform MouseTracker;

    float controlRange;
    public bool isControlled;
    public bool onHold;
    public GameObject CurrentHold;
    void Start()
    {
        MouseTracker = GameScript.MouseTracker;
        controlRange = GameScript.controlRange;
        Rb.bodyType = RigidbodyType2D.Dynamic;
        onHold = false;
        isControlled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Vector3.Distance(transform.position, MouseTracker.position) <= controlRange)
        {
            isControlled = true;
            onHold = false;
        }
        if (isControlled /* && Vector3.Distance(Rb.transform.position, something.transform.position) <= 2.1 */)
        {
            transform.position = MouseTracker.position;
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                isControlled = false;
                Rb.velocity = Vector3.zero;
            }
        }
    }
    void FixedUpdate()
    {
        if (onHold)
        {
            Rb.bodyType = RigidbodyType2D.Kinematic;
            Rb.velocity = Vector3.zero;
        }
        else if (!onHold && Rb.bodyType != RigidbodyType2D.Dynamic)
        {
            Rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Hold")
        {
            CurrentHold = col.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Hold")
        {
            CurrentHold = null;
        }
    }
}
