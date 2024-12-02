using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private float damage = 15f;
    [SerializeField] private float knockback = 10f;
    [SerializeField] private float angle = 135f;
    [SerializeField] private float angle2 = 135f;
    [SerializeField] private float knockbackDecay = 3f; // Taxa de desaceleração

    private void OnTriggerEnter2D(Collider2D collider){
        Status status = collider.GetComponent<Status>();
        PlayerMovement xd = collider.GetComponent<PlayerMovement>();

        float hp = status.Damage(damage);

        bool face = xd.getisFacingRight();

        if(face){
            xd.Knockback(knockback, angle, knockbackDecay, hp);
        }
        else{
            xd.Knockback(knockback, angle2, knockbackDecay, hp);
        }
    }

}