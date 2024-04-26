using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float currentSteeringAngle;
    LogitechGSDK.LogiControllerPropertiesData properties;
    public float gasInput, breakInput, clutchInput;

    private void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
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
        float motor = maxMotorTorque * gasInput;
        float steering = maxSteeringAngle * currentSteeringAngle;
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
                if(motor < 0)
                {
                    motor = 0;
                }
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
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
