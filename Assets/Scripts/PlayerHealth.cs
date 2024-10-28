using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    public float vidaMaxima = 100; // La vida máxima del jugador
    public float vidaActual;       // La vida actual del jugador
    public Slider barraDeVida;   // El slider que muestra la vida del jugador

    void Start()
    {
        // Inicializamos la vida actual al máximo
        vidaActual = vidaMaxima;

        // Asegurarse de que la barra de vida está actualizada al inicio
        ActualizarBarraDeVida();
    }

    // Método para que el jugador reciba daño
    public void TomarDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Jugador recibió daño. Vida restante: " + vidaActual);

        // Asegurarse de que la vida no baja de cero
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        // Actualizar la barra de vida
        ActualizarBarraDeVida();

        if (vidaActual <= 0)
        {
            // Manejar la muerte del jugador
            Debug.Log("El jugador ha muerto.");
        }
    }

    // Método para actualizar la barra de vida
    private void ActualizarBarraDeVida()
    {
        barraDeVida.value = (float)vidaActual / vidaMaxima;
    }
}