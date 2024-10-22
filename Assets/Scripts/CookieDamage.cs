using UnityEngine;

public class CookieDamage : MonoBehaviour
{
    public int damage = 5; // Daño por defecto

    // Método para ajustar el daño
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    // Método para aplicar el daño a los objetos con los que colisiona
    void OnCollisionEnter(Collision collision)
    {
        // Aquí puedes agregar la lógica para aplicar daño al objeto que colisiona
        // Por ejemplo, si colisiona con un enemigo:
        InflatableEnemy enemy = collision.gameObject.GetComponent<InflatableEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Destruir la galleta después de la colisión
        Destroy(gameObject);
    }
}
