using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowProjectile : MonoBehaviour, IPoolObject
{
    private float speed;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private List<Sprite> sprites;
    public int line;
    public int distance;
    private Vector3 vector3 = Vector3.zero;
    public bool move = false;
    public int damage;
    void Start()
    {
        speed = 6f;
        //Invoke("DestroyProjectile", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        } else
        {
            gameObject.transform.position = vector3;
        }

    }
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "CrowMonster":
                collision.GetComponent<CrowMonster>().MonsterDamaged(damage);
                DestroyProjectile();
                break;
        }
    }
    public void Positioning()
    {
        switch (line)
        {
            case 0:
                vector3.x = -7.5f;
                vector3.y = 3.2f;
                break;
            case 1:
                vector3.x = -7.5f;
                vector3.y = -0.3f;
                break;
            case 2:
                vector3.x = -7.5f;
                vector3.y = -4.0f;
                break;
        }
        gameObject.transform.position = vector3;
        spriteRenderer.sprite = sprites[line];

    }

    public void OnCreatedInPool()
    {

    }

    public void OnGettingFromPool()
    {
        
    }
}
