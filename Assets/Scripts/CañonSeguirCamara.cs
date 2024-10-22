using UnityEngine;

public class CannonFollowCamera : MonoBehaviour
{
    public Transform cameraTransform;   // La cámara del jugador
    public Transform firePoint;         // Punto de disparo del cañón

    void Update()
    {

        // Alinear la rotación del cañón completamente con la cámara (en todos los ejes)
        Vector3 lookDirection = cameraTransform.forward;

        // Rotar el cañón para que apunte en la dirección que sigue la cámara
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}