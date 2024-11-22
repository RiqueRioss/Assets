using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{

    [SerializeField] public float health = 0f;

    public float Damage(float damage){
        this.health += damage;
        return health;
        
    }

}
