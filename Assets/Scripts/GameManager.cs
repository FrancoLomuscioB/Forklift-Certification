using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float testCounter, conoCounter;
    public bool conoReverse;
    public GameManager instance;

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
    }

    // Update is called once per frame
    void Update()
    {
        switch (testCounter)
        {
            case 1:
                ConosTest();
                break;

            case 2:
                SecondTest();
                break;

            case 3:
                FinalTest();
                break;

        }
    }

    public void ConosTest()
    {

    }

    public void SecondTest()
    {

    }

    public void FinalTest()
    {

    }

    public void AddConoCounter()
    {
        conoCounter += 1;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Levels");
    }
}
