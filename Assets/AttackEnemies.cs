using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemies : MonoBehaviour
{
    [SerializeField] private KeyCode attackKey = KeyCode.L;
    private Habilities hab;
    private float attackDuration = 0.5f;
    public bool isAttacking = false;
    public bool damage = false;

    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       hab = GetComponent<Habilities>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(attackKey) && !isAttacking)
        {
            StartAttack();
            damage = true;
            //takeDamage();
            //UpdateHealthbar(); 

        }
        else if (Input.GetKeyDown(attackKey))
        {
            damage = true;
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        anim.SetBool("isAttack", true);
        Invoke("EndAttack", attackDuration); // Llama a EndAttack() después de attackDuration segundos
    }

    void EndAttack()
    {
        isAttacking = false;
        anim.SetBool("isAttack", false);
        damage = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entró en contacto tiene el tag "checkpoint"
        Debug.Log(damage);
        if (other.CompareTag("Enemy") && damage)
        {
            Debug.Log("HSK");
            other.gameObject.GetComponent<EnemyBeha>().health -= 50;
        }

    }
}
