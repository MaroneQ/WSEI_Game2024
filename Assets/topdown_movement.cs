using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topdown_movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
