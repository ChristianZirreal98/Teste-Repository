using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moviment : MonoBehaviour
{
    [SerializeField] int movement_speed;
    [SerializeField] int rotation_speed;
    
    Vector2 movement;
    
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));        

        move.Normalize();     

        movement = move * movement_speed;

        if (movement != Vector2.zero)
        {
            Quaternion torotation = Quaternion.LookRotation(Vector3.forward, -move);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, torotation, rotation_speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.Player_Is_Not_Death)
        {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
        
    }
}
