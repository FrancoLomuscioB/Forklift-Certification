using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque, gasInput, breakInput, clutchInput, maxSteeringAngle, currentSteeringAngle, currentGear, motorForce, rigiVelocity;
    public Rigidbody yaleRigi;
    LogitechGSDK.LogiControllerPropertiesData properties;

    private void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
        currentGear = 0;
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
        if(rigiVelocity == 0)
        {
            if(clutchInput == 1)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    currentGear = 1;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    currentGear = 0;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    currentGear = -1;
                }
            }
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
        else
        {
            print("No Steering Wheel connected!");
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
                axleInfo.leftWheel.brakeTorque = 0;
                axleInfo.rightWheel.brakeTorque = 0;
            }
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                if(motorForce < 0)
                {
                    motorForce = 0;
                }
                axleInfo.leftWheel.motorTorque = motorForce;
                axleInfo.rightWheel.motorTorque = motorForce;
            }
        }
    }

    public void GetAngle(float f)
    {
        currentSteeringAngle = (2*f - 1);
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
