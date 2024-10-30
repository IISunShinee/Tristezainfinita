using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CreditVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Asigna el VideoPlayer desde el Inspector
    public string escenaPrincipal = "MenuPrincipal"; // Escena a la que volver tras los cr�ditos
    public GameObject objetoCredito; // El objeto que activar� los cr�ditos cuando se toque

    private bool nivelCompletado = false;

    void Start()
    {
        videoPlayer.loopPointReached += VolverAlMenu; // Se llama cuando el video termina
        videoPlayer.gameObject.SetActive(false);  // Ocultar el VideoPlayer hasta que sea necesario
    }

    // M�todo que se llama cuando el jugador toca el objeto
    void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que colisiona es el jugador
        if (other.CompareTag("Player") && !nivelCompletado)
        {
            nivelCompletado = true;
            ReproducirCreditos();
        }
    }

    public void ReproducirCreditos()
    {
        videoPlayer.gameObject.SetActive(true);  // Mostrar el VideoPlayer
        videoPlayer.Play();  // Iniciar la reproducci�n del video
    }

    // M�todo que se llama cuando el video termina
    void VolverAlMenu(VideoPlayer vp)
    {
        SceneManager.LoadScene(escenaPrincipal);  // Volver al men� principal
    }
}

