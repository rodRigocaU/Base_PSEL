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
    
    
    //O corpo do jogador
    [SerializeField] private Rigidbody2D Corpo;
    //Para ele n�o pular infinitamente
    private bool PodePular = false;
    [SerializeField] private float ForcaPulo;

    void Update()
    {
        //Se a barra de espa�o foi pressionada e o jogador pode pular
        if(Input.GetKeyDown(KeyCode.Space) && PodePular)
        {
            //Adiciona uma for�a para cima proporcional � AlturaPulo
            Corpo.AddForce(new Vector2(0, AlturaPulo));
            //Pro�be o jogador de pular
            PodePular = false;
        }
        //Define a velocidade do corpo baseada na tecla pressionada (Input.GetAxisRaw("Horizontal"))
        //A fun��o retorna 1 se a seta pra direita ou D foram pressionados
        //Retorna 0 se a seta da esquerda ou A foram pressionados
        //Neste caso, n�o se usa Time.deltaTime, porque RigidBody2D.velocity j� opera baseado na taxa de frames

        float movimento_horizontal = Velocidade * Input.GetAxisRaw("Horizontal");
        Corpo.velocity = new Vector2(movimento_horizontal, Corpo.velocity.y);

        //Cria uma caixa, se a caixa colidir com o chao, pode pular
        //Nessa função se passa a posição, tamanho, angulo e distancia(tamanho) em relação a direção
        //Tambem passa um layer mask, pra que somente os layers associados a Chao sejam considerados
        bool PertoDoChao = Physics2D.BoxCast(PeDoPersonagem.position, new Vector2(0.5f, 0.2f), 0f, Vector2.down, 0.1f, Chao);
        
        
        //Se o acerto tem um resultado não nulo, pode pular
        if(PertoDoChao)
        {
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
    }
}
