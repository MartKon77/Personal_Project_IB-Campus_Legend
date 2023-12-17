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
    [SerializeField] GameObject ClimbWall;
    SpriteRenderer[] BGArray;
    public GameObject WinHold;

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
    void Update()
    {
        if ((Vector3.Distance(HandR.transform.position, WinHold.transform.position) <= 0.5f && HandR.GetComponent<HandScript>().onHold) ||
           (Vector3.Distance(HandL.transform.position, WinHold.transform.position) <= 0.5f && HandL.GetComponent<HandScript>().onHold))
        {
            isWin = true;
        }
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
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
}
