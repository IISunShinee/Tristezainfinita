using System.Collections;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject normalCookiePrefab;   // Prefab de galleta normal
    public GameObject bigCookiePrefab;      // Prefab de galleta grande
    public Transform firePoint;             // Punto desde donde se disparan las galletas

    public float normalCookieDamage = 5f;   // Daño de galleta normal
    public float bigCookieDamage = 10f;     // Daño de galleta grande
    public float bigCookieDelay = 3f;       // Delay para disparar la galleta grande
    public float normalCookieFireRate = 0.5f; // Tiempo entre disparos de galleta normal
    public float cookieForce = 500f;        // Fuerza aplicada al disparar las galletas

    private bool canShootBigCookie = true;  // Para controlar el delay del disparo de la galleta grande
    private bool canShootNormalCookie = true; // Para controlar el disparo normal

    void Start()
    {
        // Establecer la rotación inicial del cañón
        transform.rotation = Quaternion.Euler(0, -90, 0); // Rotación fija
    }

    void Update()
    {
        // Disparo de galleta normal al mantener click izquierdo
        if (Input.GetMouseButton(0) && canShootNormalCookie)
        {
            ShootNormalCookie();
            StartCoroutine(HandleNormalCookieFireRate());
        }

        // Disparo de galleta grande al hacer click derecho
        if (Input.GetMouseButtonDown(1) && canShootBigCookie)
        {
            StartCoroutine(ShootBigCookieWithDelay());
        }
    }

    // Función para disparar galleta normal
    void ShootNormalCookie()
    {
        GameObject cookie = Instantiate(normalCookiePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = cookie.GetComponent<Rigidbody>(); // Asegúrate de que las galletas tengan un Rigidbody

        if (rb != null)
        {
            rb.AddForce(firePoint.forward * cookieForce); // Aplica fuerza en la dirección del fuego
        }

        Debug.Log("Disparada galleta normal con daño: " + normalCookieDamage);
    }

    // Corutina para controlar la tasa de disparo de galleta normal
    IEnumerator HandleNormalCookieFireRate()
    {
        canShootNormalCookie = false;
        yield return new WaitForSeconds(normalCookieFireRate);
        canShootNormalCookie = true;
    }

    // Corutina para disparar la galleta grande con un delay de 3 segundos
    IEnumerator ShootBigCookieWithDelay()
    {
        canShootBigCookie = false;
        Debug.Log("Preparando galleta grande... Esperando 3 segundos");

        yield return new WaitForSeconds(bigCookieDelay);

        GameObject bigCookie = Instantiate(bigCookiePrefab, firePoint.position, firePoint.rotation);
        Rigidbody bigRb = bigCookie.GetComponent<Rigidbody>(); // Asegúrate de que las galletas grandes tengan un Rigidbody

        if (bigRb != null)
        {
            bigRb.AddForce(firePoint.forward * cookieForce); // Aplica fuerza en la dirección del fuego
        }

        Debug.Log("Disparada galleta grande con daño: " + bigCookieDamage);
        canShootBigCookie = true;
    }
}