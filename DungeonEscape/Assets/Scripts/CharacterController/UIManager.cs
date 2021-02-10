﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{

    public GameObject InfoBox, InfoBoxTargetOnScreen, InfoBoxTargetOffScreen, TimerText, TimerTextOnScreenTarget, TimerTextOffScreenTarget;
    private bool IsInfoBoxOnScreen = false;
    private bool InTransition = false;
    private string TempForTransition = "";
    private List<string> InfoBoxCards;
    private int cardCount = 0;

    public GameObject Skip, SkipTargetOnScreen, SkipTargetOffScreen;
    private bool IsSkipOnScreen = false;

    public GameObject ObjectiveText;

    public delegate void InfoBoxCardsEmpty();
    public event InfoBoxCardsEmpty OnInfoBoxCardsEmpty;

    private void Awake()
    {
        InfoBoxOffScreen();
        SkipTextOffScreen();
    }

    public void InfoBoxOnScreen() 
    {
        InfoBox.GetComponent<Spring>().SetTarget(InfoBoxTargetOnScreen);
        IsInfoBoxOnScreen = true;
    }

    public void InfoBoxOffScreen()
    {
        InfoBox.GetComponent<Spring>().SetTarget(InfoBoxTargetOffScreen);
        IsInfoBoxOnScreen = false;
    }

    public void SkipTextOnScreen() 
    {
        Skip.GetComponent<Spring>().SetTarget(SkipTargetOnScreen);
        IsSkipOnScreen = true;
    }

    public void SkipTextOffScreen() 
    {
        Skip.GetComponent<Spring>().SetTarget(SkipTargetOffScreen);
        IsSkipOnScreen = false;
    }

    public void TimerTextOnScreen() 
    {
        this.TimerText.GetComponent<Spring>().SetTarget(this.TimerTextOnScreenTarget);
    }

    public void TimerTextOffScreen()
    {
        this.TimerText.GetComponent<Spring>().SetTarget(this.TimerTextOffScreenTarget);
    }

    public void SetInfoBoxText(string s) 
    {
        transform.Find("InfoBox").Find("Image").Find("InfoBoxText").GetComponent<Text>().text = s;
    }

    public void SetObjectiveText(string s) 
    {
        ObjectiveText.GetComponent<Text>().text = s;
    }

    public void NextTextAnimationInfoBox(string s) 
    {
        TempForTransition = s;
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        InTransition = true;
        InfoBoxOffScreen();
        IsInfoBoxOnScreen = false;
        yield return new WaitForSeconds(0.5f);
        SetInfoBoxText(TempForTransition);
        InfoBoxOnScreen();
        IsInfoBoxOnScreen = true;
        yield return new WaitForSeconds(0.2f);
        InTransition = false;
    }

    public void SetInfoBoxCards(List<string> cards) 
    {
        this.InfoBoxCards = cards;
        cardCount = 0;
        InfoBoxOffScreen();
    }

    public void NextCardForInfoBox() 
    {
        if (this.InfoBoxCards != null && InfoBoxCards.Count > 0 && cardCount < InfoBoxCards.Count)
        {
            NextTextAnimationInfoBox(InfoBoxCards[cardCount++]);
        }
        else 
        {
            if (this.InfoBoxCards != null) OnInfoBoxCardsEmpty?.Invoke();
            SetInfoBoxCards(null);
        }
    }

    public bool InfoBoxHasCards() 
    {
        return this.InfoBoxCards != null && this.InfoBoxCards.Count > 0;
    }

    public void PlayerInputNextCard(InputAction.CallbackContext context) 
    {
        if (!InfoBoxInTransition()) 
        {
            if (InfoBoxCards != null)
            {
                NextCardForInfoBox();
            }
            else
            {
                InfoBoxOffScreen();
            }
        }
    }

    public bool InfoBoxInTransition() 
    {
        return InTransition;
    }

    public void ResetInfoCards() 
    {
        if (this.InfoBoxCards != null) OnInfoBoxCardsEmpty?.Invoke();
        SetInfoBoxCards(null);
    }
}
