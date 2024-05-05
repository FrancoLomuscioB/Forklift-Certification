using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Content.Interaction;



public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque, gasInput, breakInput, clutchInput, maxSteeringAngle, currentSteeringAngle, currentGear, motorForce, rigiVelocity;
    public bool handBrake;
    public Rigidbody yaleRigi;
    LogitechGSDK.LogiControllerPropertiesData properties;
    public XRJoystick marchasLever;
    

    private void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
        currentGear = 0;
        handBrake = true;
    }
    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void Update()
    {
        rigiVelocity = yaleRigi.velocity.magnitude;
        if (handBrake)
        {
            breakInput =+ 10;
        }
        if(rigiVelocity < 20)
        {
            if(clutchInput == 1)
            {
                marchasLever.gameObject.SetActive(true);
            }
        }
        else
        {
            marchasLever.gameObject.SetActive(false);
        }
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            gasInput = (rec.lY - 32767)/(-65534f);
            if(rec.lRz > 0)
            {
                breakInput = 0;
            }
            if(rec.lRz < 0)
            {
                breakInput = 1;
            }
            if (rec.rglSlider[0] > 0)
            {
                clutchInput = 0;
            }
            if (rec.rglSlider[0] < 0)
            {
                clutchInput = 1;
            }
        }
    }

    public void FixedUpdate()
    {
        float steering = maxSteeringAngle * currentSteeringAngle;
        switch (currentGear)
        {
            case 1:
                motorForce = maxMotorTorque * gasInput;
                break;

            case 0:
                motorForce = 0;
                break;

            case -1:
                motorForce = maxMotorTorque * gasInput * -1;
                break;
        }
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (breakInput == 1)
            {
                axleInfo.leftWheel.brakeTorque = maxMotorTorque;
                axleInfo.rightWheel.brakeTorque = maxMotorTorque;
            }
            if (breakInput == 0)
            {
                if (!handBrake)
                {
                    axleInfo.leftWheel.brakeTorque = 0;
                    axleInfo.rightWheel.brakeTorque = 0;
                }
            }
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                
                axleInfo.leftWheel.motorTorque = motorForce;
                axleInfo.rightWheel.motorTorque = motorForce;
            }
        }
    }

    public void GetAngle(float f)
    {
        currentSteeringAngle = (2*f - 1);
    }

    public void GetMarchasValue(float x)
    {
        if (x < (-0.34))
        {
            currentGear = -1;
        }
        else if (x < 0.34f)
        {
            currentGear = 0;
        }
        else if (0.34f < x)
        {
            currentGear = 1;
        }
    }

    public void GetHandBrake(bool b)
    {
        handBrake = b;
    }
}


[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}
