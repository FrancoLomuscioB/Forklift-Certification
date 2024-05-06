using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YaleSounds : MonoBehaviour
{
  public Rigidbody forkRb; // Referencia al Rigidbody
  public AudioSource audioSource; // Referencia al AudioSource
  public AudioSource beepSound;
  public AudioSource beepSound2;
  public AudioSource beepSound3;
  public float pitchMultiplier = 0.1f; // Factor de multiplicación para ajustar el pitch
  public ForkController fork;

  

  void Start()
  {
    
  }

  void Update()
  {
    EngineSounds();
    BeepSounds();
    HorquillaSounds();
  }

  void EngineSounds()
  {
    // Obtener la magnitud de la velocidad del Rigidbody
    float speed = forkRb.velocity.magnitude;
    
    // Calcular el nuevo pitch basado en la velocidad
    float newPitch = 1f + speed * pitchMultiplier;
    

    // Limitar el pitch para evitar valores extremadamente altos
    newPitch = Mathf.Clamp(newPitch, 0.1f, 3f);

    // Asignar el nuevo pitch al AudioSource
    audioSource.pitch = newPitch;
    
  }

  void BeepSounds()
  {
    float direction = Vector3.Dot(forkRb.velocity, transform.forward);

    if (direction > -0.3)
    {
      beepSound.Play();
    }
    else
    {
      beepSound.Stop();
    }
    
  }

  void HorquillaSounds()
  {
    if (fork.forkBeep2)
    {
      beepSound2.Play();
      Debug.Log("bep2");
    }
   

    if (fork.forkBeep)
    {
      Debug.Log("bep3");
      beepSound3.Play();
    }
    
  }


}
