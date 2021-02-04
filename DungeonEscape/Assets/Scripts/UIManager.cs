using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{

    public GameObject InfoBox, InfoBoxTargetOnScreen, InfoBoxTargetOffScreen;
    private bool IsInfoBoxOnScreen = false;
    private bool InTransition = false;
    private string TempForTransition = "";
    private List<string> InfoBoxCards;

    public GameObject Skip, SkipTargetOnScreen, SkipTargetOffScreen;
    private bool IsSkipOnScreen = false;

    public GameObject ObjectiveText;

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
        InfoBoxOffScreen();
    }

    public void NextCardForInfoBox() 
    {
        if (this.InfoBoxCards != null && InfoBoxCards.Count > 0)
        {
            NextTextAnimationInfoBox(InfoBoxCards[0]);
            InfoBoxCards.RemoveAt(0);
        }
        else 
        {
            InfoBoxCards = null;
        }
    }

    public void PlayerInputNextCard(InputAction.CallbackContext context) 
    {
        if (!InfoBoxInTransition()) 
        {
            if (InfoBoxCards != null && InfoBoxCards.Count > 0)
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
}
