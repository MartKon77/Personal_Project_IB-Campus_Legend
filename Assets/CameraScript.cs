using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameScript GameScript;
    GameObject HandR;
    GameObject HandL;
    float midY;
    void Start()
    {
        HandR = GameScript.HandR;
        HandL = GameScript.HandL;
    }
    void Update()
    {
        midY = (HandR.transform.position.y + HandL.transform.position.y) / 2;
        if (midY < 0) { midY = 0; }
        if (!HandR.GetComponent<HandScript>().isControlled && !HandL.GetComponent<HandScript>().isControlled)
        {
            transform.position = new Vector3(0f, midY, -10f);
        }
    }
}
