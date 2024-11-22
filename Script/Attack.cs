using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private GameObject attackArea = default;
    private bool attacking = false;
    private float timeToAttack = 0.5f;
    private float timer = 0f;

    [Header("Attack Key")]
    [SerializeField] private KeyCode attackKey = KeyCode.H;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(attackKey)){
            attack();
        }

        if(attacking){
            timer += Time.deltaTime;

            if(timer >= timeToAttack){
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }

    private void attack(){
        attacking = true;
        attackArea.SetActive(attacking);
    }
}