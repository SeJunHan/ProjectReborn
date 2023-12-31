using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MGDDRManager : MiniGameManager
{
    private int keyStack; // 게임 진행시 나오는 키의 개수
    private int keyCount; // 진행 도중 알맞게 누른 키의 개수
    private float playTime; // 게임 진행 시간
    private bool gameActive = false; // 게임 가능여부
    private int[] RandomKey;
    private int round;
    private int maxRound;
    private int[] keyOfRound;
    private Vector3 managerTrans;
    [SerializeField]
    private Arrow arrow;
    private TextMeshProUGUI timeText;
    Arrow[] arrowArray;
    private Slider timeSlider;

    private void Awake()
    {
        GameType = 3;

        timeText = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        timeSlider = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Slider>();

        managerTrans = new Vector3(0, 0, 0);

        keyCount = 0;
        round = 0;

        keyOfRound = new int[5];
        maxRound = keyOfRound.Length - 1;
        keyOfRound[0] = 3;
        keyOfRound[1] = 4;
        keyOfRound[2] = 5;
        keyOfRound[3] = 5;
        keyOfRound[4] = 5;

    }

    protected override void Start()
    {
        base.Start();
        panelActiveSelf = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (gameActive)
        {
            base.Update();
        }
        if (panelActiveSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FrameWorkManager.SetDisabledPanel();
                Character.instance.SetCharacterInput(true, true, true);
            }
        }
    }
    private void asd(int k)
    {
        if(panelActiveSelf)
        {
            // 주조 선택 입력

        }
        else
        {
            PressKey(1);
        }
    }
    public override void EscapeKeyPress() { }
    public override void LeftArrowKeyPress()
    {
        PressKey(1);
        asd(1);
    }
    public override void RightArrowKeyPress()
    {
        PressKey(3);
    }
    public override void UpArrowKeyPress()
    {
        PressKey(0);
    }
    public override void DownArrowKeyPress()
    {
        PressKey(2);
    }
    public override void XKeyPress() { }
    public override void GameStart()
    {
        FrameWorkManager.GameStart();
    }
    public override void GameEnd(bool clear)
    {
        FrameWorkManager.GameEnd();
        StopCoroutine("CountTime");
        timeSlider.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        gameActive = false;
        Character.instance.SetCharacterInput(true, true, true);
        QuestManager.instance.MinigameClear(true);
    }
    public override void SetRound(int nextRound)
    {
        if(!gameActive)
        {
            timeSlider.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
        }
        SetMainWork(keyOfRound[nextRound]);
        Generate();
        gameActive = true;
    }
    public override void SetGame() // MinigameDdr()
    {
        playTime = 60.0f; // 플레이 타임을 정한다
        timeSlider.maxValue = playTime; // 플레이 타임에 맞게 시간 프로그레스 바의 최대값을 정해준다
        StartCoroutine("CountTime", 0.1);
    }
    public override void SetMainWork(int key) // setKey()
    {
        keyStack = key; // 이번 게임의 키 개수가 정해진다
        managerTrans.x = -(key - 1);
        gameObject.transform.position = managerTrans;
        RandomKey = new int[keyStack];  // 키 개수만큼 배열을 만들어준다
        arrowArray = new Arrow[keyStack]; // 키 개수만큼 애로우가 들어갈 배열을 만들어준다

        for (int i = 0; i < keyStack; i++) // 배열안에 0~3까지 난수 생성 / 0 = L , 1 = R , 2 = U, 3 = D
        {
            RandomKey[i] = Random.Range(0, 4);
        }
    }
    public override void Generate()
    {
        for (int i = 0; i < arrowArray.Length; i++)
        {
            arrowArray[i] = Instantiate(arrow, new Vector3(transform.position.x + 2f * i, transform.position.y, transform.position.z), Quaternion.identity) as Arrow;
            // 부모 오브젝트 설정. 부모의 transform을 받기 위함
            arrowArray[i].transform.Rotate(0, 0, 90 * RandomKey[i]);
        }
    }
    public override void PressKey(int key)
    {
        if (gameActive) // ddr 게임
        {
            if (RandomKey[keyCount] == key)
            {
                if (RandomKey[keyCount] == key) // 키 박스의 숫자와 눌린 키가 일치한지
                {
                    if (keyCount != keyStack) // 키 카운트가 최대값이 아닐 때
                    {
                        arrowArray[keyCount].ArrowAnim();
                        keyCount++; // 키 카운트를 증가시킨다
                        if (keyCount == keyStack) // 키 카운트가 최대에 도달 했을 때
                        {
                            keyCount = 0;
                            if (round++ != maxRound)
                            {
                                SetRound(round);
                            }
                            else
                            {
                                round = 0;
                                GameEnd(true);
                            }
                        }
                    }
                }
            }
            else if (playTime > 5)
            {
                Debug.Log("잘 못 누름");
                playTime -= 5;
            }
            else
            {
                playTime = 0;
                timeSlider.value = playTime;
                for(int i = keyCount; i < keyStack; i++)
                {
                    arrowArray[i].DestroyImage();
                }
                GameEnd(false);
            }
        }
    }
    public override IEnumerator CountTime(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (playTime > 0f && gameActive) // ddr 게임 진행중
        {
            playTime -= 0.1f;
            timeSlider.value = playTime;
            StartCoroutine("CountTime", 0.1f);
        }
    }
}
