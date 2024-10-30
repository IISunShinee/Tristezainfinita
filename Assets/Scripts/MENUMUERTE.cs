using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMuerte : MonoBehaviour
{
    // Asignar en el inspector
    public Button botonNivel1; // Botón para Nivel 1
    public Button botonNivel2; // Botón para Nivel 2
    public Button botonSalir;   // Botón para Salir

    void Start()
    {
        // Asigna las funciones a los botones
        botonNivel1.onClick.AddListener(IrANivel1);
        botonNivel2.onClick.AddListener(IrANivel2);
        botonSalir.onClick.AddListener(SalirDelJuego);
    }

    void IrANivel1()
    {
        SceneManager.LoadScene("Pasteleria"); // Cambia "Nivel1" por el nombre exacto de tu primera escena
    }

    void IrANivel2()
    {
        SceneManager.LoadScene("Cuevas"); // Cambia "Nivel2" por el nombre exacto de tu segunda escena
    }

    void SalirDelJuego()
    {
        Application.Quit(); // Cierra el juego
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Solo funciona en el editor
#endif
    }
}

