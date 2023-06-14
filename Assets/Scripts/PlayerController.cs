using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    // input-muuttujat
    private float horizontalMovement;

    // liikkumismuuttujat
    private float moveSpeed = 5f;
    private Vector2 movement = new Vector2();

    // hyppääminen
    public float jumpForce = 5f;
    private bool grounded;

    // animaatio
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // input
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        //Debug.Log(horizontalMovement);
        movement.x = horizontalMovement * moveSpeed;
        // hyppääminen
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        // hahmon kääntäminen
        if (horizontalMovement > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (horizontalMovement < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        // animaatio
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
    }
    void FixedUpdate()
    {
        transform.Translate(movement * Time.deltaTime);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
        }
    }
}
