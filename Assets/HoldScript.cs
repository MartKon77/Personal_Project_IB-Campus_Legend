using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hold : MonoBehaviour
{
    [SerializeField] GameScript GameScript;
    [SerializeField] GameObject Bar;
    Transform MouseTracker;
    GameObject HandR;
    GameObject HandL;
    HandScript ScriptR;
    HandScript ScriptL;
    BoxCollider2D Collider;
    SpriteRenderer BarSprite;

    [SerializeField] float grabRange;
    bool canRegrab;
    float holdTime;
    float currentHoldTime;

    [SerializeField] int holdType; // 0 - Jug, 1 - Crimp, 2 - Sloper
    Sprite HoldSprite;

    [SerializeField] SpriteRenderer SpriteRender;
    float spriteR;
    float spriteG;
    float spriteB;

    [SerializeField] int handCol; // 0 - no hands are near the hold, 1 - HandR is near the hold, 2 - HandL is near the hold
    void Start()
    {
        MouseTracker = GameScript.MouseTracker;
        HandR = GameScript.HandR;
        HandL = GameScript.HandL;
        ScriptR = HandR.GetComponent<HandScript>();
        ScriptL = HandL.GetComponent<HandScript>();
        BarSprite = Bar.GetComponent<SpriteRenderer>();
        Collider = gameObject.GetComponent<BoxCollider2D>();
        Collider.size = Vector2.one * (grabRange * 2);
        //canRegrab = true

        if (holdType == 0)
        {
            holdTime = GameScript.jugTime;
            HoldSprite = GameScript.JugSprite;
        }
        else if(holdType == 1)
        {
            holdTime = GameScript.crimpTime;
            HoldSprite = GameScript.CrimpSprite;
        }
        else
        {
            holdTime = GameScript.sloperTime;
            HoldSprite = GameScript.SloperSprite;
        }
        currentHoldTime = 1f;
        SpriteRender.sprite = HoldSprite;

        spriteR = Random.Range(100f, 200f);
        spriteG = Random.Range(100f, 200f);
        spriteB = Random.Range(100f, 200f);
        SpriteRender.color = new Color(spriteR / 255, spriteG / 255, spriteB / 255);
        BarSprite.color = new Color(spriteR / 255, spriteG / 255, spriteB / 255, 0.7f);
    }
    void Update()
    {
        if(handCol == 1)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) && Vector3.Distance(transform.position, HandR.transform.position) <= grabRange && Vector3.Distance(transform.position, MouseTracker.position) <= grabRange/* && canRegrab*/)
            {
                ScriptR.onHold = true;
                currentHoldTime = 1f;
            }
        }
        if(handCol == 2)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) && Vector3.Distance(transform.position, HandL.transform.position) <= grabRange && Vector3.Distance(transform.position, MouseTracker.position) <= grabRange/* && canRegrab*/)
            {
                ScriptL.onHold = true;
                currentHoldTime = 1f;
            }
        }
        
        if ((ScriptR.onHold && gameObject == ScriptR.CurrentHold) || (ScriptL.onHold && gameObject == ScriptL.CurrentHold))
        {
            Bar.SetActive(true);
        }
        else if(!(ScriptR.onHold && gameObject == ScriptR.CurrentHold) || !(ScriptL.onHold && gameObject == ScriptL.CurrentHold))
        {
            Bar.SetActive(false);
        }
        Bar.transform.localScale = new Vector3(currentHoldTime * 2, 0.1f, 1);
    }
    void FixedUpdate()
    {
        if (currentHoldTime > 0 && ((ScriptR.onHold && gameObject == ScriptR.CurrentHold) || (ScriptL.onHold && gameObject == ScriptL.CurrentHold))) { currentHoldTime -= Time.fixedDeltaTime / holdTime; }
        if (currentHoldTime < 0) { currentHoldTime = 0; }
        if (currentHoldTime == 0 && gameObject == ScriptR.CurrentHold) { ScriptR.onHold = false; }
        if (currentHoldTime == 0 && gameObject == ScriptL.CurrentHold) { ScriptL.onHold = false; }

        if (handCol == 0)
        {
            Physics2D.IgnoreCollision(Collider, HandR.GetComponent<CircleCollider2D>(), false);
            Physics2D.IgnoreCollision(Collider, HandL.GetComponent<CircleCollider2D>(), false);
        }
        if (handCol == 1) { Physics2D.IgnoreCollision(Collider, HandL.GetComponent<CircleCollider2D>(), true); }
        if (handCol == 2) { Physics2D.IgnoreCollision(Collider, HandR.GetComponent<CircleCollider2D>(), true); }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "HandR" && handCol == 0) { handCol = 1; }
        if (col.tag == "HandL" && handCol == 0) { handCol = 2; }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if ((col.tag == "HandR" || col.tag == "HandL") && handCol != 0) { handCol = 0; }
    }
}
