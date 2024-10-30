using UnityEngine;
using UnityEngine.SceneManagement;

public class Soga : MonoBehaviour
{
    // Este m�todo se llama cuando el jugador colisiona con la soga
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto que colisiona tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Cargar la escena de los cr�ditos
            SceneManager.LoadScene("Creditos"); // Aseg�rate de que la escena se llame "Creditos"
        }
    }
}
