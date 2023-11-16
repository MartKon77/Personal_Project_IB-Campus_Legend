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

    [Header("Game Objects")]
    public Transform MouseTracker;
    public GameObject HandR;
    public GameObject HandL;

    [Header("Hand properties")]
    [Range(0f, 1f)]
    public float controlRange;
    public float handReach;

    [Header("Hold Properties")]
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
}
