using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public AudioClip sonidoBoton; // Asigna tu sonido en el inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // M�todo para iniciar el juego
    public void IniciarJuego()
    {
        SceneManager.LoadScene("Pasteleria"); // Cambia esto al nombre de tu primera escena
    }

    // M�todo para salir del juego
    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }

    // M�todo para reproducir sonido
    public void ReproducirSonido()
    {
        if (sonidoBoton != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoBoton);
        }
    }
}

