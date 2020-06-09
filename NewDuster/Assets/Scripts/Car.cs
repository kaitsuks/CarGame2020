using UnityEngine;
using System.Collections;

namespace KaitsuCar { 

    public class Car : MonoBehaviour
    {
        public float upForce;                    //Upward force of the "flap".
        private bool isDead = false;            //Has the player collided with a wall?

        private Animator anim;                    //Reference to the Animator component.
        private Rigidbody2D rb2d;                //Holds a reference to the Rigidbody2D component of the bird.

        public GameObject maisema1;
        public GameObject maisema2;
        public GameObject tausta1;
        public GameObject tausta2;

        private Transform maisema1T;
        private Transform maisema2T;
        private Transform tausta1T;
        private Transform tausta2T;

        private Rigidbody2D maisema1RB;
        private Rigidbody2D maisema2RB;
        private Rigidbody2D tausta1RB;
        private Rigidbody2D tausta2RB;

        public float speed = 1f; //etuala 1 / distant 0.1
        public float speedFactor = 0.02f; //etuala 0.2 / distant -0.1
        private Vector2 direction = new Vector2(0f, 0f);

        void Start()
        {
            //Get reference to the Animator component attached to this GameObject.
            anim = GetComponent<Animator>();
            //Get and store a reference to the Rigidbody2D attached to this GameObject.
            rb2d = GetComponent<Rigidbody2D>(); //AUTON
            maisema1T = maisema1.transform;
            maisema2T = maisema2.transform;
            tausta1T = tausta1.transform;
            tausta2T = tausta2.transform;

            maisema1RB = maisema1.GetComponent<Rigidbody2D>();
            maisema2RB = maisema2.GetComponent<Rigidbody2D>();
            tausta1RB = tausta1.GetComponent<Rigidbody2D>();
            tausta2RB = tausta2.GetComponent<Rigidbody2D>();

        }

        void Update()
        {
            speed = KKSGameControl.instance.scrollSpeed;
            //Don't allow control if the car is destroyed.
            if (isDead == false)
            {
                //Look for input to trigger a "flap".
                if (Input.GetMouseButtonDown(0))
                {
                    //...tell the animator about it and then...
                    anim.SetTrigger("Flap");
                    //...zero out the birds current y velocity before...
                    rb2d.velocity = Vector2.zero;
                    //    new Vector2(rb2d.velocity.x, 0);
                    //..giving the bird some upward force.
                    rb2d.AddForce(new Vector2(0, upForce));
                }
            }

            maisema2T.position = maisema1T.position + new Vector3(20f, 0, 0);
            tausta2T.position = tausta1T.position + new Vector3(20f, 0, 0);
        }

        void FixedUpdate()
        {
            //if (rb2D.velocity.x < 0) direction = -direction;
            //speed = rb2D.velocity.magnitude;
            direction = maisema1RB.velocity;
            direction.y = 0f;
            //direction.z = 0f; //Vector2:ssa ei ole z
            direction.x = -direction.x / 10;
            speed = maisema1RB.velocity.magnitude;
            //Debug.Log(" speed etuala" + speed);
            //      if (relativeToRotation)
            //{
            //	rigidbody2D.AddRelativeForce(direction * speed * speedFactor);
            //}
            //else
            //{
            //	rigidbody2D.AddForce(direction * speed * speedFactor);
            //}

            tausta1T.Translate(direction.x * speedFactor, 0, 0);

        }

        void OnCollisionEnter2D(Collision2D other)
        {
            // Zero out the bird's velocity
            rb2d.velocity = Vector2.zero;
            // If the bird collides with something set it to dead...
            //isDead = true;
            //...tell the Animator about it...
            // anim.SetTrigger("Die"); // TODO
            //...and tell the game control about it.
            //KKSGameControl.instance.CarDestroyed();
        }
    }

}