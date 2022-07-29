using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _anim;
    [SerializeField] float deadForce = 10f;

    [SerializeField] int healthPoints = 3;
    private bool isDead = false;

    GameObject player = null;
    Vector3 currentScale;
    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            if(_rb.velocity.x == 0)
            {
                _anim.Play("idle");
            }
            else
            {
                _anim.Play("running");
            }
            if(player != null)
            {
                if(player.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(currentScale.x, currentScale.y, currentScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(-currentScale.x, currentScale.y, currentScale.z);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.transform.root.tag == "Player")
        {
            IEnumerator coroutine = takeDamage(other);
            StartCoroutine(coroutine);   
        }

    }

    IEnumerator takeDamage(Collider2D other)
    {
        Color originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
        healthPoints--;
        if(healthPoints <= 0)
        {
            _anim.Play("fall");
            isDead = true;
            bool facingRight = player.GetComponent<CharacterController2D>().m_FacingRight;
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
            
            if(_rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(currentScale.x, currentScale.y, currentScale.z);
            }
            else if(_rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-currentScale.x, currentScale.y, currentScale.z);
            }
        }   
    }
}
