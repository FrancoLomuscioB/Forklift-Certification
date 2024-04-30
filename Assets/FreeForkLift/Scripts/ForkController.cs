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
    public Transform targetRotaion;
    public Vector3 maxRotX;
    public Vector3 minRotX;
    public Vector3 maxX;
    public Vector3 minX;
    public float elevValue, sideValue, rotationValue;





    private bool mastMoveTrue = false; //Activate or deactivate the movement of the mast
    private bool mastXMoveTrue = false;

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.Log(mastMoveTrue);
        Vector3 Rotmax = Vector3.RotateTowards(targetRotaion.forward, maxRotX, speedTranslate * Time.deltaTime, 0.0f);
        Vector3 Rotmin = Vector3.RotateTowards(targetRotaion.forward, minRotX, speedTranslate * Time.deltaTime, 0.0f);
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
            if (mastMoveTrue)
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
            targetRotaion.rotation = Quaternion.LookRotation(Rotmax);
            if (mastMoveTrue)
            {
                mast.transform.localPosition = Vector3.MoveTowards(mast.transform.localPosition, maxYmast, speedTranslate * Time.deltaTime);
            }

        }
        if (Input.GetKey(KeyCode.E))
        {
            targetRotaion.rotation = Quaternion.LookRotation(Rotmin);
            if (mastMoveTrue)
            {
                mast.transform.localPosition = Vector3.MoveTowards(mast.transform.localPosition, minYmast, speedTranslate * Time.deltaTime);

            }

        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            fork.transform.localPosition += Vector3.left * speedTranslate * Time.deltaTime;


        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            // fork.transform.localPosition = Vector3.MoveTowards(fork.transform.localPosition, minX, speedTranslate * Time.deltaTime);
            fork.transform.localPosition += Vector3.right * speedTranslate * Time.deltaTime;


        }


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
