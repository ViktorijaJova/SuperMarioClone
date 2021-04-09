using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
   private Animator m_Animator;
   public float Speed = 5.0f;
   public float JumpSpeed = 0.5f;
   private Transform m_GroundCheck;
   public LayerMask GroundLayers;

   private void Start()
   {
      m_Animator = GetComponent<Animator>();
      m_GroundCheck = transform.FindChild("GroundCheck");
   }

   private void FixedUpdate()
   {
      bool isGrounded = Physics2D.OverlapPoint(m_GroundCheck.position,GroundLayers);

     if( Input.GetButton("Jump"))
     {

        if (isGrounded)
        {
           GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpSpeed,ForceMode2D.Impulse);
           isGrounded = false;
        }
      }
     m_Animator.SetBool("IsGrounded",isGrounded);

      float hSpeed = Input.GetAxis("Horizontal");
      m_Animator.SetFloat("Speed",Mathf.Abs(hSpeed));

      if (hSpeed > 0)
      {
         transform.localScale = new Vector3(-1, 1, 1);
      }
      else if(hSpeed < 0)
      {
         transform.localScale = new Vector3(1, 1, 1);
      }

     GetComponent<Rigidbody2D>().velocity = new Vector2(hSpeed * Speed,GetComponent<Rigidbody2D>().velocity.y);
      

   }
}
