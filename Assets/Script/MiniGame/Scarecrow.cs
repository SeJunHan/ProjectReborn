using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    private float curtime;
    private float cooltime;
    private ScarecrowProjectile projectile;
    public int line;
    public int distance;
    private RaycastHit2D hitinfo;
    private int projectileDamageMin;
    private int projectileDamageMax;

    void Start()
    {
        cooltime = 1f;
        projectileDamageMin = 50;
        projectileDamageMax = 50;
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(this.transform.position, Vector2.right * 10, Color.red);

        hitinfo = Physics2D.Raycast(this.transform.position,this.transform.right,10f, layerMask);
        if(hitinfo.collider != null)
        {
            Debug.Log(hitinfo.collider.tag);
            if (hitinfo.collider.tag == "CrowMonster")
            {
                Debug.Log("╬Нец");
                Attack();
            }
        }
        
    }
    public void UpgradeAttackSpeed(float speed)
    {
        cooltime = speed;
    }
    private void Attack()
    {
        if (curtime <= 0)
        {
            GenerateProjectile();
            curtime = cooltime;
        }
        curtime -= Time.fixedDeltaTime;
    }
    private void GenerateProjectile()
    {
        projectile = AdventureGameManager.instance.pool.GetFromPool<ScarecrowProjectile>((int)PoolData.ScarecrowProjectile);
        //projectile.transform.SetParent(transform.GetChild(0).transform, false);
        projectile.line = line;
        projectile.distance = distance;
        projectile.Positioning();
        projectile.move = true;
        projectile.damage = Random.Range(projectileDamageMin, projectileDamageMax);
    }
}
