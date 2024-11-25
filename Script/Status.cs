using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public PorcentagemVida porcentagemvida;
    public Transform spawnPoint;

    [SerializeField] public float health = 0f;

    public float Damage(float damage){
        this.health += damage;
        porcentagemvida.UpdateHealthText();
        return health;
        
    }


    void Update(){
        if(transform.position.y <= -5){ // Se o player cair do mapa ele morre
            MorteJogador.RespawnPlayer(this.gameObject,spawnPoint); //Esse gameObject ou seja o Player
        }
    }   

}
