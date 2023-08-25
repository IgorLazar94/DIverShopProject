using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject HeaderPanel;
    [SerializeField] private GameObject InputPanel;
    [SerializeField] private GameObject KitchenPanel;
    [SerializeField] private GameObject debugPanel;
    [SerializeField] private GameObject trainingPanel;
    [SerializeField] private GameObject sidePanel;
    [SerializeField] private GameObject soundPanel;
    [SerializeField] private TextMeshProUGUI kitchenCurrentAFish;
    [SerializeField] private TextMeshProUGUI kitchenCurrentBFish;
    [SerializeField] private TextMeshProUGUI kitchenCurrentCFish;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private List<KitchenCard> kitchenCards = new List<KitchenCard>();
    [SerializeField] private Slider musicSlider, sfxSlider;
    private List<Parameter> parameters = new List<Parameter>();
    private Kitchen kitchen;
    private void Start()
    {
        FillKitchenCardsList();
        FillParameters();
        UpdateCurrentFishText(0, 0, 0);
        KitchenPanel.SetActive(false);
        trainingPanel.SetActive(false);
    }

    private void OnEnable()
    {
        DebugZone.OnEnterDebug += InDebug;
        DebugZone.OnExitDebug += OutDebug;
    }

    private void OnDisable()
    {
        DebugZone.OnEnterDebug -= InDebug;
        DebugZone.OnExitDebug -= OutDebug;
    }

    private void HideTutorialText()
    {
        tutorialText.rectTransform.DOScale(Vector3.zero, 0.25f).OnComplete(() => tutorialText.gameObject.SetActive(false));
    }

    private void FillKitchenCardsList()
    {
        kitchenCards = gameObject.GetComponentsInChildren<KitchenCard>().ToList();
    }

    private void FillParameters()
    {
        parameters = gameObject.GetComponentsInChildren<Parameter>().ToList();
    }

    public void ShowKitchenUI()
    {
        HidePanel(sidePanel);
        HidePanel(InputPanel);
        ShowPanel(KitchenPanel);
    }

    public void HideKitchenUI()
    {
        HidePanel(KitchenPanel);
        ShowPanel(sidePanel);
        ShowPanel(InputPanel);
    }

    public void ShowSoundPanel()
    {
        HidePanel(sidePanel);
        HidePanel(InputPanel);
        ShowPanel(soundPanel);
    }

    public void HideSoundPanel()
    {
        HidePanel(soundPanel);
        ShowPanel(sidePanel);
        ShowPanel(InputPanel);
    }

    private void HidePanel(GameObject panel)
    {
        panel.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => panel.SetActive(false));
    }

    private void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
        panel.transform.DOScale(Vector3.one, 0.25f);
    }

    public void UpdateCurrentFishText(int fishACount, int fishBCount, int fishCCount)
    {
        kitchenCurrentAFish.text = " - " + fishACount.ToString();
        kitchenCurrentBFish.text = " - " + fishBCount.ToString();
        kitchenCurrentCFish.text = " - " + fishCCount.ToString();
        CheckReadyCards(fishACount, fishBCount, fishCCount);
    }

    private void CheckReadyCards(int fishACount, int fishBCount, int fishCCount)
    {
        foreach (var card in kitchenCards)
        {
            card.SwitchButtonReadyStatus(fishACount, fishBCount, fishCCount);
        }
    }

    public void SetKitchen(Kitchen _kitchen)
    {
        kitchen = _kitchen;
    }

    public void RequireToCookFoodA()
    {
        kitchen.CookedFriedFish();
    }
    public void RequireToCookFoodB()
    {
        kitchen.CookedSandwich();
    }
    public void RequireToCookFoodC()
    {
        kitchen.CookedFishburger();
    }

    public void ShowTutorialMessage(string message)
    {
        tutorialText.text = message;
        tutorialText.gameObject.SetActive(true);
        Sequence tutorialSequence = DOTween.Sequence();
        tutorialSequence.Append(tutorialText.rectTransform.DOScale(Vector3.one, 0.5f)).SetEase(Ease.InQuint);
        tutorialSequence.AppendInterval(2.5f);
        tutorialSequence.AppendCallback(HideTutorialText);
    }



    public void ShowTrainingPanel()
    {
        HidePanel(sidePanel);
        HidePanel(InputPanel);
        ShowPanel(trainingPanel);
        CheckTrainingButtonsActive();
    }

    public void CheckTrainingButtonsActive()
    {
        foreach (var parameter in parameters)
        {
            parameter.CheckReadyUpdateButton();
        }
    }

    public void HideTrainingPanel()
    {
        ShowPanel(sidePanel);
        HidePanel(trainingPanel);
        ShowPanel(InputPanel);
    }

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(sfxSlider.value);
    }

    // Debug
    private void InDebug()
    {
        HidePanel(InputPanel);
        debugPanel.SetActive(true);
    }

    private void OutDebug()
    {
        debugPanel.SetActive(false);
        ShowPanel(InputPanel);
    }
}
