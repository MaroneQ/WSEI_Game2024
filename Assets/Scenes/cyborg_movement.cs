using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyborgMovement : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3f;
    public float rotationSpeed = 3f;
    public float shootDistance = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    public float bulletLifetime = 2f;
    public int damageAmount = 10;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isPlayerInRange;
    private float nextFireTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nextFireTime = Time.time;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetDirection = target.position - transform.position;
            targetDirection.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            float distanceToPlayer = Vector3.Distance(transform.position, target.position);
            if (distanceToPlayer <= shootDistance)
            {
                isPlayerInRange = true;
            }
            else
            {
                isPlayerInRange = false;
            }

            if (!isPlayerInRange)
            {
                moveDirection = targetDirection.normalized;
                rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            }

            if (isPlayerInRange && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = (target.position - bulletSpawnPoint.position).normalized * bulletSpeed;
        Destroy(bullet, bulletLifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
