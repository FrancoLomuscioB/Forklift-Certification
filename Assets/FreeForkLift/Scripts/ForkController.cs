using UnityEngine;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;

public class ForkController : MonoBehaviour {

    public Transform fork;
    public Transform mast;
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
    
    





    private bool mastMoveTrue = false; //Activate or deactivate the movement of the mast
    private bool mastXMoveTrue = false;

    // Update is called once per frame
    void FixedUpdate ()
    {

        LiftHeight();
        LiftRotation();
        LiftLaterals();



    }

    void LiftLaterals()
    {
        if (Input.GetKey(KeyCode.Alpha1)) // mover izquierda
        {
            float targetX = fork.transform.localPosition.x - speedTranslate * Time.deltaTime;
            targetX = Mathf.Clamp(targetX, minX, maxX); // Aplicar límites de movimiento
            fork.transform.localPosition = new Vector3(targetX, fork.transform.localPosition.y, fork.transform.localPosition.z);
        }

        if (Input.GetKey(KeyCode.Alpha3)) // mover derecha
        {
            float targetX = fork.transform.localPosition.x + speedTranslate * Time.deltaTime;
            targetX = Mathf.Clamp(targetX, minX, maxX); // Aplicar límites de movimiento
            fork.transform.localPosition = new Vector3(targetX, fork.transform.localPosition.y, fork.transform.localPosition.z);
        }

        
        
    }

    void LiftHeight()
    {
       
        if (fork.transform.localPosition.z >= maxYmast.y && fork.transform.localPosition.z < maxY.z)
        {
            mastMoveTrue = true;
        }
        else
        {
            mastMoveTrue = false;
         
        }

        if (fork.transform.localPosition.z <= maxYmast.y)
        {
            mastMoveTrue = false;
        }
       
      
        if (Input.GetKey(KeyCode.PageUp)) // mover arriba
        {
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, maxY, speedTranslate * Time.deltaTime);
            if(mastMoveTrue)
            {
                mast.transform.localPosition = Vector3.MoveTowards(mast.transform.localPosition, maxYmast, speedTranslate * Time.deltaTime);
            }
          
        }
        if (Input.GetKey(KeyCode.PageDown)) //mover abajo
        {
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, minY, speedTranslate * Time.deltaTime);

            if (mastMoveTrue)
            {
                mast.transform.localPosition = Vector3.MoveTowards(mast.transform.localPosition, minYmast, speedTranslate * Time.deltaTime);

            }

        }
        
    }

    void LiftRotation()
    {
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            rotationInput = -1f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rotationInput = 1f;
        }

        // Rotar el objeto en el eje X basado en la entrada del jugador
        fork.transform.Rotate(Vector3.right, rotationInput * rotationSpeed * Time.deltaTime);

        // Limitar la rotación en el eje X
        float currentXRotation = fork.transform.localEulerAngles.x;

        // Ajustar el ángulo para que esté en el rango de 0 a 360 grados
        if (currentXRotation > 180)
            currentXRotation -= 360;

        // Limitar la rotación entre los valores especificados
        currentXRotation = Mathf.Clamp(currentXRotation, minRotateAngle, maxRotationAngle);

        // Aplicar la nueva rotación al objeto
        fork.transform.localEulerAngles = new Vector3(currentXRotation, fork.transform.localEulerAngles.y, fork.transform.localEulerAngles.z);
        
        
    }





}
