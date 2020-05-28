using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Movement/Auto Rotation")]
[RequireComponent(typeof(Rigidbody2D))]
public class AutoRotation : Physics2DObject
{

	// This is the force that rotate the object every frame
	public float rotationSpeed = 0;

	private float currentRotation;
    [HideInInspector]
    public bool bBrake = false;
    [HideInInspector]
    public bool bClutch = false;


    // FixedUpdate is called once per frame
    void FixedUpdate ()
	{
		// Find the right rotation, according to speed
		currentRotation += .02f * rotationSpeed * 10f;

        if (!bClutch)
        {
            // Apply the rotation to the Rigidbody2d
            rigidbody2D.MoveRotation(-currentRotation);
        }
	}

	//Draw an arrow to show the direction in which the object will rotate
	void OnDrawGizmosSelected()
	{
		if(this.enabled)
		{
			Utils.DrawRotateArrowGizmo(transform.position, rotationSpeed);
		}
	}
}
