using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingMove : MonoBehaviour
{
    [SerializeField]
    private VirtualJoyStick virtualJoyStick;
    [SerializeField]
    private float moveSpeed = 2;
    [SerializeField]
    private float fireRate = 0.2f;
    [SerializeField]
    private Transform bulletPosition = null;
    [SerializeField]
    private GameObject bulletPrefab = null;

    private Rigidbody2D rigid = null;
    private Vector2 targetPosition = Vector2.zero;
    private GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;

    public Animator animator;

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public bool isDamaged = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    private void Update()
    {
        float x = virtualJoyStick.Horizontal();
        float y = virtualJoyStick.Vertical();


        if (x!=0|| y!=0)
        {

            transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, gameManager.MinPosition.x, gameManager.MaxPosition.x), Mathf.Clamp(transform.position.y, gameManager.MinPosition.y, gameManager.MaxPosition.y));
        }
    }
    
    private IEnumerator Fire()
    {
        while (true)
        {
            InstantiateOrPool();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private GameObject InstantiateOrPool()
    {
        GameObject result = null;
  
        if (gameManager.poolManager.transform.childCount > 0)
        {
            result = gameManager.poolManager.transform.GetChild(0).gameObject;
            result.transform.position = bulletPosition.position;
            result.transform.SetParent(null);
            result.SetActive(true);
        }
        
        else
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletPosition);
            newBullet.transform.position = bulletPosition.position;
            newBullet.transform.SetParent(null);
            result = newBullet;
        }

        return result;
    }


    

    private IEnumerator Damage()
    {
        gameManager.Dead();
        isDamaged = true;
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        isDamaged = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged) return;
        StartCoroutine(Damage());
    }
}
