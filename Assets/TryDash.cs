using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 0.3f; // La fuerza del dash
    public float dashDuration = 0.2f; // Duración del dash en segundos
    public KeyCode dashKey = KeyCode.J; // Tecla para activar el dash

    private Rigidbody2D rb;
    private bool isDashing = false;
    private float dashTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(dashKey) && !isDashing)
        {
            isDashing = true;
            dashTimer = 0f;

            // Aplicar fuerza en la dirección actual del personaje
            Vector2 dashDirection = transform.right; // Cambiado a transform.right para un juego 2D
            rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        }

        if (isDashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
            }
        }
    }
}
