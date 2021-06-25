using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnake : EnemyMove
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float fireRate = 1f;

    private float timer = 0f;
    private Vector3 diff = Vector3.zero;
    private VikingMove player = null;
    private float rotationZ = 0f;
    private GameObject bullet = null;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<VikingMove>();
    }

    protected override void Update()
    {
        if (isDead) return;

        transform.Translate(Vector2.left * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            diff = player.transform.position - transform.position;
            diff.Normalize();

            rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            timer = 0f;
            bullet = Instantiate(bulletPrefab, transform);
            bullet.transform.SetParent(null);
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90f);
        }

        if (transform.position.x < gameManager.MinPosition.x - 2f)
        {
            Destroy(gameObject);
        }
    }
}
