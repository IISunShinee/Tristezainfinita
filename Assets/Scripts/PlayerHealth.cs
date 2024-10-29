using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    public float vidaMaxima = 100f;  // La vida m�xima del jugador
    public float vidaActual;         // La vida actual del jugador
    public Slider barraDeVida;       // El slider que muestra la vida del jugador
    public PlayerDeath playerDeathScript; // Referencia al script PlayerDeath

    void Start()
    {
        // Inicializamos la vida actual al m�ximo
        vidaActual = vidaMaxima;

        // Asegurarse de que la barra de vida est� actualizada al inicio
        ActualizarBarraDeVida();

        // Busca el script PlayerDeath en la escena
        if (playerDeathScript == null)
        {
            playerDeathScript = GameObject.Find("MUERTE").GetComponent<PlayerDeath>();
        }
    }

    // M�todo para que el jugador reciba da�o
    public void TomarDa�o(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Jugador recibi� da�o. Vida restante: " + vidaActual);

        // Asegurarse de que la vida no baja de cero
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        // Actualizar la barra de vida
        ActualizarBarraDeVida();

        // Si la vida es 0 o menor, el jugador muere
        if (vidaActual <= 0)
        {
            Debug.Log("El jugador ha muerto.");

            // Llamar al m�todo para manejar la muerte del jugador
            if (playerDeathScript != null)
            {
                playerDeathScript.MatarJugador();
            }
        }
    }

    // M�todo para curar al jugador
    public void CurarJugador(float cantidad)
    {
        vidaActual += cantidad;
        Debug.Log("Jugador se cur�. Vida actual: " + vidaActual);

        // Asegurarse de que la vida no exceda la vida m�xima
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        // Actualizar la barra de vida
        ActualizarBarraDeVida();
    }

    // M�todo para actualizar la barra de vida
    private void ActualizarBarraDeVida()
    {
        barraDeVida.value = vidaActual / vidaMaxima;
    }
}
