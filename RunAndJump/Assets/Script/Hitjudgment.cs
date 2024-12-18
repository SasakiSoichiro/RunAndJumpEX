using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePos : MonoBehaviour
{
	//public Transform checkPoint;
	public bool hasCrossrd = false;     //ü‚ð’´‚¦‚½‚©‚Ç‚¤‚©
	public float LineYposition = 5f;    //’´‚¦‚é‚×‚«ü‚ÌYÀ•W

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            hasCrossrd = true;          //ü‚ð’´‚¦‚½
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Line"))
        {
            hasCrossrd = false;         //ü‚ð’´‚¦‚Ä‚¢‚È‚¢
        }
    }

    //ü‚Ì•¨—“I”»’è
    //private void Update()
    //{
    //    if (transform.position.y>LineYposition)
    //    {
    //        hasCrossrd = true;
    //    }
    //    else
    //    {
    //        hasCrossrd = false;
    //    }
    //}
}
