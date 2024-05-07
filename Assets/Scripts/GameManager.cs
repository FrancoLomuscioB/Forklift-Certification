using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float testCounter, conoCounter, choqueCounter;
    public bool conoReverse, yellowBox, redBox, greenBox;
    public static GameManager instance;
    public GameObject conoCollider1, conoCollider2, conoCollider3, cono1, cono2, cono3, cono4, yaleObjective1, yaleObjective2, yellowObjective, yaleObjective3, redObjective, greenObjective, yaleObjective4;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        testCounter = 1;
        conoCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConosTest()
    {
        switch (conoReverse)
        {
            case false:
                if(conoCounter == 3)
                {
                    yaleObjective1.SetActive(false);
                    conoCollider1.SetActive(true);
                    conoCollider2.SetActive(true);
                    conoCollider3.SetActive(true);
                    yaleObjective2.SetActive(true);
                    conoReverse = true;
                    conoCounter = 0;
                }
                break;
            case true:
                if(conoCounter == 3)
                {
                    cono1.SetActive(false);
                    cono2.SetActive(false);
                    cono3.SetActive(false);
                    cono4.SetActive(false);
                    yaleObjective2.SetActive(false);
                    testCounter = 2;
                }
                break;
        }
    }

    public void SecondTest()
    {
        if (yellowBox)
        {
            yellowObjective.SetActive(false);
            yaleObjective3.SetActive(false);
            redObjective.SetActive(true);
            greenObjective.SetActive(true);
            yaleObjective4.SetActive(true);
            testCounter = 3;
        }
    }

    public void FinalTest()
    {
        if (greenBox && redBox)
        {
            EndGame();
        }
    }

    public void AddConoCounter()
    {
        conoCounter += 1;
    }

    public void YellowBoxCheck()
    {
        yellowBox = !yellowBox;
    }

    public void RedBoxCheck()
    {
        redBox = !redBox;
    }

    public void GreenBoxCheck()
    {
        greenBox = !greenBox;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Levels");
    }
}
