using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelUIScript : MonoBehaviour
{
    [SerializeField] GameScript GameScript;
    [SerializeField] GameObject GameScreen;
    [SerializeField] GameObject WinScreen;
    [SerializeField] GameObject LevelMenu;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject HandBar;
    [SerializeField] Button ContBut;
    [SerializeField] Button HelpBut;
    [SerializeField] Button BackBut;
    public float handDistance;
    public float handVelocity;
    void Update()
    {
        handVelocity = GameScript.HandR.GetComponent<Rigidbody2D>().velocity.y;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (LevelMenu.activeInHierarchy)
            {
                Time.timeScale = 0f;
                LevelMenu.SetActive(false);
                PauseMenu.SetActive(true);
            }
            else
            {
                LevelMenu.SetActive(true);
                PauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        if (GameScript.isWin)
        {
            GameScreen.SetActive(false);
            WinScreen.SetActive(true);
        }

        if(GameScript.HandR.transform.position.x >= GameScript.HandL.transform.position.x)
        {
            handDistance = GameScript.HandR.transform.position.y - GameScript.HandL.transform.position.y;
            HandBar.transform.localScale = new Vector3(1f, (handDistance / GameScript.handReach) * 200f, 1f);
        }
        else
        {
            handDistance = GameScript.HandL.transform.position.y - GameScript.HandR.transform.position.y;
            HandBar.transform.localScale = new Vector3(1f, (handDistance / GameScript.handReach) * 200f, 1f);
        }
    }
}
