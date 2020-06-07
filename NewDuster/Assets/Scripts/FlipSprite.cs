using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour {

    SpriteRenderer m_SpriteRenderer;
    //The Color to be assigned to the Renderer’s Material
    public Color m_NewColor;
    bool flipped = false;
    public GameObject gameObject;

    //These are the values that the Color Sliders return
    float m_Red, m_Blue, m_Green;

    // Use this for initialization
    void Start()
    {
        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //Set the GameObject's Color quickly to a set Color (blue)
       // m_SpriteRenderer.color = Color.red;
        Debug.Log("Startissa");
    }

    public void Flip()
    {
        if (!flipped)
        {
            m_SpriteRenderer.color = Color.white;
            m_SpriteRenderer.flipX = true;
            flipped = true;
        }
        else
        if (flipped)
        {
            //m_SpriteRenderer.color = Color.red;
            m_SpriteRenderer.flipX = false;
            flipped = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}