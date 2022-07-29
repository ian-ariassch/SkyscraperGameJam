using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float deadForce = 10f;

    private int healthPoints = 3;
    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.transform.root.tag == "Player")
        {
            healthPoints--;
            if(healthPoints <= 0)
            {
                CharacterController2D player = other.gameObject.GetComponentInParent<CharacterController2D>();
                bool facingRight = player.m_FacingRight;
                switch(other.gameObject.tag)
                {
                    case "hitOne":
                        _rb.AddForce(new Vector2(facingRight ? 1 : -1, 0) * deadForce, ForceMode2D.Impulse);
                        break;
                    case "hitTwo":
                        _rb.gravityScale = 3;
                        _rb.AddForce(new Vector2(facingRight ? 0.2f : -0.2f, 2) * deadForce, ForceMode2D.Impulse);
                        break;
                    case "hitThree":
                        _rb.AddForce(new Vector2(facingRight ? 1 : -1, 0) * deadForce * 2, ForceMode2D.Impulse);
                        break;
                }
            }            
        }

    }
}
