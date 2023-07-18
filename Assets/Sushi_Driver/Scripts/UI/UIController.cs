using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject HeaderPanel;
    [SerializeField] private GameObject InputPanel;
    [SerializeField] private GameObject KitchenPanel;



    public void ShowKitchenUI ()
    {
        HidePanel(HeaderPanel);
        HidePanel(InputPanel);
        ShowPanel(KitchenPanel);
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
}
