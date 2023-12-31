using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MGTimingManager : MiniGameManager
{

    private float timingValue;
    private bool timingChangeDirection;
    private bool timingGameActive = false;
    private float randomNumber;
    private int temNumber;
    private int timingRound;
    
    private void Awake()
    {
        GameType = 4;
    }

    protected override void Start()
    {
        base.Start();
        panelActiveSelf = false;
    }

    // Update is called once per frame
    protected override void Update()
    {

        if (timingGameActive)
        {
            // 타이밍맞추기
            base.Update();
        }
        if (panelActiveSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                FrameWorkManager.SetDisabledPanel();
                Character.instance.SetCharacterInput(true, true, true);
            }
        }

    }
    public override void EscapeKeyPress() { }
    public override void LeftArrowKeyPress() { }
    public override void RightArrowKeyPress() { }
    public override void UpArrowKeyPress() { }
    public override void DownArrowKeyPress() { }
    public override void XKeyPress() { }
    public override void SpaceKeyPress()
    {
        timingGameActive = false;
        StopCoroutine("ChangeTimingValue");
        FrameWorkManager.good.gameObject.SetActive(true);
        if (FrameWorkManager.timingSlider.value > (randomNumber - 1.5f) && FrameWorkManager.timingSlider.value < (randomNumber + 1.5f))
        {
            FrameWorkManager.good.ChangeSource(true);
            Debug.Log("명중");
        }
        else
        {
            FrameWorkManager.good.ChangeSource(false);
            Debug.Log("실패");
        }
        StartCoroutine("CountTime", 0.5f);
        timingRound++;
        if (timingRound < 5)
        {
            SetMainWork();
        }
        else if (timingRound == 5)
        {
            GameEnd(true);
            Debug.Log("gameEnd");
        }
    }
    public override void GameStart()
    {
        FrameWorkManager.GameStart();
    }


    

    
    public override void GameEnd(bool clear)
    {
        FrameWorkManager.GameEnd();
        
        FrameWorkManager.timingSlider.gameObject.SetActive(false);
        timingGameActive = false;
        Character.instance.SetCharacterInput(true, true, true);
        QuestManager.instance.MinigameClear(true);

    }
    public override void SetRound(int num) // SetTimingRound()
    {
        timingValue = 0f;
        timingChangeDirection = true;
        timingGameActive = true;
        timingRound = 0;
        SetMainWork();
    }
    public override void SetMainWork() // SetTimingPosition()
    {
        if (timingRound < 5)
        {
            randomNumber = Random.Range(2.0f, 8.0f); // 2에서 8까지 랜덤 넘버를 잡는다 >> 타이밍 카운터가 움직일 범위
            temNumber = (int)(randomNumber * 100f);
            FrameWorkManager.perfectFloor.rectTransform.anchoredPosition = new Vector3(temNumber, FrameWorkManager.perfectFloor.rectTransform.anchoredPosition.y);
            timingValue = 0;
            FrameWorkManager.timingSlider.value = timingValue;
            timingChangeDirection = true;

            FrameWorkManager.timingSlider.gameObject.SetActive(true);
            timingGameActive = true;
            StartCoroutine("ChangeTimingValue", 0.1f);
        }
    }
    public override IEnumerator ChangeTimingValue(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (timingGameActive)
        {
            if (timingChangeDirection && timingValue < 10)
            {
                FrameWorkManager.timingSlider.value = timingValue;
                timingValue += 0.2f;
                StartCoroutine("ChangeTimingValue", 0.0075f);
            }
            else if (!timingChangeDirection && timingValue > 0)
            {
                FrameWorkManager.timingSlider.value = timingValue;
                timingValue -= 0.2f;
                StartCoroutine("ChangeTimingValue", 0.0075f);
            }
            else
            {
                timingChangeDirection = !timingChangeDirection;
                StartCoroutine("ChangeTimingValue", 0.0075f);
                //timingGameActive = false;
            }
        }
    }

    public override IEnumerator CountTime(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }
}
