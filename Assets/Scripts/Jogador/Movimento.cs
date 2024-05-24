using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [SerializeField] private float Velocidade, AlturaPulo;
    [SerializeField] private Transform PeDoPersonagem;
    [SerializeField] private LayerMask Chao;
    [SerializeField] private Animator anim;
    
    
    //O corpo do jogador
    [SerializeField] private Rigidbody2D Corpo;
    //Para ele n�o pular infinitamente
    private bool PodePular = false;
    private bool facingRight = true;
    private int saltosRestantes = 1;
    [SerializeField] private float ForcaPulo;


    //Dash Variables
    public float dashDistance = 5f;
    public float dashCooldown = 0.3f;
    public float dashSpeed = 10f;

    private bool isDashing = false;
    private bool canDash = true;
    private float lastDashTime = 0f;
    private Vector2 dashDirection;

    public float gravityUpD = 1f;
    

    //Running 
    private float runSpeedMultiplier = 1f;
    public KeyCode runKey = KeyCode.LeftShift;



    void Update()
    {





       // Debug.Log("Funco?");
        //Se a barra de espa�o foi pressionada e o jogador pode pular
        if(Input.GetKeyDown(KeyCode.Space) && (PodePular || saltosRestantes > 0))
        {
            //Adiciona uma for�a para cima proporcional � AlturaPulo

            Debug.Log(gravityUpD);

            saltosRestantes -= 1;
            Corpo.velocity = new Vector2(Corpo.velocity.x, ForcaPulo * gravityUpD);
            //Corpo.AddForce(new Vector2(ForcaPulo, AlturaPulo));
            //Pro�be o jogador de pular
            PodePular = false;
        }
        //Define a velocidade do corpo baseada na tecla pressionada (Input.GetAxisRaw("Horizontal"))
        //A fun��o retorna 1 se a seta pra direita ou D foram pressionados
        //Retorna 0 se a seta da esquerda ou A foram pressionados
        //Neste caso, n�o se usa Time.deltaTime, porque RigidBody2D.velocity j� opera baseado na taxa de frames


        if (Input.GetKeyDown(runKey))
        {

            anim.SetBool("isRunningPress", true);
            runSpeedMultiplier = 2f;
        }
        if (Input.GetKeyUp(runKey))
        {
            anim.SetBool("isRunningPress", false);
            runSpeedMultiplier = 1f;
        }

        


        float movimento_horizontal = Velocidade * runSpeedMultiplier * Input.GetAxisRaw("Horizontal");


        // Obtén la escala actual del sprite
        Vector3 scale = transform.localScale;

        
        if (movimento_horizontal < 0.0 && facingRight)
        {
            Flip();
        }
        else if(movimento_horizontal > 0.0 && !facingRight)
        {
            Flip();
        }

        anim.SetFloat("Speed", Mathf.Abs(movimento_horizontal));
        Corpo.velocity = new Vector2(movimento_horizontal, Corpo.velocity.y);

        //Cria uma caixa, se a caixa colidir com o chao, pode pular
        //Nessa função se passa a posição, tamanho, angulo e distancia(tamanho) em relação a direção
        //Tambem passa um layer mask, pra que somente os layers associados a Chao sejam considerados
        bool PertoDoChao = Physics2D.BoxCast(PeDoPersonagem.position, new Vector2(0.5f, 0.2f), 0f, Vector2.down, 0.1f, Chao);
       


        //Se o acerto tem um resultado não nulo, pode pular
        if (PertoDoChao)
        {
            //Debug.Log("Funco2?");
            saltosRestantes = 1;
            PodePular = true;
        }
        else //Caso contrário, não se pode pular
        {
            PodePular = false;
        }
        
        //Se pode pular e o jogador apertou espaço
        if (PodePular && Input.GetKeyDown(KeyCode.Space) )
        {
            //Adiciona uma força proporcional a ForcaPulo pra cima
            Corpo.AddForce(Vector2.up * ForcaPulo);
        }


        //Dash Handle
        if (Input.GetKeyDown(KeyCode.J) && canDash)
        {
            Dash();
        }

        // Si está en el dash, aplicar el movimiento
        if (isDashing)
        {
            // Lerp para suavizar la transición del dash
            //Corpo.velocity = Vector2.Lerp(Corpo.velocity, dashDirection * dashSpeed, Time.deltaTime * 10f);
            Corpo.velocity = new Vector2(transform.localScale.x * dashDistance, Corpo.velocity.y);
        }




    }
    void Flip()
    {
        // Flip the character
        facingRight = !facingRight;

        
        Vector3 scale = transform.localScale;

        // Change the orientation in de axis x
        scale.x *= -1;

        
        transform.localScale = scale;
    }

    void Dash()
    {
        isDashing = true;
        canDash = false;
        lastDashTime = Time.time;

        
        Corpo.velocity = new Vector2(Corpo.velocity.x, 0f);

        dashDirection = new Vector2(transform.localScale.x, 0f);

        // Dash
        Invoke("EndDash", 0.1f);
    }

    void EndDash()
    {
        isDashing = false;
        // Apply velocity in the air
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.1f))
        {
            Corpo.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Velocidade, Corpo.velocity.y);
        }
    }
    void FixedUpdate()
    {
        // Pode Fazer um Dash
        if (!canDash && Time.time - lastDashTime >= dashCooldown)
        {
            canDash = true;
        }
    }
}
