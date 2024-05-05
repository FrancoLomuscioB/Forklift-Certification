using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YaleSounds : MonoBehaviour
{
  public Rigidbody rb; // Referencia al Rigidbody
  public AudioSource audioSource; // Referencia al AudioSource
  public float pitchMultiplier = 0.1f; // Factor de multiplicación para ajustar el pitch

  void Update()
  {
    // Obtener la magnitud de la velocidad del Rigidbody
    float speed = rb.velocity.magnitude;

    // Calcular el nuevo pitch basado en la velocidad
    float newPitch = 1f + speed * pitchMultiplier;
    

    // Limitar el pitch para evitar valores extremadamente altos
    newPitch = Mathf.Clamp(newPitch, 0.1f, 3f);

    // Asignar el nuevo pitch al AudioSource
    audioSource.pitch = newPitch;
  }
}
