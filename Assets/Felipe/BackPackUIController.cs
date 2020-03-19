using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackPackUIController : MonoBehaviour, IPointerDownHandler
{
    //GameManager.instance.backPack.potion25;
    public Text potionNumber25;
    public Text potionNumber50;
    public Text potionNumber100;
    public GameObject panel;

    public void OnPointerDown(PointerEventData eventData)
    {
        panel.SetActive(true);
        //potionNumber25.text = GameManager.instance.backPack.potion25.ToString();
        potionNumber25.text = GameManager.instance.backPack.potion25.ToString();
        potionNumber50.text = GameManager.instance.backPack.potion50.ToString();
        potionNumber100.text = GameManager.instance.backPack.potion100.ToString();
    }
}
