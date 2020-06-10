using UnityEngine;
using System.Collections;

namespace KaitsuCar { 

    public class KKSColumn : MonoBehaviour
    {
        public bool bRoof = false;
        public bool bDiamond = false;
        public bool bHappenOnlyOnce = true;
        [HideInInspector]
        public bool bCollided = false;
        private BoxCollider2D boxCollider2D;
        private CapsuleCollider2D capsuleCollider2D;
        private Rigidbody2D rb2D;
        void OnTriggerEnter2D(Collider2D other)
        {
            if ((other.GetComponent<Car>() != null) && !bDiamond && !bCollided)
            {
                //If the bird hits the trigger collider in between the columns then
                //tell the game control that the bird scored.
                //GameControl.instance.CarDestroyed();
                KKSGameControl.instance.CarCondition();
                bCollided = true;
                Invoke("MakeDead", 1f);
                
            }
            //else // "VAARALLINEN"
            //{
                if ((other.GetComponent<Car>() != null) && !bCollided && bDiamond)
                {
                    KKSGameControl.instance.AddDiamonds(); //LISÄTÄÄN SCOREA
                    bCollided = true;
                }
            //}

            if (other.GetComponent<EdgeCollider2D>() != null && bRoof)
            {
                //If the bird hits the trigger collider in between the columns then
                //tell the game control that the bird scored.
                //GameControl.instance.CarDestroyed();
                KKSGameControl.instance.CarDestroyed();
            }
            
        }

        public void MakeDead()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            boxCollider2D.enabled = false;
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            capsuleCollider2D.enabled = false;
            rb2D = GetComponent<Rigidbody2D>();
            rb2D.gravityScale = 0f;
        }
    }

}
