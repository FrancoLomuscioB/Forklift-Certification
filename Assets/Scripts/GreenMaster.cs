using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMaster : MonoBehaviour
{
    public Material blueMat, myMat;

    private void Awake()
    {
        myMat = GetComponent<Renderer>().material;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GreenBox"))
        {
            GameManager.instance.GreenBoxCheck();
            GetComponent<Renderer>().material = blueMat;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GreenBox"))
        {
            GameManager.instance.GreenBoxCheck();
            GetComponent<Renderer>().material = myMat;
        }
    }
}
