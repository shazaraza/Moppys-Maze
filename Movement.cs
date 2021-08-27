using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// aim of the game - get as many treats and reach the end before time runs out!
public class Movement : MonoBehaviour
{
    public GameObject sprite;
    private Rigidbody2D rb;
    private float speed = 3.0f;

        void Awake()
    {
        rb = sprite.GetComponent<Rigidbody2D>();
        Debug.Log(sprite.transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        rb.freezeRotation = true;
        Vector3 characterRotation = sprite.transform.localScale;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(0.0f, speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(0.0f, -speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(speed, 0.0f);
            characterRotation.x = 1.5f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-speed, 0.0f);
            characterRotation.x = -1.5f;
        }
        sprite.transform.localScale = characterRotation;

    }


}
