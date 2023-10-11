using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hold : MonoBehaviour
{
    [SerializeField] int holdType; //0 - Jug, 1 - Crimp, 2 - Sloper
    [SerializeField] float jugTime;
    [SerializeField] float crimpTime;
    [SerializeField] float sloperTime;
    float holdTime;
    [SerializeField] Texture2D JugSprite;
    [SerializeField] Texture2D CrimpSprite;
    [SerializeField] Texture2D SloperSprite;
    Texture2D HoldSprite;

    SpriteRenderer Sprite;
    float spriteR;
    float spriteG;
    float spriteB;
    void Start()
    {
        Sprite = GetComponentInChildren<SpriteRenderer>();

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

        spriteR = Random.Range(70f, 150f);
        if (Time.fixedDeltaTime % 2 == 0)
        {
            spriteR = spriteR + Random.Range(30f, 50f);
            spriteB = spriteG + Random.Range(30f, 50f);
        }
        else
        {
            spriteG = spriteR - Random.Range(30f, 50f);
            spriteB = spriteG - Random.Range(30f, 50f);
            if (spriteB < 70f)
                spriteB = 70f;
        }
        Sprite.color = new Color(spriteR / 255, spriteG / 255, spriteB / 255);
    }
    void Update()
    {
        
    }
}
