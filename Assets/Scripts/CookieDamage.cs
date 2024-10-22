using UnityEngine;

public class CookieDamage : MonoBehaviour
{
    public int damage = 5; // Da�o por defecto

    // M�todo para ajustar el da�o
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    // M�todo para aplicar el da�o a los objetos con los que colisiona
    void OnCollisionEnter(Collision collision)
    {
        // Aqu� puedes agregar la l�gica para aplicar da�o al objeto que colisiona
        // Por ejemplo, si colisiona con un enemigo:
        InflatableEnemy enemy = collision.gameObject.GetComponent<InflatableEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Destruir la galleta despu�s de la colisi�n
        Destroy(gameObject);
    }
}
