using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoManager : MonoBehaviour
{
    [SerializeField] private Sprite esfSprite;
    [SerializeField] private Sprite fumsoftSprite;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                this.gameObject.SetActive(false);
                break;
            case CurrentEstoque.Funsoft:
                image.sprite = fumsoftSprite;
                break;
            case CurrentEstoque.ESF:
                image.sprite = esfSprite;
                break;
            default:
                break;
        }
    }
}
