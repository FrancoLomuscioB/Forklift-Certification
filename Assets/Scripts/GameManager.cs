using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Samples.Hands;

public class GameManager : MonoBehaviour
{
    public float testCounter, conoCounter, choqueCounter;
    public bool conoReverse, yellowBox, redBox, greenBox, instructoAudioPlaying;
    public static GameManager instance;
    public GameObject conoCollider1, conoCollider2, conoCollider3, cono1, cono2, cono3, cono4, yaleObjective1, yaleObjective2, yellowObjective, yaleObjective3, redObjective, greenObjective, yaleObjective4, pushButton;

    public AudioClip[] test;
    public List<AudioClip> instruccionesClips;
    private AudioSource audioActual;
    public AudioSource radioNoises;
    public AudioSource radioBeep;
    public int indiceTest = 0;
    public int indiceInstrucciones = 0;
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
        audioActual = GetComponent<AudioSource>();
        InstruccionesAudios();
        testCounter = 0;
        conoCounter = 0;
        instructoAudioPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (instructoAudioPlaying)
        {
            AvanzarInstruccion();
        }
    }

    public void InstruccionesAudios()
    {
        if (instruccionesClips.Count == 0)
        {
            Debug.Log("asignar una instruccion");
        }
        else
        {
            audioActual.clip = instruccionesClips[indiceInstrucciones];
            radioBeep.Play();
            audioActual.Play();
            radioNoises.Play();
        }
        
    }

    public void AvanzarInstruccion()
    {
        if (!audioActual.isPlaying && indiceInstrucciones == instruccionesClips.Count - 1)
        {
            audioActual.Stop();
            radioNoises.Stop();
            TestAudios();
            instructoAudioPlaying = false;
        }
        else if (!audioActual.isPlaying)
        {
            if ((indiceInstrucciones < instruccionesClips.Count - 1))
            {
                indiceInstrucciones++;
                audioActual.clip = instruccionesClips[indiceInstrucciones];
                audioActual.Play();
                radioNoises.Play(); 
            }
        }
    }
    
    public void AudioButton()
    {
        audioActual.Stop();
    }

    public void TestAudios()
    {
        testCounter = 1;
        pushButton.SetActive(false);
        if (test.Length == 0)
        {
            Debug.Log("asigna un audio");
        }
        else
        {
            audioActual.clip = test[indiceTest];
            audioActual.Play();
        }
    }

    public void TestActualAudio()
    {
        audioActual.Stop();
        indiceTest = (indiceTest + 1) % test.Length;
        audioActual.clip = test[indiceTest];
        audioActual.Play();
    }

    public void ConosTest()
    {
        if (instructoAudioPlaying)
        {
            instructoAudioPlaying = false;
            audioActual.Stop();
            TestAudios();
        }
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
                    TestActualAudio();
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
                    TestActualAudio();
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
            TestActualAudio();
        }
    }

    public void FinalTest()
    {
        if (greenBox && redBox)
        {
            TestActualAudio();
            EndGame();
        }
    }

    public void AddConoCounter()
    {
        conoCounter += 1;
        if (instructoAudioPlaying)
        {
            instructoAudioPlaying = false;
            audioActual.Stop();
            TestAudios();
        }
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
