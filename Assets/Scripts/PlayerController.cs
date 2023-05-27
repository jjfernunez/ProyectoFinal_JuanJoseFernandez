using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // Velocidad de movimiento del personaje
    public float groundCheckRadius = 0.2f; // Radio de comprobación de colisión con el suelo
    public Transform groundCheck; // Transform del objeto utilizado para comprobar la colisión con el suelo
    public LayerMask whatIsGround; // Capa de objetos que representan el suelo

    private Rigidbody2D rb; // Componente Rigidbody2D del personaje
    private Animator anim; // Componente Animator del personaje
    private bool isGrounded; // Indica si el personaje está tocando el suelo
    private Animator animator;
    public int spriteSize;

    private void Awake()
    {
        PlayerPrefs.SetInt("idCharacter", 1);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponentInChildren<Animator>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        moveSpeed = this.gameObject.GetComponent<StatsLogic>().speed.GetValue();
       
    }

    private void Update()
    {
        if (moveSpeed != this.gameObject.GetComponent<StatsLogic>().speed.GetValue())
            moveSpeed = this.gameObject.GetComponent<StatsLogic>().speed.GetValue();
    }

    void FixedUpdate()
    {
        

        if (!GameManager.gameIsPaused)
        {
            float moveInputHorizontal = Input.GetAxisRaw("Horizontal");
            float moveInputVertical = Input.GetAxisRaw("Vertical");
            Vector2 moveInput = new Vector2(moveInputHorizontal, moveInputVertical).normalized;
            rb.velocity = moveInput * moveSpeed;

            if (moveInputHorizontal > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (moveInputHorizontal < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        anim.SetFloat("speed", rb.velocity.magnitude);
        
    }
}
