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

    public Rigidbody2D autoBody;
    Vector2 carVelocity;
    float carSpeed;
    public Text speedText;

    public Toggle reverse;
    private bool bReverse;
    private int revFactor = 1;
    private bool bBrake;
    private bool bClutch;

    //moottori ja vaihteet
    public float moottoriTeho = 0;
    //välityssuhteet
    public float vaihde1 = 3.545f;
    public float vaihde2 = 1.9f;
    public float vaihde3 = 1.3f;
    public float vaihde4 = 1.0f;
    public float pakki = 3.2f;
    public float vetopyorasto = 4.0f;
    private float kardaani = 1;

    //vaihenappulat
    public Toggle v1;
    public Toggle v2;
    public Toggle v3;
    public Toggle v4;

    //vaihde booleanit
    private bool bv1 = false;
    private bool bv2 = false;
    private bool bv3 = false;
    private bool bv4 = false;


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
        moottoriTeho++;
        frontRot.rotationSpeed = moottoriTeho/ kardaani * revFactor;
        rearRot.rotationSpeed = moottoriTeho / kardaani * revFactor;
    }

    public void DecreaseGas()
    {
        moottoriTeho--;
        frontRot.rotationSpeed = moottoriTeho / kardaani * revFactor;
        rearRot.rotationSpeed = moottoriTeho / kardaani * revFactor;
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
        carVelocity = autoBody.velocity;
        carSpeed = carVelocity.x;
        carSpeed = carSpeed * -60;
        int iCarSpeed = (int)carSpeed;
        //speedText.text = "AUTON NOPEUS " + iCarSpeed.ToString();
        //Debug.Log("nopeus " + carSpeed);

        //pyörästä
        //int vauhti = (int)(moottoriTeho / kardaani * revFactor);
        //speedText.text = "AUTON NOPEUS " + vauhti.ToString();

        int vauhti = (int)(moottoriTeho / kardaani * revFactor);
        speedText.text = "AUTON NOPEUS " + vauhti.ToString();

        if (bv1)
        {
            kardaani = vaihde1;
        }

        if (bv2)
        {
            kardaani = vaihde2;
        }

        if (bv3)
        {
            kardaani = vaihde3;
        }

        if (bv4)
        {
            kardaani = vaihde4;
        }

        Debug.Log("nopeus " + kardaani);

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

    public void Vaihda1()
    {
       if (v1.isOn)
        {
            bv1 = false;
            bv3 = false;
            bv3 = false;
            bv4 = false;

            bv1 = true;
        }
    }

    public void Vaihda2()
    {
       if (v2.isOn)
        {
            bv1 = false;
            bv3 = false;
            bv3 = false;
            bv4 = false;

            bv2 = true;
        }
    }

    public void Vaihda3()
    {
       if (v3.isOn)
        {
            bv1 = false;
            bv3 = false;
            bv3 = false;
            bv4 = false;

            bv3 = true;
        }
    }

    public void Vaihda4()
    {
        if (v4.isOn)
        {
            bv1 = false;
            bv3 = false;
            bv3 = false;
            bv4 = false;

            bv4 = true;
        }
    }
}
