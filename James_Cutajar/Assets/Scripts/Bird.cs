using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    [SerializeField] float launchForce = 500;
    [SerializeField] float maxDragDistance = 5;


    Vector2 startPosition;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    void OnMouseDown()
    {
        spriteRenderer.color = Color.red;
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = startPosition - currentPosition;
        direction.Normalize();

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * launchForce);

        spriteRenderer.color = Color.white;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //limit to drag
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, startPosition);
        if (distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - startPosition;
            direction.Normalize();
            desiredPosition = startPosition + (direction * maxDragDistance);
        }

        if (desiredPosition.x > startPosition.x)
            desiredPosition.x = startPosition.x;

        _rigidbody2D.position = desiredPosition;
    }

    //reset bird position
    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidbody2D.position = startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
