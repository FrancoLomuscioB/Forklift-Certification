using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowMaster : MonoBehaviour
{
    public Material blueMat, myMat;

    private void Awake()
    {
        myMat = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("YellowBox"))
        {
            GameManager.instance.YellowBoxCheck();
            GetComponent<Renderer>().material = blueMat;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("YellowBox"))
        {
            GameManager.instance.YellowBoxCheck();
            GetComponent<Renderer>().material = myMat;
        }
    }
}
