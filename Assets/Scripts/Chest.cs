using UnityEngine;

public class Chest : MonoBehaviour, IDamagable
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private Animator anim => GetComponentInChildren<Animator>();

    [Header("Open Chest Details")]
    [SerializeField] private Vector2 openChestVelocity;
    public bool TakeDamage(float damage,float elementalDamage, ElementType element,Transform damageDealer)
    {
        anim.SetBool("chestOpen", true);
        rb.linearVelocity = openChestVelocity;

        rb.angularVelocity = Random.Range(-200f, 200f);

        //Drop items

        return true;
    }
}
