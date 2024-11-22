using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public PorcentagemVida porcentagemvida;

    [SerializeField] public float health = 0f;

    public float Damage(float damage){
        this.health += damage;
        porcentagemvida.UpdateHealthText();
        return health;
        
    }

}
