using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class MiniGameManager : MonoBehaviour
{
    protected int GameType;
    protected bool panelActiveSelf;
    protected UIFrameWorkManager FrameWorkManager;
    protected virtual void Start()
    {
        Character.instance.transform.position = new Vector3(0f, 0f, 0f);
        FrameWorkManager = GameObject.Find("UIMinigameManager").GetComponent<UIFrameWorkManager>();
    }
    public abstract void GameStart();
    public abstract void GameEnd(bool clear);
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeKeyPress();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftArrowKeyPress();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightArrowKeyPress();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpArrowKeyPress();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DownArrowKeyPress();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            XKeyPress();
        }        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceKeyPress();
        }
    }

    public virtual void SetRound(int num) { }
    public virtual void SetGame() { }
    public virtual void SetMainWork() { }
    public virtual void SetMainWork(int num) { }
    public virtual void EscapeKeyPress() { }
    public virtual void LeftArrowKeyPress() { }
    public virtual void RightArrowKeyPress() { }
    public virtual void UpArrowKeyPress() { }
    public virtual void DownArrowKeyPress() { }
    public virtual void XKeyPress() { }
    public virtual void SpaceKeyPress() { }

    public virtual void Generate() { }
    public virtual void PressKey(int num) { }
    public int GetGameType() { return GameType; }
    public void SetPanelActive(bool active)
    {
        panelActiveSelf = active;
    }
    public virtual IEnumerator CountTime(float delayTime) { yield return new WaitForSeconds(delayTime); }
    public virtual IEnumerator ChangeTimingValue(float delayTime) { yield return new WaitForSeconds(delayTime); }
}
