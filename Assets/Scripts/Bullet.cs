using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float fireRate = 5.0f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;
    
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;
        
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * fireRate;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<EnemyHeath>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}
