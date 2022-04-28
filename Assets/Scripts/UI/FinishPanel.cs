using Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishPanel : UIPanel
{
    [SerializeField] Text _finishHeaderText;
    [SerializeField] Text _buttonText;
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        CloseAndClear();
        GameManager.EventManager.OnLevelFinished += SetHeaderText;
    }

    private void OnDestroy()
    {
        GameManager.EventManager.OnLevelFinished -= SetHeaderText;
    }


    public void SetHeaderText(bool isSuccess)
    {
        ShowPanel();
        if (isSuccess)
        {
            _finishHeaderText.text = "Level Completed";
            _buttonText.text = "NEXT";
        }
        else
        {
            _finishHeaderText.text = "Level Failed";
            _buttonText.text = "RETRY";
        }
    }

    public void CloseAndClear()
    {
        _finishHeaderText.text = "";
        _buttonText.text = "";
        HidePanel();
    }

    public void NextButton()
    {
        GameManager.EventManager.LevelStarted();
    }


}
