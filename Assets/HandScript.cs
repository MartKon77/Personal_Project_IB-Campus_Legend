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
    float handReach;
    public bool isControlled;
    public bool onHold;
    public GameObject CurrentHold;
    void Start()
    {
        MouseTracker = GameScript.MouseTracker;
        controlRange = GameScript.controlRange;
        handReach = GameScript.handReach;
        Rb.bodyType = RigidbodyType2D.Dynamic;
        onHold = false;
        isControlled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Vector3.Distance(transform.position, MouseTracker.position) <= controlRange && Time.timeScale > 0)
        {
            isControlled = true;
            onHold = false;
            Rb.velocity = Vector3.zero;
        }
        if (isControlled)
        {
            transform.position = MouseTracker.position;
            Rb.velocity = Vector3.zero;
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

        if(gameObject.name == "Right Hand")
        {
            if (isControlled && transform.position.y - GameScript.HandL.transform.position.y > handReach)
            {
                isControlled = false;
                Rb.velocity = Vector3.zero;
            }
        }
        else if (gameObject.name == "Left Hand")
        {
            if (isControlled && transform.position.y - GameScript.HandR.transform.position.y > handReach)
            {
                isControlled = false;
                Rb.velocity = Vector3.zero;
            }
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
