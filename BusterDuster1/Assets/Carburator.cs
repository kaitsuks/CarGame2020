using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carburator : MonoBehaviour
{
    public GameObject frontWheel;
    public GameObject rearWheel;
    private AutoRotation frontRot;
    private AutoRotation rearRot;
    public Toggle reverse;
    private bool bReverse;
    private int revFactor = 1;
    private bool bBrake;
    private bool bClutch;

    // Start is called before the first frame update
    void Start()
    {
        frontRot = frontWheel.GetComponent<AutoRotation>();
        rearRot = rearWheel.GetComponent<AutoRotation>();
    }

    public void IncreaseGas()
    {
        bBrake = false;
        frontRot.bBrake = false;
        rearRot.bBrake = false;
        frontRot.bClutch = false;
        rearRot.bClutch = false;
        frontRot.rotationSpeed += 1f * revFactor;
        rearRot.rotationSpeed += 1f * revFactor;
    }

    public void DecreaseGas()
    {
        frontRot.rotationSpeed -= 1f * revFactor;
        rearRot.rotationSpeed -= 1f * revFactor;
    }

    public void SetReverse()
    {
        if (reverse.isOn)
        {
            bReverse = true; revFactor = -1;
            frontRot.rotationSpeed = -frontRot.rotationSpeed;
            rearRot.rotationSpeed = -rearRot.rotationSpeed;
        }
        else
        {
            bReverse = false; revFactor = 1;
            frontRot.rotationSpeed = -frontRot.rotationSpeed;
            rearRot.rotationSpeed = -rearRot.rotationSpeed;
        }
    }

    public void SetClutch()
    {
        bClutch = true;
        frontRot.bClutch = true;
        rearRot.bClutch = true;
    }

    public void SetBrake()
    {
        bBrake = true;
        frontRot.bBrake = true;
        rearRot.bBrake = true;
        frontRot.bClutch = false;
        rearRot.bClutch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bBrake)
        {
            if (!bReverse)
            {
                frontRot.rotationSpeed -= 1f;
                rearRot.rotationSpeed -= 1f;

                if (frontRot.rotationSpeed < 0)
                {
                    frontRot.rotationSpeed = 0;
                }

                if (rearRot.rotationSpeed < 0)
                {
                    rearRot.rotationSpeed = 0;
                }

            }

            if (bReverse)
            {
                frontRot.rotationSpeed += 1f;
                rearRot.rotationSpeed += 1f;

                if (frontRot.rotationSpeed > 0)
                {
                    frontRot.rotationSpeed = 0;
                }

                if (rearRot.rotationSpeed > 0)
                {
                    rearRot.rotationSpeed = 0;
                }

            }
        }
    }
}
