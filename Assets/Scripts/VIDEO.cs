using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Creditos : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Asigna tu VideoPlayer en el inspector

    void Start()
    {
        // Iniciar el video al cargar la escena
        videoPlayer.Play();
    }

    // Puedes agregar un método para volver a la escena principal o menú después del video
    public void VolverAlMenu()
    {
        // Asegúrate de que el nombre de tu escena de menú sea correcto
        SceneManager.LoadScene("Main Menu");
    }
}

