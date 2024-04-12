using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseContoller : MonoBehaviour
{
    //Qual é o gameObject do jogador
    [SerializeField] private GameObject Player;

    //Ao colidir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Se colidiu com uma plataforma
        if (collision.collider.tag == "Plataforma")
            //Chama o método PermitirPulo do jogador
            Player.GetComponent<Movimento>().PermitirPulo();

        //O objetivo aqui é permitir que o jogador pule novamente somente quando sua base inferior
        //tocar no chão, impedindo que ele encoste numa parede e pule infinitamente
    }
}
