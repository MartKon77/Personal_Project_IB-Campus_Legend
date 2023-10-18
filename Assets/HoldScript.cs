using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hold : MonoBehaviour
{
    [SerializeField] GameScript GameScript;
    [SerializeField] Transform MouseTracker;
    [SerializeField] GameObject HandR;
    [SerializeField] GameObject HandL;
    [SerializeField] GameObject Bar;
    HandScript ScriptR;
    HandScript ScriptL;
    BoxCollider2D Collider;
    SpriteRenderer BarSprite;

    [SerializeField] float grabRange;
    bool canRegrab;
    float holdTime;
    float currentHoldTime;

    [SerializeField] int holdType; // 0 - Jug, 1 - Crimp, 2 - Sloper
    // Different Hold Properties:
    float jugTime;
    float crimpTime;
    float sloperTime;
    Sprite JugSprite;
    Sprite CrimpSprite;
    Sprite SloperSprite;
    Sprite HoldSprite;

    [SerializeField] SpriteRenderer SpriteRender;
    float spriteR;
    float spriteG;
    float spriteB;
    void Start()
    {
        jugTime = GameScript.jugTime;
        crimpTime = GameScript.crimpTime;
        sloperTime = GameScript.sloperTime;
        JugSprite = GameScript.JugSprite;
        CrimpSprite = GameScript.CrimpSprite;
        SloperSprite = GameScript.SloperSprite;

        ScriptR = HandR.GetComponent<HandScript>();
        ScriptL = HandL.GetComponent<HandScript>();
        BarSprite = Bar.GetComponent<SpriteRenderer>();
        Collider = gameObject.GetComponent<BoxCollider2D>();
        Collider.size = Vector2.one * (grabRange * 2);
        canRegrab = true;

        if (holdType == 0)
        {
            holdTime = jugTime;
            HoldSprite = JugSprite;
        }
        else if(holdType == 1)
        {
            holdTime = crimpTime;
            HoldSprite = CrimpSprite;
        }
        else
        {
            holdTime = sloperTime;
            HoldSprite = SloperSprite;
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
        if (Input.GetKeyUp(KeyCode.Mouse0) && Vector3.Distance(transform.position, HandR.transform.position) <= grabRange && Vector3.Distance(transform.position, MouseTracker.position) <= grabRange && canRegrab)
        {
            ScriptR.onHold = true;
            currentHoldTime = 1f;
        }
        if (ScriptR.onHold && gameObject == ScriptR.CurrentHold)
        {
            Bar.SetActive(true);
        }
        else if(!ScriptR.onHold && gameObject != ScriptR.CurrentHold)
        {
            Bar.SetActive(false);
        }
        Bar.transform.localScale = new Vector3(currentHoldTime * 2, 0.1f, 1);
    }
    void FixedUpdate()
    {
        if (currentHoldTime > 0 && ScriptR.onHold && gameObject == ScriptR.CurrentHold) { currentHoldTime -= Time.fixedDeltaTime / holdTime; }
        if (currentHoldTime < 0) { currentHoldTime = 0; }
        if (currentHoldTime == 0 && gameObject == ScriptR.CurrentHold) { ScriptR.onHold = false; }
    }
}
