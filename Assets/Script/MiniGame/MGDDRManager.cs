using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MGDDRManager : MiniGameManager
{
    private int keyStack; // ���� ����� ������ Ű�� ����
    private int keyCount; // ���� ���� �˸°� ���� Ű�� ����
    private float playTime; // ���� ���� �ð�
    private bool gameActive = false; // ���� ���ɿ���
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
            // ���� ���� �Է�

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
        playTime = 60.0f; // �÷��� Ÿ���� ���Ѵ�
        timeSlider.maxValue = playTime; // �÷��� Ÿ�ӿ� �°� �ð� ���α׷��� ���� �ִ밪�� �����ش�
        StartCoroutine("CountTime", 0.1);
    }
    public override void SetMainWork(int key) // setKey()
    {
        keyStack = key; // �̹� ������ Ű ������ ��������
        managerTrans.x = -(key - 1);
        gameObject.transform.position = managerTrans;
        RandomKey = new int[keyStack];  // Ű ������ŭ �迭�� ������ش�
        arrowArray = new Arrow[keyStack]; // Ű ������ŭ �ַο찡 �� �迭�� ������ش�

        for (int i = 0; i < keyStack; i++) // �迭�ȿ� 0~3���� ���� ���� / 0 = L , 1 = R , 2 = U, 3 = D
        {
            RandomKey[i] = Random.Range(0, 4);
        }
    }
    public override void Generate()
    {
        for (int i = 0; i < arrowArray.Length; i++)
        {
            arrowArray[i] = Instantiate(arrow, new Vector3(transform.position.x + 2f * i, transform.position.y, transform.position.z), Quaternion.identity) as Arrow;
            // �θ� ������Ʈ ����. �θ��� transform�� �ޱ� ����
            arrowArray[i].transform.Rotate(0, 0, 90 * RandomKey[i]);
        }
    }
    public override void PressKey(int key)
    {
        if (gameActive) // ddr ����
        {
            if (RandomKey[keyCount] == key)
            {
                if (RandomKey[keyCount] == key) // Ű �ڽ��� ���ڿ� ���� Ű�� ��ġ����
                {
                    if (keyCount != keyStack) // Ű ī��Ʈ�� �ִ밪�� �ƴ� ��
                    {
                        arrowArray[keyCount].ArrowAnim();
                        keyCount++; // Ű ī��Ʈ�� ������Ų��
                        if (keyCount == keyStack) // Ű ī��Ʈ�� �ִ뿡 ���� ���� ��
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
                Debug.Log("�� �� ����");
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
        if (playTime > 0f && gameActive) // ddr ���� ������
        {
            playTime -= 0.1f;
            timeSlider.value = playTime;
            StartCoroutine("CountTime", 0.1f);
        }
    }
}