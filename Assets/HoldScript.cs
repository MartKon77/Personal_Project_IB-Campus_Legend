using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hold : MonoBehaviour
{
    [SerializeField] Transform MouseTracker;
    [SerializeField] GameObject HandR;
    HandScript ScriptR;
    BoxCollider2D Collider;
    [SerializeField] GameObject Bar;
    [SerializeField] SpriteRenderer BarSprite;

    [SerializeField] float grabRange;
    bool canRegrab;

    [SerializeField] int holdType; // 0 - Jug, 1 - Crimp, 2 - Sloper
    [Header("Different Hold Properties")]
    [SerializeField] float jugTime;
    [SerializeField] float crimpTime;
    [SerializeField] float sloperTime;
    float holdTime;
    float currentHoldTime;
    [SerializeField] Sprite JugSprite;
    [SerializeField] Sprite CrimpSprite;
    [SerializeField] Sprite SloperSprite;
    Sprite HoldSprite;

    [SerializeField] SpriteRenderer SpriteRender;
    float spriteR;
    float spriteG;
    float spriteB;
    void Start()
    {
        ScriptR = HandR.GetComponent<HandScript>();
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
