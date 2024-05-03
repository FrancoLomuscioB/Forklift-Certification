using UnityEngine;
using System.Collections;

public class ForkController : MonoBehaviour {

    public Transform fork;
    public Transform mastElev, mastRot;
    public float speedTranslate; //Platform travel speed
    public Vector3 maxY; //The maximum height of the platform
    public Vector3 minY; //The minimum height of the platform
    public Vector3 maxYmast; //The maximum height of the mast
    public Vector3 minYmast; //The minimum height of the mast

    [Header("Rotacion y desplazamiento fork")]
    public float maxX;
    public float minX;

    public float maxRotationAngle;
    public float minRotateAngle;
    public float rotationSpeed;

    public float elevValue, sideValue, rotationValue;

    private bool mastMoveTrue = false;
    private bool moveFork = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        LiftHeight();
        LiftRotation();
        LiftLaterals();
    }

    void LiftLaterals()
    {
        if (sideValue > 0.24f) // mover izquierda
        {
            float targetX = fork.transform.localPosition.x + speedTranslate * sideValue * Time.deltaTime;
            targetX = Mathf.Clamp(targetX, minX, maxX); // Aplicar límites de movimiento
            Debug.Log(targetX);
            fork.transform.localPosition = new Vector3(targetX, fork.transform.localPosition.y, fork.transform.localPosition.z);
        }

        if (sideValue < -0.24f) // mover derecha
        {
            float targetX = fork.transform.localPosition.x + speedTranslate * sideValue * Time.deltaTime;
            targetX = Mathf.Clamp(targetX, minX, maxX); // Aplicar límites de movimiento
            Debug.Log(targetX);
            fork.transform.localPosition = new Vector3(targetX, fork.transform.localPosition.y, fork.transform.localPosition.z);
        }

    }

    void LiftHeight()
    {
        Debug.Log(mastMoveTrue);
        if (fork.transform.localPosition.z >= maxY.z)
        {
            mastMoveTrue = true;
        }
        else
        {
            mastMoveTrue = false;
        }
        if(mastElev.transform.localPosition.z > minYmast.z)
        {
            moveFork = false;
        }
        else
        {
            moveFork = true;
        }
        if (elevValue > 0.24f) // mover arriba
        {
            if (moveFork)
            {
                fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, maxY, speedTranslate * elevValue * Time.deltaTime);
            }
            if (mastMoveTrue)
            {
                mastElev.transform.localPosition = Vector3.MoveTowards(mastElev.transform.localPosition, maxYmast, speedTranslate * elevValue * Time.deltaTime);
            }
        }
        if (elevValue < -0.24f) //mover abajo
        {
            if (moveFork)
            {
                fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, minY, speedTranslate * elevValue * Time.deltaTime * -1f);
            }
            if (mastMoveTrue)
            {
                mastElev.transform.localPosition = Vector3.MoveTowards(mastElev.transform.localPosition, minYmast, speedTranslate * elevValue * Time.deltaTime * -1f);
            }
        }
    }

    void LiftRotation()
    {
        float rotationInput = 0f;

        if (rotationValue > 0.24f)
        {
            rotationInput = -1f;
        }
        else if (rotationValue < -0.24f)
        {
            rotationInput = 1f;
        }

        // Rotar el objeto en el eje X basado en la entrada del jugador
        mastRot.transform.Rotate(Vector3.right, rotationInput * rotationSpeed * rotationValue * Time.deltaTime);

        // Limitar la rotación en el eje X
        float currentXRotation = fork.transform.localEulerAngles.x;

        // Ajustar el ángulo para que esté en el rango de 0 a 360 grados
        if (currentXRotation > 180)
            currentXRotation -= 360;

        // Limitar la rotación entre los valores especificados
        currentXRotation = Mathf.Clamp(currentXRotation, minRotateAngle, maxRotationAngle);

        // Aplicar la nueva rotación al objeto
        mastRot.transform.localEulerAngles = new Vector3(currentXRotation, fork.transform.localEulerAngles.y, fork.transform.localEulerAngles.z);


    }

    public void GetRotValue(float x)
    {
        rotationValue = x;
    }

    public void GetElevValue(float x)
    {
        elevValue = x;
    }

    public void GetSideValue(float x)
    {
        sideValue = x;
    }
}
