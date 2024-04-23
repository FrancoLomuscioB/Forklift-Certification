using UnityEngine;
using System.Collections;

public class ForkController : MonoBehaviour {

    public Transform fork; 
    public Transform mast;
    public float speedTranslate; //Platform travel speed
    public Vector3 maxY; //The maximum height of the platform
    public Vector3 minY; //The minimum height of the platform
    public Vector3 maxYmast; //The maximum height of the mast
    public Vector3 minYmast; //The minimum height of the mast
    public Transform rotationTarget;
    public Vector3 maxRotX;
    public Vector3 minRotX;
    public Vector3 maxX;
    public Vector3 minX;


    private bool mastMoveTrue = false; //Activate or deactivate the movement of the mast

    // Update is called once per frame
    void FixedUpdate () {

        Debug.Log(mastMoveTrue);
        Vector3 Rotmax = Vector3.RotateTowards(rotationTarget.forward, maxRotX, speedTranslate * Time.deltaTime, 0.0f);
        Vector3 Rotmin = Vector3.RotateTowards(rotationTarget.forward, minRotX, speedTranslate * Time.deltaTime, 0.0f);
        if (fork.transform.localPosition.y >= maxYmast.y && fork.transform.localPosition.y < maxY.y)
        {
            mastMoveTrue = true;
        }
        else
        {
            mastMoveTrue = false;

        }

        if (fork.transform.localPosition.y <= maxYmast.y)
        {
            mastMoveTrue = false;
        }
      
        if (Input.GetKey(KeyCode.PageUp))
        {
           //fork.Translate(Vector3.up * speedTranslate * Time.deltaTime);
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, maxY, speedTranslate * Time.deltaTime);
            if(mastMoveTrue)
            {
                mast.transform.localPosition = Vector3.MoveTowards(mast.transform.localPosition, maxYmast, speedTranslate * Time.deltaTime);
            }
          
        }
        if (Input.GetKey(KeyCode.PageDown))
        {
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, minY, speedTranslate * Time.deltaTime);
           

            if (mastMoveTrue)
            {
                mast.transform.localPosition = Vector3.MoveTowards(mast.transform.localPosition, minYmast, speedTranslate * Time.deltaTime);

            }

        }
        if (Input.GetKey(KeyCode.Q))
        {
            rotationTarget.rotation = Quaternion.LookRotation(Rotmax);
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationTarget.rotation = Quaternion.LookRotation(Rotmin);

        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, maxX, speedTranslate * Time.deltaTime);
          
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, minX, speedTranslate * Time.deltaTime);
            

        }


    }
}
