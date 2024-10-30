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

    // Puedes agregar un m�todo para volver a la escena principal o men� despu�s del video
    public void VolverAlMenu()
    {
        // Aseg�rate de que el nombre de tu escena de men� sea correcto
        SceneManager.LoadScene("Main Menu");
    }
}

