using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private GameManager gameManager = null;
    private void Start()
    {
   
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (transform.position.x > gameManager.MaxPosition.x + 2f)
        {
            Destroy(gameObject);
        }
    }
}