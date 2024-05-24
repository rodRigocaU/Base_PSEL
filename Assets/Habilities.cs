using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Habilities : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Corpo;
    [SerializeField] private KeyCode gravityKey = KeyCode.K;
    [SerializeField] private KeyCode attackKey = KeyCode.L;
    [SerializeField] private Movimento movi;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform healthbar;
    [SerializeField] private Transform healthbar2;
    [SerializeField] private Collider2D bodyCollider;


    
    [SerializeField] public Camera mainCamera;
    [SerializeField] public LineRenderer _lineRenderer;
    [SerializeField] public DistanceJoint2D _distaceJoint;
    [SerializeField] public int keys;

    public float recoilForce = 500f; 
    public float recoilDuration = 1.5f;



    public Transform player;
    public float ganchoVelocidad = 10f;
    public float distanciaMaxima = 10f;

    private Vector2 direccionGancho;

    private float attackDuration = 0.5f;
    public bool isAttacking = false;
    private int health = 100;
    private float initialHealthbarWidth;
    private Transform lastCheckPoint;

    public float damageCooldown = 1.0f; // Cooldown time of taking damage
    private bool isOnCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        keys = 0;
        movi = GetComponent<Movimento>();
        initialHealthbarWidth = healthbar.localScale.x;
        movi.gravityUpD = 1f;

        _distaceJoint.enabled = false;
    }


    void Update()
    {
        if(health <= 0)
        {
            isDead();
        }
        if(Input.GetKeyDown(gravityKey))
        {
            changeGravity();
        }

        //HOOK CODE
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _lineRenderer.SetPosition(0, mousePos);
            _lineRenderer.SetPosition(1, transform.position);
            _distaceJoint.connectedAnchor = mousePos;
            _distaceJoint.enabled = true;
            _lineRenderer.enabled = true;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distaceJoint.enabled = false;
            _lineRenderer.enabled = false;
        }

        if(_distaceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }

        if(isOnCooldown)
        {
            healthbar2.gameObject.SetActive(true);
        }
        else
        {
            healthbar2.gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("CheckPoint"))
        {
            // CheckPoint
            lastCheckPoint = other.transform;
           
        }

        if (other.CompareTag("key"))
        {
            keys += 1;
            Debug.Log("LLaves" + keys);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Enemy") && !isOnCooldown)
        {
            takeDamage(other.gameObject.GetComponent<EnemyBeha>().power_damage);

            // Cooldown of receiving damage
            StartCoroutine(DamageCooldown());


        }

        if (other.CompareTag("limit"))
        {
            
            takeDamage(100);

        }
        if (other.CompareTag("spikes"))
        {
           
            takeDamage(15);
            StartCoroutine(DamageCooldown());

        }

        if(other.CompareTag("doors") && keys >= 4)
        {
            Debug.Log("Emtresdfdssd");
            Debug.Log(keys);
            other.gameObject.GetComponent<DoorAnim>().StarAnimation();
        }




    }



    void changeGravity()
    {
        movi.gravityUpD *= -1; 
        Corpo.gravityScale *= -1;

        Vector3 scale = transform.localScale;

        // Change the gravity by -1
        scale.y *= -1;

        
        transform.localScale = scale;

    }

    void StartAttack()
    {
        isAttacking = true;
        anim.SetBool("isAttack", true);
        Invoke("EndAttack", attackDuration); // Function to attack animation
    }

    void EndAttack()
    {
        isAttacking = false;
        anim.SetBool("isAttack", false);
    }
    void takeDamage(int damage = 20)
    {

        health -= damage;
        UpdateHealthbar();
    }

    void UpdateHealthbar()
    {
        // Scale health Bar
        float newScaleX = initialHealthbarWidth * ((float)health / 100f);

        // Update healthBar
        healthbar.localScale = new Vector3(newScaleX, healthbar.localScale.y, healthbar.localScale.z);
        healthbar2.localScale = new Vector3(newScaleX, healthbar.localScale.y, healthbar.localScale.z);

    }

    void isDead()
    {
        health = 100;
        UpdateHealthbar();
        transform.position = lastCheckPoint.position;
        
    }

    IEnumerator DamageCooldown()
    {
        isOnCooldown = true;

        yield return new WaitForSeconds(damageCooldown);

        isOnCooldown = false;
    }


}
