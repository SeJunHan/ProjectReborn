using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class MGScarecrowManager : MiniGameManager
{
    [SerializeField] private Scarecrow Scarecrow;
    [SerializeField] private Sprite Scarecrow2;
    [SerializeField] private Sprite Scarecrow3;
    [SerializeField] private GameObject CrowSpawner;
    [SerializeField] private GameObject Fence;
    private List<Dictionary<string, object>> ScarecrowMonsterTable;


    private GameObject CrowSpawner1;
    private GameObject CrowSpawner2;
    private GameObject CrowSpawner3;
    private SpriteRenderer ScarecrowSpriteRenderer;
    private Scarecrow[] scarecrowList;
    private GameObject[] FenceList;
    private List<CrowMonster> CrowList;
    private CrowMonster temCrowMonster;
    private int round;
    private int accCrowCount;
    private int preCrowCount;
    private int line;
    private int monsterType;
    private float delayTime;
    private InitMonsterData monsterData;
    private int gold;

    private void Awake()
    {
        ScarecrowMonsterTable = CSVReader.Read("CSV/ScarecrowMonsterTable");
        scarecrowList = new Scarecrow[3];
        scarecrowList[0] = Instantiate(Scarecrow, new Vector3(-7.8f, 3.2f, 0), Quaternion.identity);
        scarecrowList[0].line = 0;
        scarecrowList[1] = Instantiate(Scarecrow, new Vector3(-7.8f, -0.3f, 0), Quaternion.identity);
        scarecrowList[1].line = 1;
        scarecrowList[2] = Instantiate(Scarecrow, new Vector3(-7.8f, -4.0f, 0), Quaternion.identity);
        scarecrowList[2].line = 2;
        FenceList = new GameObject[3];
        FenceList[0] = Instantiate(Fence, new Vector3(-5.8f, 3.2f, 0), Quaternion.identity);
        FenceList[1] = Instantiate(Fence, new Vector3(-5.8f, -0.3f, 0), Quaternion.identity);
        FenceList[2] = Instantiate(Fence, new Vector3(-5.8f, -4.0f, 0), Quaternion.identity);
        CrowSpawner1 = Instantiate(CrowSpawner, new Vector3(8.5f, 3.2f, 0), Quaternion.identity);
        CrowSpawner1.name = "CrowSpawner1";
        CrowSpawner2 = Instantiate(CrowSpawner, new Vector3(8.5f, -0.3f, 0), Quaternion.identity);
        CrowSpawner2.name = "CrowSpawner2";
        CrowSpawner3 = Instantiate(CrowSpawner, new Vector3(8.5f, -4.0f, 0), Quaternion.identity);
        CrowSpawner3.name = "CrowSpawner3";
    }
    protected override void Start()
    {
        base.Start();
        CrowList = new List<CrowMonster>();
        ScarecrowSpriteRenderer = Scarecrow.GetComponent<SpriteRenderer>();
        Debug.Log(Scarecrow);
        

        GameStart();
        

    }


    protected override void Update()
    {
        base.Update();
    }
    public override void GameStart()
    {
        // 허수아비 소환
        
        scarecrowList[1].GetComponent<SpriteRenderer>().sprite = Scarecrow2;
        scarecrowList[2].GetComponent<SpriteRenderer>().sprite = Scarecrow3;

        // 라운드 시작
        round = 1;
        monsterType = (int)PoolData.CrowMonster;

        // 까마귀 랜덤 소환
        SetRound(round);

    }
    public override void GameEnd(bool clear)
    {
        // 게임 엔드 화면 구성

    }
    public override void SetRound(int roundNum)
    {
        switch(roundNum)
        {
            case 0:
                break;
            case 1:
                // crow의 종류 선택
                break;
            case 2:
                // crow의 종류 선택
                break;
            case 3:
                // crow의 종류 선택
                // monsterType = ??
                break;
        }
        StartCoroutine(GenerateCoroutine());
    }

    public void ReturnPool(CrowMonster crow, int dropGold)
    {
        AdventureGameManager.instance.pool.TakeToPool<CrowMonster>(crow.idName, crow);
        //CrowList.RemoveAt(preCrowCount-1);
        preCrowCount--;
        gold += dropGold;
        // 까마귀 모두 파괴시 게임 엔드로
        if (preCrowCount == 0)
        {
            switch (round)
            {
                case 0:
                    break;
                case 1:
                    round = 2;
                    SetRound(round);
                    break;
                case 2:
                    round = 3;
                    SetRound(round);
                    break;
                case 3:
                    GameEnd(true);
                    break;
            }
        }
    }
    IEnumerator GenerateCoroutine()
    {
        for(int i=0;i<7;i++)
        {
            StartCoroutine(GenerateCrowCoroutine(monsterType, 0));
            StartCoroutine(GenerateCrowCoroutine(monsterType, 1));
            StartCoroutine(GenerateCrowCoroutine(monsterType, 2));
            yield return YieldCache.WaitForSeconds(3f);
        }
        accCrowCount = 0;
        yield return null;
    }
    IEnumerator GenerateCrowCoroutine(int type, int line)
    {
        switch(line)
        {
            case 0:
                delayTime = Random.Range(0, 2) * 0.6f + 1f;
                break;
            case 1:
                delayTime = Random.Range(0, 2) * 0.6f + 1.1f;
                break;
            case 2:
                delayTime = Random.Range(0, 2) * 0.6f + 1.2f;
                break;
        }
        yield return YieldCache.WaitForSeconds(delayTime);
        monsterData = new InitMonsterData();
        temCrowMonster = AdventureGameManager.instance.pool.GetFromPool<CrowMonster>(type);
        switch(round)
        {
            case 1:
                monsterData.speed = 2f;
                break;
            case 2:
                monsterData.speed = 4f;
                break;
            case 3:
                monsterData.speed = 5f;
                break;
        }
        monsterData.dropGoldMax = int.Parse(ScarecrowMonsterTable[round - 1]["Drop_Gold_Max"].ToString());
        monsterData.dropGoldMin = int.Parse(ScarecrowMonsterTable[round - 1]["Drop_Gold_Min"].ToString());
        monsterData.hp = int.Parse(ScarecrowMonsterTable[round - 1]["HP"].ToString());
        monsterData.line = line;
        monsterData.idName = "CrowMonster";
        monsterData.name = "CrowMonster";
        temCrowMonster.InitMonster(monsterData);
        temCrowMonster.ChangeAnimation(round);
        temCrowMonster.Positioning();
        temCrowMonster.move = true;

        accCrowCount++;
        preCrowCount++;
        yield return null;
    }


}
public class InitMonsterData
{
    public int line;
    public string idName;
    public string name;
    public float speed;
    public int dropGoldMin;
    public int dropGoldMax;
    public int hp;
}
