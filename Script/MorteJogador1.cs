using UnityEngine;
using System.Collections;

public class MorteJogador : MonoBehaviour
{ 

    public static void RespawnPlayer(GameObject player, Transform spawnPoint){
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
    }
}