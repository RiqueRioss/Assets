using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Status : MonoBehaviour
{

    public PorcentagemVida porcentagemvida;
    public Transform spawnPoint;
    public int vidas = 3;

    [SerializeField] public float health = 0f;
    [SerializeField] public PlayerMovement player;

    public float Damage(float damage){
        this.health += damage;
        porcentagemvida.UpdateHealthText();
        return health;
        
    }


    void Update(){
        if(transform.position.y <= -5){ // Se o player cair do mapa ele morre
            vidas -= 1;
            if(vidas <= 0){
                if(this.gameObject == GameObject.FindWithTag("Player1"))
                    SceneManager.LoadSceneAsync(3);
                else
                    SceneManager.LoadSceneAsync(2);
            }
            MorteJogador.RespawnPlayer(this.gameObject, spawnPoint); //Esse gameObject ou seja o Player
            player.stopHorizontal();
            health = 0;
        }
    }   

}
