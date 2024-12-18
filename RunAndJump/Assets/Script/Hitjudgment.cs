using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePos : MonoBehaviour
{
	//public Transform checkPoint;
	public bool hasCrossrd = false;     //線を超えたかどうか
	public float LineYposition = 5f;    //超えるべき線のY座標

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            hasCrossrd = true;          //線を超えた
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Line"))
        {
            hasCrossrd = false;         //線を超えていない
        }
    }

    //線の物理的判定
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
