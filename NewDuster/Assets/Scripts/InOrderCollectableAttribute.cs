using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Attributes/InOrderCollectable")]
//[RequireComponent(typeof(Collider2D))]
public class InOrderCollectableAttribute : MonoBehaviour
{
	public int orderNumber = 0;
    private AlbiAutoCollection collection;

	// Start is called at the beginning
	private void Start()
	{
		// Find the Collection in the scene and store a reference for later use
        collection = GameObject.FindObjectOfType<AlbiAutoCollection>();
        Debug.Log("Collection HAETTU hnr = " + collection.highNumber);        
    }

	// This function gets called everytime this object collides with another
	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
		string playerTag = otherCollider.gameObject.tag;
        Debug.Log("Collision orderNumber = " + orderNumber);

        // is the other object a player?
        if (playerTag == "Player" || playerTag == "Player2")
		{            
            if (collection != null)
            {
                // add one item if in this is collected in order
                if (orderNumber == collection.highNumber)
                {
                    // then destroy this object
                    Debug.Log("Destroying orderNumber = " + orderNumber);
                    collection.AddNumber(orderNumber);
                    Destroy(gameObject);
                    
                }
            }            
		}
	}
}
