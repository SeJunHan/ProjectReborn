using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MGQuizManager : MiniGameManager
{
    private bool quizGameActive = false;
    private int answerChoiceNumber;
    private int answerNumber;
    private int quizRound;
    private int gameNumberRound;
    private int temNum;
    private Color temColor;
    private Good good;
    List<Dictionary<string, object>> quizList;
    private Image quizPanel;
    private Image contextPanel;
    private TextMeshProUGUI quizText;
    private TextMeshProUGUI contextText;
    private Image[] answerPanel;
    private TextMeshProUGUI[] answerText;
    private TextMeshProUGUI timeText;
    [SerializeField]
    private GameObject BookObject;
    private GameObject temImage;
    private float playTime; // 게임 진행 시간

    private void Awake()
    {
        good = GameObject.Find("Canvas").transform.GetChild(4).GetComponent<Good>();

        timeText = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        quizPanel = GameObject.Find("Canvas").transform.GetChild(5).GetComponent<Image>();
        quizText = quizPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        contextPanel = quizPanel.transform.GetChild(1).GetComponent<Image>();
        contextText = contextPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        answerPanel = new Image[4];
        answerText = new TextMeshProUGUI[answerPanel.Length];
        for (int i = 0; i < answerPanel.Length; i++)
        {
            answerPanel[i] = quizPanel.transform.GetChild(i + 2).GetComponent<Image>();
            answerText[i] = answerPanel[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        temColor = Color.white;
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (quizGameActive)
        {
            base.Update();
        }
    }
    public override void EscapeKeyPress() { }
    public override void LeftArrowKeyPress()
    {
        PressKey(1);
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
    public override void XKeyPress()
    {
        SelectAnswer();
    }
    public override void SpaceKeyPress()
    {
        SelectAnswer();
    }
    public override void GameStart()
    {
        quizRound = 0;
        answerChoiceNumber = 0;
        Generate();
        SetGame();
        timeText.gameObject.SetActive(true);
        quizPanel.gameObject.SetActive(true);
        StartCoroutine(CountTime(0.1f));
    }
    public override void GameEnd(bool clear)
    {
        SelectedChange(false);
        answerChoiceNumber = 0;
        SelectedChange(true);
        timeText.gameObject.SetActive(false);
        quizPanel.gameObject.SetActive(false);
        if (Character.instance.MyMapNumber == "0005" || Character.instance.MyMapNumber == "0105")
        {
            Destroy(temImage);
        }

        quizGameActive = false;


        QuestManager.instance.MinigameClear(true);
        Character.instance.SetCharacterInput(true, true, true);
    }

    public override void PressKey(int key)
    {
        switch (key)
        {
            case 0:
                if (answerChoiceNumber == 2 || answerChoiceNumber == 3)
                {
                    SetMainWork(0);
                }
                break;
            case 1:
                if (answerChoiceNumber == 1 || answerChoiceNumber == 3)
                {
                    SetMainWork(1);
                }
                break;
            case 2:
                if (answerChoiceNumber == 0 || answerChoiceNumber == 1)
                {
                    SetMainWork(2);
                }
                break;
            case 3:
                if (answerChoiceNumber == 0 || answerChoiceNumber == 2)
                {
                    SetMainWork(3);
                }
                break;
            default:
                break;
        }
    }
    private void SelectAnswer()
    {
        quizGameActive = false;
        good.gameObject.SetActive(true);
        if (answerChoiceNumber == answerNumber)
        {
            good.ChangeSource(true);
        }
        else
        {
            good.ChangeSource(false);
        }
        StartCoroutine("WaitingTime", 0.5f);
        quizRound++;
        if (quizRound < 5)
        {
            SetGame();
        }
        else if (quizRound == 5)
        {
            GameEnd(true);
        }
    }
    public override void SetGame() // QuizSetting()
    {
        quizGameActive = true;
        playTime = 60.0f; // 플레이 타임을 정한다
        temNum = Random.Range(1, 3);
        switch (Character.instance.MyMapNumber)
        {
            case "0005":
                gameNumberRound = quizRound * temNum;
                break;
            case "0105":
                gameNumberRound = (quizRound * temNum) + 10;
                break;
        }
        contextText.text = quizList[gameNumberRound]["Context"].ToString();
        answerText[0].text = quizList[gameNumberRound]["Answer1"].ToString();
        answerText[1].text = quizList[gameNumberRound]["Answer2"].ToString();
        answerText[2].text = quizList[gameNumberRound]["Answer3"].ToString();
        answerText[3].text = quizList[gameNumberRound]["Answer4"].ToString();
        answerNumber = int.Parse(quizList[gameNumberRound]["AnswerNumber"].ToString()) - 1;
    }
    public override void Generate() // QuizGenerate()
    {
        quizList = CSVReader.Read("QuizText");
        temImage = Instantiate(BookObject) as GameObject;
    }
    public override void SetMainWork(int position) // SelectedChange()
    {
        SelectedChange(false);
        switch (position)
        {
            case 0:
                answerChoiceNumber -= 2; // 선택된 넘버를 변경
                break;
            case 1:
                answerChoiceNumber -= 1;
                break;
            case 2:
                answerChoiceNumber += 2;
                break;
            case 3:
                answerChoiceNumber += 1;
                break;
        }

        SelectedChange(true);


    }
    private void SelectedChange(bool select)
    {
        if(select)
        {
            temColor = answerPanel[answerChoiceNumber].color; // 현재 패널의 컬러를 임시 컬러에 넣는다
            temColor.a = 1f; // 임시 컬러의 알파값을 1f, 즉 255로 만든다 (불투명 - 선택됨)
            answerPanel[answerChoiceNumber].color = temColor; // 패널의 컬러값에 임시 컬러값을 적용시킨다.
        } else
        {
            temColor = answerPanel[answerChoiceNumber].color; // 현재 패널의 컬러를 임시 컬러에 넣는다
            temColor.a = 0.4f; // 임시 컬러의 알파값을 0.4f, 즉 100 정도로 만든다
            answerPanel[answerChoiceNumber].color = temColor; // 패널의 컬러값에 임시 컬러값을 적용시킨다.
        }
    }
    public override IEnumerator CountTime(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (playTime > 0f && quizGameActive) // 퀴즈 게임 진행중
        {
            playTime -= 0.1f;
            timeText.text = playTime.ToString();
            StartCoroutine(CountTime(0.1f));
        }
        else if (playTime < 0f && quizGameActive)
        {
            playTime = 0f;
            GameEnd(false);
        }
    }
    IEnumerator WaitingTime(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }

}
