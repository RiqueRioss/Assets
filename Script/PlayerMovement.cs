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
    private bool isAttacking = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Custom Key Bindings")]
    [SerializeField] public KeyCode moveLeftKey = KeyCode.A;
    [SerializeField] public KeyCode moveRightKey = KeyCode.D;
    [SerializeField] public KeyCode lookUpKey = KeyCode.W;
    [SerializeField] public KeyCode jumpKey = KeyCode.Space;

    public KeyCode getmoveLeftKey(){
        return moveLeftKey;
    }
    public KeyCode getmoveRightKey(){
        return moveRightKey;
    }
    public KeyCode getlookUpKey(){
        return lookUpKey;
    }
    public KeyCode getjumpKey(){
        return jumpKey;
    }


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
            if (isAttacking)
            {
                animator.SetFloat("Speed", 0);
            }
            else {
                animator.SetFloat("Speed", Mathf.Abs(horizontal));
            }


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
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            }

            if (Input.GetKeyUp(jumpKey) && rb.linearVelocity.y > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }

            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!isKnockedBack) // Movimenta apenas quando o knockback não está ativo
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
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

            // Inverte a rotação em Y
            float rotationY = isFacingRight ? 0f : 180f;
            transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
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
        rb.linearVelocity = force * (hp/200);

        setknockbackDecay(kk);
    }

    private void setknockbackDecay(float kkk){
        this.knockbackDecay = kkk;
    }

    private void ApplyKnockbackDeceleration()
    {
        // Reduz gradualmente a velocidade horizontal
        Vector2 velocity = rb.linearVelocity;
        velocity.x = Mathf.MoveTowards(velocity.x, 0f, knockbackDecay * Time.fixedDeltaTime);

        // Atualiza a velocidade do Rigidbody
        rb.linearVelocity = velocity;

        // Desativa o knockback quando a velocidade horizontal for próxima de zero e estiver no chão
        if (Mathf.Abs(velocity.x) < 0.1f && IsGrounded())
        {
            isKnockedBack = false; // Permite movimento normal novamente
        }
    }

    public void TriggerAttack(float duration)
    {
        StartCoroutine(AttackCoroutine(duration));
    }

    private IEnumerator AttackCoroutine(float duration)
    {
        isAttacking = true; // Ativa o estado de ataque
        yield return new WaitForSeconds(duration); // Espera pela duração especificada
        isAttacking = false; // Desativa o estado de ataque
    }

}
