using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public SimpleCarController yaleCar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!GameManager.instance.conoReverse)
            {
                if(yaleCar.currentGear == 1f)
                {
                    GameManager.instance.AddConoCounter();
                    gameObject.SetActive(false);
                }
            }
            if (GameManager.instance.conoReverse)
            {
                if (yaleCar.currentGear == -1f)
                {
                    GameManager.instance.AddConoCounter();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
