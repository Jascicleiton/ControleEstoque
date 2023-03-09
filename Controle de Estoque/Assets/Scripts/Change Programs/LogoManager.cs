using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoManager : MonoBehaviour
{
    [SerializeField] private Sprite esfSprite;
    [SerializeField] private Sprite fumsoftSprite;
    [SerializeField] private Sprite testingSprite;
    [SerializeField] private Sprite clientesSprite;
    [SerializeField] private Sprite concerttSprite;

    private Image image;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
    }

    /// <summary>
    /// Show or hide an extra logo depending of which "estoque" is being used
    /// </summary>
    private void Start()
    {
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                image.color = new Color(0f, 0f, 0f, 0f);
                break;
            case CurrentEstoque.Funsoft:
                image.sprite = fumsoftSprite;
                break;
            case CurrentEstoque.ESF:
                image.sprite = esfSprite;
                break;
            case CurrentEstoque.Testing:
                image.sprite = testingSprite;
                break;
            case CurrentEstoque.Clientes:
                image.sprite = clientesSprite;
                break;
            case CurrentEstoque.Concert:
                image.sprite = concerttSprite;
                break;
            default:
                this.gameObject.SetActive(false);
                break;
        }
    }
}
