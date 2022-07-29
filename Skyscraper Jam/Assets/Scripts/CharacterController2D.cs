using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CharacterController2D : MonoBehaviour
{
	public Animator anim;
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	private bool hitAnimFinished = true;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
	}
	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			}
		}
	}


	public void Move(float move, bool jump)
	{
		if ((m_Grounded || m_AirControl))
		{
			if(m_Grounded && hitAnimFinished)
			{
				if(m_Rigidbody2D.velocity.y == 0){
					if(move!=0)
					{
						anim.Play("running");
					}
					else
					{
						anim.Play("idle");
					}
				}
			}
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		if (m_Grounded && jump && m_Rigidbody2D.velocity.y <= 1)
		{
			float extraJumpForce = 0;
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce + extraJumpForce));	
		}

		if(!m_Grounded && hitAnimFinished)
		{
			anim.Play("jump");
		}
	}

    public void Hit()
	{
		string[] hitAnims = {"hit1", "hit2", "hit3"};
		int rand = Random.Range(0, hitAnims.Length);
		if(hitAnimFinished)
		{	
			hitAnimFinished = false;
			anim.Play(hitAnims[rand]);
			StartCoroutine("WaitForHit");
		}
	}

	IEnumerator WaitForHit()
	{
		yield return new WaitForSeconds(0.5f);
		hitAnimFinished = true;
	}

	private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}