using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Status : MonoBehaviour
{

    public PorcentagemVida porcentagemvida;
    public Transform spawnPoint;
<<<<<<< Updated upstream
=======
    public int vidas = 3;
>>>>>>> Stashed changes

    [SerializeField] public float health = 0f;

    public float Damage(float damage){
        this.health += damage;
        porcentagemvida.UpdateHealthText();
        return health;
        
    }


    void Update(){
        if(transform.position.y <= -5){ // Se o player cair do mapa ele morre
<<<<<<< Updated upstream
            MorteJogador.RespawnPlayer(this.gameObject,spawnPoint); //Esse gameObject ou seja o Player
        }
    }   

}
=======
            vidas -= 1;
            if(vidas <= 0){
                if(this.gameObject == GameObject.FindWithTag("Player1"))
                    SceneManager.LoadSceneAsync(3);
                else
                    SceneManager.LoadSceneAsync(2);
            }
            MorteJogador.RespawnPlayer(this.gameObject, spawnPoint); //Esse gameObject ou seja o Player
        }
    }   

}
>>>>>>> Stashed changes
