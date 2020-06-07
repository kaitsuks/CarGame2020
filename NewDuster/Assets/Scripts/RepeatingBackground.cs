using UnityEngine;
using System.Collections;

namespace KaitsuCar
{
    public class RepeatingBackground : MonoBehaviour
    {

        private EdgeCollider2D groundEdgeCollider;        //This stores a reference to the collider attached to the Ground.
        private float groundHorizontalLength;        //A float to store the x-axis length of the collider2D attached to the Ground GameObject.
        private BoxCollider2D groundCollider;
        public bool extraOffset = false;
        private float eOffset = 0f;
        //public Transform car; //auto pysyy paikoillaan!
        public float speedFactor = 1f;


        //Awake is called before Start.
        private void Awake()
        {
            //Get and store a reference to the collider2D attached to Ground.
            groundEdgeCollider = GetComponent<EdgeCollider2D>();
            groundCollider = GetComponent<BoxCollider2D>();
            //Store the size of the collider along the x axis (its length in units).
            //groundHorizontalLength = groundEdgeCollider.bounds.size.x /8f;
            //groundHorizontalLength = groundEdgeCollider.bounds.size.x/2;
            groundHorizontalLength = groundCollider.size.x;
            if (extraOffset)
            {
                eOffset = groundHorizontalLength;
            }
        }

        //Update runs once per frame
        private void FixedUpdate()
        {
            //Debug.Log("X = " + transform.position.x + " ExtraOffset " + extraOffset);
            //Debug.Log("GroundLength = " + groundHorizontalLength + " ExtraOffset " + extraOffset);
            //Check if the difference along the x axis between the main Camera and the position of the object this is attached to is greater than groundHorizontalLength.
            if (transform.position.x < -groundHorizontalLength * 1f)
            //if (transform.position.x < 0.0f)
            {
                Debug.Log("NYT TAPAHTUU X = " + transform.position.x + " ExtraOffset " + extraOffset);
                //If true, this means this object is no longer visible and we can safely move it forward to be re-used.
                RepositionBackground();
            }
        }

        //Moves the object this script is attached to right in order to create our looping background effect.
        private void RepositionBackground()
        {
            //This is how far to the right we will move our background object, in this case, twice its length. This will position it directly to the right of the currently visible background object.
            Vector2 groundOffSet = new Vector2((groundHorizontalLength + eOffset) * 1f, 0);

            //Move this object from it's position offscreen, behind the player, to the new position off-camera in front of the player.
            transform.position = (Vector2)transform.position + groundOffSet;
        }
    }

}
