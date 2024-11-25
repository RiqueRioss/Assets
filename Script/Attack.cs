using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] private GameObject attackArea = default;
    private bool attacking = false;
    private float startlag;
    private float timeToAttack = 0.5f;
=======
    [SerializeField] private GameObject area_a_parado = default;
    [SerializeField] private float area_a_parado_t = 0.7f;
    [SerializeField] private GameObject area_a_frente = default;
    [SerializeField] private float area_a_frente_t = 0.4f;
    [SerializeField] private GameObject area_a_cima = default;
    [SerializeField] private float area_a_cima_t = 0.4f;

    [SerializeField] public Animator animator;
    [SerializeField] private Transform spriteHolder;

    [SerializeField] private PlayerMovement player;

    private bool is_a_p = false;
    private bool is_a_f = false;
    private bool is_a_c = false;

    private float startlag;
>>>>>>> Stashed changes
    private float timer = 0f;

    [SerializeField] public KeyCode moveLeftKey;
    [SerializeField] public KeyCode moveRightKey;
    [SerializeField] public KeyCode lookUpKey;
    [SerializeField] public KeyCode jumpKey;

    [Header("Attack Key")]
    [SerializeField] private KeyCode attackKey = KeyCode.H;

    // Start is called before the first frame update
    void Start()
    {
        area_a_parado = transform.GetChild(0).gameObject;
        area_a_frente = transform.GetChild(0).gameObject;
        area_a_cima = transform.GetChild(0).gameObject;

        moveLeftKey = player.getmoveLeftKey();
        moveRightKey = player.getmoveRightKey();
        lookUpKey = player.getlookUpKey();
        jumpKey = player.getjumpKey();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("A_Parado", is_a_p);
        animator.SetBool("A_Frente", is_a_f);
        animator.SetBool("A_Cima", is_a_c);

        if(Input.GetKeyDown(attackKey) && (Input.GetKey(moveLeftKey) || Input.GetKey(moveRightKey))){
            a_frente();
        }
        else if(Input.GetKeyDown(attackKey) && Input.GetKey(lookUpKey)){
            a_cima();
        }
        else if(Input.GetKeyDown(attackKey)){
            a_parado();
        }

        if(is_a_p){
            timer += Time.deltaTime;

            if(timer >= area_a_parado_t){
                timer = 0;
                is_a_p = false;
                area_a_parado.SetActive(is_a_p);
            }
        }
        if(is_a_f){
            timer += Time.deltaTime;

            if(timer >= area_a_frente_t){
                timer = 0;
                is_a_f = false;
                area_a_frente.SetActive(is_a_f);
            }
        }
        if(is_a_c){
            timer += Time.deltaTime;

            if(timer >= area_a_cima_t){
                timer = 0;
                is_a_c = false;
                area_a_cima.SetActive(is_a_c);
            }
        }
    }

    private void a_parado(){
        is_a_p = true;
        area_a_parado.SetActive(is_a_p);
        player.TriggerAttack(area_a_parado_t);
    }
    private void a_frente(){
        is_a_f = true;
        area_a_frente.SetActive(is_a_f);
        player.TriggerAttack(area_a_frente_t);
    }
    private void a_cima(){
        is_a_c = true;
        area_a_cima.SetActive(is_a_c);
        player.TriggerAttack(area_a_cima_t);
    }
}