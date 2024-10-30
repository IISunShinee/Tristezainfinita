using UnityEngine;
using UnityEngine.SceneManagement;

public class Soga : MonoBehaviour
{
    // Este método se llama cuando el jugador colisiona con la soga
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto que colisiona tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Cargar la escena de los créditos
            SceneManager.LoadScene("Creditos"); // Asegúrate de que la escena se llame "Creditos"
        }
    }
}
