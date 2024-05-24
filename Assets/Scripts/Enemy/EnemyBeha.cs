using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeha : MonoBehaviour
{

    public float velocidadMovimientoLenta = 1.6f; 
    public float tiempoAccion = 4.2f; // Duration of each state of behaviur of the enemy

    private Animator animador;
    private bool moviendose = false;
    private float direccion;
    private float tiempoSiguienteAccion = 0f;
    private Vector3 initial;
    public int power_damage;
    public int health;

    void Start()
    {
        health = 100;
        animador = GetComponent<Animator>();

        // Program the next aleatory move
        ProgramarSiguienteAccion();
        initial = transform.localScale;
        
    }

    void Update()
    {
        // Verify wich actions is doing right now
        if (Time.time >= tiempoSiguienteAccion)
        {
            
            if (moviendose)
            {
                // Stay in the same place
                Quieto();
            }
            else
            {
                //Move aleatoriamente
               
                MoverseAleatoriamente();
            }

            ProgramarSiguienteAccion();
        }
        else
        {
            
            float velocidadMovimiento = Random.Range(velocidadMovimientoLenta * 0.5f, velocidadMovimientoLenta * 1.5f);

            // Compute the posiiton of the enemy
            Vector3 nuevaPosicion = transform.position + new Vector3(direccion * velocidadMovimiento * Time.deltaTime, 0f, 0f);

            
            transform.position = nuevaPosicion;

            
        }
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }

    }

    void ProgramarSiguienteAccion()
    {
        // Compute the next time of the next action to do
        tiempoSiguienteAccion = Time.time + tiempoAccion;

        //Choose if we are going to move or stay in the same place

        moviendose = Random.Range(0, 2) == 0; // 0: Stay, 1: Move
        
    }

    void MoverseAleatoriamente()
    {
        // Change the animation to move
        animador.SetBool("Move", true);

        // Move to the right or the left?
        direccion = Random.Range(0f, 1f) > 0.5f ? 1f : -1f;

        if(direccion == 1)
        {
            
            Vector3 scale = transform.localScale;

            
            scale.x *= -1;

            transform.localScale = scale;
        }
        else
        {
            transform.localScale = initial;
        }

        
    }

    void Quieto()
    {
        direccion = 0f;
        // Stop animation of the enemy
        animador.SetBool("Move", false);
    }

    void takeDamage()
    {

    }
}
