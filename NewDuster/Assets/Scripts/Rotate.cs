using UnityEngine;
using System.Collections;



namespace KaitsuCar { 

    [AddComponentMenu("Playground/Movement/Rotate")]
[RequireComponent(typeof(Rigidbody2D))]

    public class Rotate : Physics2DObject
    {
        [Header("Input keys")]
        public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;

        [Header("Rotation")]
        public float speed = 5f;
        public float speedFactor = 5f;

        private float spin;

        //public GameObject etuala;
        //float speedCar;
        //public GameObject car;
        //private Rigidbody2D m_rigidbody;


        private void Start()
        {
            //speedCar = etuala.GetComponent<AutoMove>().speed;
            //m_rigidbody = car.GetComponent<Rigidbody2D>();
        }


        // Update gets called every frame
        void Update()
        {
            // Register the spin from the player input
            // Moving with the arrow keys
            if (typeOfControl == Enums.KeyGroups.ArrowKeys)
            {
                spin = Input.GetAxis("Horizontal");
            }
            else
            {
                spin = Input.GetAxis("Horizontal2");
            }
            speed = -KaitsuCar.KKSGameControl.instance.scrollSpeed * speedFactor; //TESTpoisto
        }


        // FixedUpdate is called every frame when the physics are calculated
        void FixedUpdate()
        {
            // Apply the torque to the Rigidbody2D
            rigidbody2D.AddTorque(-spin * speed);
            //speedCar = m_rigidbody.velocity.magnitude * 6f;
            //Debug.Log(" Car velocity " + m_rigidbody.velocity.magnitude);
            //Debug.Log(" speedCar " + speedCar);

            //Debug.Log(" SPIN " + spin);


        }
    }
}
