using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(inputX, inputY);

        rb.velocity = movement * speed;
    }
}

   /* public int health;
    public Collider healthCheck;
    public string tagName;
    public Text healthDisplay;


    private void OnTriggerEnter()
    {
        if (healthCheck.gameObject.tag == tagName)
        {
            health--;
        }
    }

    private void FixedUpdate()
    {
        healthDisplay.text = "Health: " + health.ToString();
    }
}*/
   