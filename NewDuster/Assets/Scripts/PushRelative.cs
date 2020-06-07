using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Movement/PushRelative")]
[RequireComponent(typeof(Rigidbody2D))]
public class PushRelative : Physics2DObject
{
	[Header("Input key")]

	// the key used to activate the push
	public KeyCode key = KeyCode.Space;

	[Header("Direction and strength")]

	// strength of the push, and the axis on which it is applied (can be X or Y)
	public float pushStrength = 5f;
	public Enums.Axes axis = Enums.Axes.Y;
	public bool relativeAxis = true;

    [Header("Steering keys")]
    public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;

    [Header("Movement")]
    [Tooltip("Speed of movement")]
    public Enums.MovementType movementType = Enums.MovementType.OnlyHorizontal;

    [Header("Orientation")]
    public bool orientToDirection = true;
    // The direction that will face the player
    public Enums.Directions lookAxis = Enums.Directions.Up;

    private Vector2 movement, cachedDirection;
    private float moveHorizontal;
    private float moveVertical;


    private bool keyPressed = false;
	private Vector2 pushVector; //TYÖNTÖVOIMAVEKTORI
    private Vector2 directionVector; //SUUNTAVEKTORI

    [Header("Rotation")]
    public float speed = 5f;

    private float spin; //AJONEUVON SUUNTA

    private void Start()
    {
        directionVector = new Vector2(0f, 0f);
    }

    // Read the input from the player
    void Update()
	{
		keyPressed = Input.GetKey(key);

        if (typeOfControl == Enums.KeyGroups.ArrowKeys)
        {
            spin = Input.GetAxis("Horizontal");
        }
        else
        {
            spin = Input.GetAxis("Horizontal2");
        }


        //// Moving with the arrow keys
        //if (typeOfControl == Enums.KeyGroups.ArrowKeys)
        //{
        //    moveHorizontal = Input.GetAxis("Horizontal");
        //    moveVertical = Input.GetAxis("Vertical");
        //}
        //else
        //{
        //    moveHorizontal = Input.GetAxis("Horizontal2");
        //    moveVertical = Input.GetAxis("Vertical2");
        //}

        ////zero-out the axes that are not needed, if the movement is constrained
        //switch (movementType)
        //{
        //    case Enums.MovementType.OnlyHorizontal:
        //        moveVertical = 0f;
        //        break;
        //    case Enums.MovementType.OnlyVertical:
        //        moveHorizontal = 0f;
        //        break;
        //}

        //movement = new Vector2(moveHorizontal, moveVertical);
        ////if(movement == Vector2.zero) animator.SetTrigger("To Standing");



        ////rotate the GameObject towards the direction of movement
        ////the axis to look can be decided with the "axis" variable
        //if (orientToDirection)
        //{
        //    if (movement.sqrMagnitude >= 0.01f)
        //    {
        //        cachedDirection = movement;
        //    }
        //    Utils.SetAxisTowards(lookAxis, transform, cachedDirection);
        //}

        directionVector.x = spin; //SUUNTAVEKTORILLE AUTON SUUNTA
    }


	// FixedUpdate is called every frame when the physics are calculated
	void FixedUpdate()
	{
        rigidbody2D.AddTorque(-spin * speed); //KÄÄNNETÄÄN AUTOA

        if (keyPressed)
		{
			pushVector = Utils.GetVectorFromAxis(axis) * pushStrength + directionVector; //TYÖNTÖSUUNTA
            //LASKETAAN 

			//Apply the push
			if(relativeAxis)
			{
				rigidbody2D.AddRelativeForce(pushVector);
			}
			else
			{
				rigidbody2D.AddForce(pushVector);
			}
		}
	}

	//Draw an arrow to show the direction in which the object will move
	void OnDrawGizmosSelected()
	{
		if(this.enabled)
		{
			float extraAngle = (relativeAxis) ? transform.rotation.eulerAngles.z : 0f;
			pushVector = Utils.GetVectorFromAxis(axis) * pushStrength;
			Utils.DrawMoveArrowGizmo(transform.position, pushVector, extraAngle, pushStrength * .5f);
		}
	}
}
