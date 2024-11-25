using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 6f;
    private float jumpingPower = 20f;
    public bool isFacingRight = true;
    private bool isKnockedBack = false; // Estado de knockback

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Custom Key Bindings")]
    [SerializeField] private KeyCode moveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode moveRightKey = KeyCode.D;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private float knockbackDecay; // Taxa de desaceleração

    [SerializeField] public Animator animator;
    [SerializeField] private Transform spriteHolder;

    // Update is called once per frame
    void Update()
    {
        if (!isKnockedBack) // Permite controle apenas se o knockback não estiver ativo
        {
            if (Mathf.Abs(horizontal) != 0)
            {
                horizontal *= 0.9f; // Reduz gradualmente a velocidade
            }

            if (Mathf.Abs(horizontal) < 0.01f)
            {
                horizontal = 0f; // Para completamente quando muito lento
            }

            // Atualiza o valor de Speed no Animator
            animator.SetFloat("Speed", Mathf.Abs(horizontal));

            // Custom Key Bindings para Movimento
            if (Input.GetKey(moveLeftKey))
            {
                horizontal = -1f;
            }
            else if (Input.GetKey(moveRightKey))
            {
                horizontal = 1f;
            }

            // Jump Handling
            if (Input.GetKeyDown(jumpKey) && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (Input.GetKeyUp(jumpKey) && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isKnockedBack) // Movimenta apenas quando o knockback não está ativo
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            ApplyKnockbackDeceleration(); // Aplica desaceleração durante o knockback
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void Knockback(float kb, float angle, float kk, float hp)
    {
        isKnockedBack = true; // Ativa o estado de knockback

        // Converte o ângulo de graus para radianos
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calcula a direção a partir do ângulo
        Vector2 knockbackDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;

        // Calcula a força na direção
        Vector2 force = knockbackDirection * kb;

        // Aplica a força ao Rigidbody2D , quanto maior a vida mais longe vai
        rb.velocity = force * (hp/200);

        setknockbackDecay(kk);
    }

    private void setknockbackDecay(float kkk){
        this.knockbackDecay = kkk;
    }

    private void ApplyKnockbackDeceleration()
    {
        // Reduz gradualmente a velocidade horizontal
        Vector2 velocity = rb.velocity;
        velocity.x = Mathf.MoveTowards(velocity.x, 0f, knockbackDecay * Time.fixedDeltaTime);

        // Atualiza a velocidade do Rigidbody
        rb.velocity = velocity;

        // Desativa o knockback quando a velocidade horizontal for próxima de zero e estiver no chão
        if (Mathf.Abs(velocity.x) < 0.1f && IsGrounded())
        {
            isKnockedBack = false; // Permite movimento normal novamente
        }
    }
}
