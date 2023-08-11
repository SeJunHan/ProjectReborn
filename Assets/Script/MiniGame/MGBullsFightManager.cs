using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MGBullsFightManager : MiniGameManager
{

    private Button _attackButton;
    private Button _shieldButton;
    private Button _ultimateButton;

    protected override void Start()
    {
        base.Start();
        var root = GetComponent<UIDocument>().rootVisualElement;

        // 공방필 버튼
        _attackButton = root.Q<Button>("AttackButton");
        _shieldButton = root.Q<Button>("ShieldButton");
        _ultimateButton = root.Q<Button>("UltimateButton");

        // 버튼이 할 일
        _attackButton.RegisterCallback<ClickEvent>(OnAttackButtonClicked);
        _shieldButton.RegisterCallback<ClickEvent>(OnShieldButtonClicked);
        _ultimateButton.RegisterCallback<ClickEvent>(OnUltimateButtonClicked);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void GameStart()
    {

    }
    public override void GameEnd(bool clear)
    {

    }

    private void OnAttackButtonClicked(ClickEvent evt)
    {
        Debug.Log("공격버튼 클릭");
    }
    private void OnShieldButtonClicked(ClickEvent evt)
    {
        Debug.Log("방어버튼 클릭");
    }
    private void OnUltimateButtonClicked(ClickEvent evt)
    {
        Debug.Log("필살기버튼 클릭");
    }
}
