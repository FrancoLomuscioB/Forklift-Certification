using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YaleMasterCheck : MonoBehaviour
{
    public float number;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(number == 1)
            {
                GameManager.instance.ConosTest();
            }
            if (number == 2)
            {
                GameManager.instance.ConosTest();
            }
            if (number == 3)
            {
                GameManager.instance.SecondTest();
            }
            if (number == 4)
            {
                GameManager.instance.FinalTest();
            }
        }
    }
}
