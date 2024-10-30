using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public int vidaMaxima = 100; // Vida m�xima del jugador
    private int vidaActual;       // Vida actual del jugador

    void Start()
    {
        // Inicializa la vida actual al m�ximo
        vidaActual = vidaMaxima;
    }

    void Update()
    {
        // Simula la muerte del jugador al presionar "K"
        if (Input.GetKeyDown(KeyCode.K))
        {
            MatarJugador();
        }
    }

    // M�todo para recibir da�o
    public void TomarDa�o(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Jugador recibi� da�o. Vida restante: " + vidaActual);

        // Verifica si la vida llega a cero
        if (vidaActual <= 0)
        {
            MatarJugador();
        }
    }

    // M�todo para manejar la muerte del jugador
    public void MatarJugador()
    {
        Debug.Log("El jugador ha muerto.");
        // Cambia a la escena "Muerte"
        SceneManager.LoadScene("Muerte"); // Aseg�rate de que "Muerte" sea el nombre exacto de tu escena

        // Habilitar el cursor y mostrarlo
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Permite mover el cursor libremente
    }
}




