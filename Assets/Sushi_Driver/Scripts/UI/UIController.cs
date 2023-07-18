using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Linq;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject HeaderPanel;
    [SerializeField] private GameObject InputPanel;
    [SerializeField] private GameObject KitchenPanel;

    [SerializeField] private TextMeshProUGUI kitchenCurrentAFish;
    [SerializeField] private TextMeshProUGUI kitchenCurrentBFish;
    [SerializeField] private TextMeshProUGUI kitchenCurrentCFish;

    [SerializeField] private List<KitchenCard> kitchenCards = new List<KitchenCard>();


    private void Start()
    {
        FillKitchenCardsList();
        KitchenPanel.SetActive(false);
    }

    private void FillKitchenCardsList()
    {
        kitchenCards = gameObject.GetComponentsInChildren<KitchenCard>().ToList();
    }

    public void ShowKitchenUI()
    {
        HidePanel(HeaderPanel);
        HidePanel(InputPanel);
        ShowPanel(KitchenPanel);
    }

    public void HideKitchenUI()
    {
        HidePanel(KitchenPanel);
        ShowPanel(HeaderPanel);
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
        CheckReadyCards(fishACount, fishBCount, fishACount);
    }

    private void CheckReadyCards(int fishACount, int fishBCount, int fishCCount)
    {
        foreach (var card in kitchenCards)
        {
            card.SwitchReadyStatus(fishACount, fishBCount, fishCCount);
        }
    }
}
