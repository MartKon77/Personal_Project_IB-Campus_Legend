using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    public GameObject ClimbWall;
    SpriteRenderer[] BGArray;

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

        BGArray = ClimbWall.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer x in BGArray) { x.color = new Color(Random.Range(212f, 222f) / 255, Random.Range(124f, 134f) / 255, Random.Range(68f, 78f) / 255); }
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
