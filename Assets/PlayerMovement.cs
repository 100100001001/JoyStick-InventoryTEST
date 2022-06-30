using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xMove;
    public float yMove;
    public float moveSpeed = 5f;

    public VirtualJoyStick virtualJoyStick;

    private PlayerInput playerInput;

    private Rigidbody2D playerRigid;

    private SpriteRenderer rend;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Vector2 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        // playerRigid.MovePosition(playerRigid.position + moveDistance);

        // 좌우반전

        xMove = virtualJoyStick.horizontal;
        yMove = virtualJoyStick.vertical;
        
        if (xMove > 0) rend.flipX = false;
        else if (xMove < 0) rend.flipX = true;

        //transform.Translate(playerInput.move * moveSpeed * Time.deltaTime, 0, 0);
        //transform.Translate(xMove * moveSpeed * Time.deltaTime, 0, 0);
        //transform.Translate(playerInput.move * moveSpeed * Time.deltaTime, 0, 0);

        transform.Translate(xMove * moveSpeed * Time.deltaTime, yMove * moveSpeed * Time.deltaTime, 0);
        
    }
}
