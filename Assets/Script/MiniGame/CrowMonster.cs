using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Redcode.Pools;
using UnityEngine.UIElements;

public class CrowMonster : MonoBehaviour, IPoolObject
{
    public string idName;
    private float speed = 2f;
    public bool move = false;
    private Vector3 vector3 = Vector3.zero;
    private int line;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private RuntimeAnimatorController sparrowController;
    [SerializeField]
    private RuntimeAnimatorController eagleController;
    [SerializeField]
    private RuntimeAnimatorController crowController;
    [SerializeField]
    private RuntimeAnimatorController owlController;

    private int dropGoldMax;
    private int dropGoldMin;
    private int hp;
    private bool onHit = false;
    // Start is called before the first frame update
    void Start()
    {
        //speed = 2f;
        vector3 = new Vector3(7.0f, 2.0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (move && !onHit)
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        } else if(move)
        {
            transform.Translate(transform.right * -1 * speed * 0.1f * Time.deltaTime);
            StartCoroutine(HitTimer());
            //gameObject.transform.position = vector3;
        } else
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "PlayerAttack":
                //MonsterDamaged();
                break;
        }
    }
    public void MonsterDamaged(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            // »ç¸Á¸ð¼Ç
            move = false;
            animator.SetBool("Death", true);
            animator.SetTrigger("doDamaged");
            StartCoroutine(DeathCoroutine());
            
        } else
        {
            onHit = true;
            // ÇÇ°Ý¸ð¼Ç
            animator.SetTrigger("doDamaged");
        }
        
    }
    public void InitMonster(InitMonsterData data)
    {
        idName = data.idName;
        line = data.line;
        dropGoldMax = data.dropGoldMax;
        dropGoldMin = data.dropGoldMin;
        speed = data.speed;
        name = data.name;
        hp = data.hp;
    }
    public void Positioning()
    {
        Debug.Log("Æ÷Áö¼Å´×");

        switch (line)
        {
            case 0:
                vector3.x = 8.5f;
                vector3.y = 3.2f;
                break;
            case 1:
                vector3.x = 8.5f;
                vector3.y = -0.3f;
                break;
            case 2:
                vector3.x = 8.5f;
                vector3.y = -4.0f;
                break;
        }
        gameObject.transform.position = vector3;
        //move = true;
    }
    private int GoldDrop()
    {
        return Random.Range(dropGoldMin, dropGoldMax);
    }
    public void ChangeAnimation(int round)
    {
        switch(round)
        {
            case 1:
                break;
            case 2:
                animator.runtimeAnimatorController = crowController;
                break;
            case 3:
                animator.runtimeAnimatorController = eagleController;
                break;
            case 4:
                animator.runtimeAnimatorController = owlController;
                break;
            case 5:
                switch(Random.Range(0,4))
                {
                    case 0:
                        animator.runtimeAnimatorController = sparrowController;
                        break;
                    case 1:
                        animator.runtimeAnimatorController = crowController;
                        break;
                    case 2:
                        animator.runtimeAnimatorController = eagleController;
                        break;
                    case 3:
                        animator.runtimeAnimatorController = owlController;
                        break;
                }
                break;
        }
    }
    IEnumerator DeathCoroutine()
    {
        yield return YieldCache.WaitForSeconds(1f);
        AdventureGameManager.instance.scarecrowManager.ReturnPool(this, GoldDrop());
    }
    IEnumerator HitTimer()
    {
        yield return YieldCache.WaitForSeconds(0.5f);
        onHit = false;
    }
    public void OnCreatedInPool()
    {
        gameObject.transform.position = vector3;
    }
    public void OnGettingFromPool()
    {
        Debug.Log("Ç®¸µ");
        Invoke("Positioning", 0.01f);
    }
}
