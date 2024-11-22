using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private float damage = 15f;
    private float knockback = 50f;

    private void OnTriggerEnter2D(Collider2D collider){
        Status status = collider.GetComponent<Status>();
        status.Damage(damage);

        PlayerMovement xd = collider.GetComponent<PlayerMovement>();
        xd.knockback(knockback);
    }

}