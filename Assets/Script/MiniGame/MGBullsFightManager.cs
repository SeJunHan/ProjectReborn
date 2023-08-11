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

        // ������ ��ư
        _attackButton = root.Q<Button>("AttackButton");
        _shieldButton = root.Q<Button>("ShieldButton");
        _ultimateButton = root.Q<Button>("UltimateButton");

        // ��ư�� �� ��
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
        Debug.Log("���ݹ�ư Ŭ��");
    }
    private void OnShieldButtonClicked(ClickEvent evt)
    {
        Debug.Log("����ư Ŭ��");
    }
    private void OnUltimateButtonClicked(ClickEvent evt)
    {
        Debug.Log("�ʻ���ư Ŭ��");
    }
}
