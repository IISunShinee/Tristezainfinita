using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    public float vidaMaxima = 100f;  // La vida máxima del jugador
    public float vidaActual;         // La vida actual del jugador
    public Slider barraDeVida;       // El slider que muestra la vida del jugador
    public PlayerDeath playerDeathScript; // Referencia al script PlayerDeath

    void Start()
    {
        // Inicializamos la vida actual al máximo
        vidaActual = vidaMaxima;

        // Asegurarse de que la barra de vida está actualizada al inicio
        ActualizarBarraDeVida();

        // Busca el script PlayerDeath en la escena
        if (playerDeathScript == null)
        {
            playerDeathScript = GameObject.Find("MUERTE").GetComponent<PlayerDeath>();
        }
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

        // Si la vida es 0 o menor, el jugador muere
        if (vidaActual <= 0)
        {
            Debug.Log("El jugador ha muerto.");

            // Llamar al método para manejar la muerte del jugador
            if (playerDeathScript != null)
            {
                playerDeathScript.MatarJugador();
            }
        }
    }

    // Método para curar al jugador
    public void CurarJugador(float cantidad)
    {
        vidaActual += cantidad;
        Debug.Log("Jugador se curó. Vida actual: " + vidaActual);

        // Asegurarse de que la vida no exceda la vida máxima
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        // Actualizar la barra de vida
        ActualizarBarraDeVida();
    }

    // Método para actualizar la barra de vida
    private void ActualizarBarraDeVida()
    {
        barraDeVida.value = vidaActual / vidaMaxima;
    }
}
