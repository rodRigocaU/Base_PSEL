using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [SerializeField] private float Velocidade, AlturaPulo;
    //O corpo do jogador
    [SerializeField] private Rigidbody2D Corpo;
    //Para ele não pular infinitamente
    private bool PodePular = true;

    void Update()
    {
        //Se a barra de espaço foi pressionada e o jogador pode pular
        if(Input.GetKeyDown(KeyCode.Space) && PodePular)
        {
            //Adiciona uma força para cima proporcional à AlturaPulo
            Corpo.AddForce(new Vector2(0, AlturaPulo));
            //Proíbe o jogador de pular
            PodePular = false;
        }
        //Define a velocidade do corpo baseada na tecla pressionada (Input.GetAxisRaw("Horizontal"))
        //A função retorna 1 se a seta pra direita ou D foram pressionados
        //Retorna 0 se a seta da esquerda ou A foram pressionados
        //Neste caso, não se usa Time.deltaTime, porque RigidBody2D.velocity já opera baseado na taxa de frames
        Corpo.velocity = new Vector2(Velocidade * Input.GetAxisRaw("Horizontal"), Corpo.velocity.y);
    }

    //Função para ser chamada pela base
    public void PermitirPulo()
    {
        PodePular = true;
    }
}
