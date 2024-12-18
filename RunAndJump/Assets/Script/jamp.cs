using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jamp : MonoBehaviour
{
    private float rayDistance;
    private float speed = 6.0f;
    private float jumpSpeed = 8.0f;
    private float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private PlayerContoroller controller;
    private float startPos;
    private float groundPos;
    public float jumpDistance;



    void Start()
    {
        controller = GetComponent<PlayerContoroller>();
        rayDistance = 1.0f;
    }

    void Update()
    {
        //Raycastによる接地判定
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);
        bool isFloor = Physics.Raycast(ray, rayDistance);
        Debug.DrawRay(rayPosition, Vector3.down * rayDistance, Color.red);

        //CharacterControllerを使ったジャンプ移動
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        controller.moveDir = (moveDirection * Time.deltaTime);

        //ジャンプした距離の計算
        if (isFloor)
        {
            startPos = this.transform.position.x;
        }
        else
        {
            groundPos = this.transform.position.x;
            jumpDistance = groundPos - startPos;
            Debug.Log(jumpDistance);
        }
    }

}
