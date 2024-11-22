using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private float damage = 15f;
    [SerializeField] private float knockback = 10f;
    [SerializeField] private float angle = 135f;
    [SerializeField] private float knockbackDecay = 3f; // Taxa de desaceleração

    private void OnTriggerEnter2D(Collider2D collider){
        Status status = collider.GetComponent<Status>();
        PlayerMovement xd = collider.GetComponent<PlayerMovement>();

/*
        if(!xd.isFacingRight){
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }*/

        float hp = status.Damage(damage);

        xd.Knockback(knockback, angle, knockbackDecay, hp);
    }

}