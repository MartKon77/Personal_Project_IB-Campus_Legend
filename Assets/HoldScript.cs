using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    public class HoldClass
    {
        public float holdTime;
    }
    void Start()
    {
        HoldClass Crimp = new HoldClass();
        Crimp.holdTime = 5f;
    }
    void Update()
    {
        
    }
}
