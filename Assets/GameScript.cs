using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameScript : MonoBehaviour
{
    [Header("Game variables")]
    [SerializeField] float fixedDeltaTime;
    [SerializeField] float timeScale;
    public bool isWin;

    [Header("Different Hold Properties")]
    public float jugTime;
    public float crimpTime;
    public float sloperTime;
    public Sprite JugSprite;
    public Sprite CrimpSprite;
    public Sprite SloperSprite;
    void Start()
    {
        Time.fixedDeltaTime = fixedDeltaTime;
        Time.timeScale = timeScale;
        isWin = false;
    }
    void Update()
    {
        
    }
}
