using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _boundsX;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private float _moveInput;
    private float _speed = 10f;

    public BoxCollider2D collider;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _moveInput = Input.GetAxis("Horizontal");
        _sr.flipX = _rb.velocity.x < 0;

        Vector3 temp = transform.position;
        if (temp.x < -_boundsX)
        {
            transform.Translate(new Vector2(2 * _boundsX, 0));
        }
        else if (temp.x > _boundsX)
        {
            transform.Translate(new Vector2(-2 * _boundsX, 0));
        }

        Rigidbody2D playerRb = collider.GetComponent<Rigidbody2D>();
        if (playerRb.velocity.y < 0f)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (_moveInput == 0f) return;
        _rb.velocity = new Vector2(_moveInput * _speed, _rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Finish"))
        {
            GameManager.instance.GameEnd();
        }
    }
}
