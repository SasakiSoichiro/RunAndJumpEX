using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePos : MonoBehaviour
{
	//public Transform checkPoint;
	public bool hasCrossrd = false;     //���𒴂������ǂ���
	public float LineYposition = 5f;    //������ׂ�����Y���W

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            hasCrossrd = true;          //���𒴂���
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Line"))
        {
            hasCrossrd = false;         //���𒴂��Ă��Ȃ�
        }
    }

    //���̕����I����
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
