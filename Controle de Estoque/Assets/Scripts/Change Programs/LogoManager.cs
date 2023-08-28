using Assets.Scripts.Inventory.Database;
using UnityEngine;
using UnityEngine.UIElements;

public class LogoManager : MonoBehaviour
{
    [SerializeField] private Sprite _esfSprite;
    [SerializeField] private Sprite _fumsoftSprite;
    [SerializeField] private Sprite _testingSprite;
    [SerializeField] private Sprite _clientesSprite;
    [SerializeField] private Sprite _concerttSprite;
    [SerializeField] private Sprite _quistoSprite;

    private VisualElement _logo;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _logo = root.Q<VisualElement>("ExtraLogo");
    }

    /// <summary>
    /// Show or hide an extra logo depending of which "estoque" is being used
    /// </summary>
    private void Start()
    {
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                _logo.style.display = DisplayStyle.None;
                break;
            case CurrentEstoque.Fumsoft:
                _logo.style.backgroundImage = new StyleBackground(_fumsoftSprite);
                break;
            case CurrentEstoque.ESF:
                _logo.style.backgroundImage = new StyleBackground(_esfSprite);
                break;
            case CurrentEstoque.Testing:
                _logo.style.backgroundImage = new StyleBackground(_testingSprite);
                break;
            case CurrentEstoque.Clientes:
                _logo.style.backgroundImage = new StyleBackground(_clientesSprite);
                break;
            case CurrentEstoque.Concert:
                _logo.style.backgroundImage = new StyleBackground(_concerttSprite);
                break;
            case CurrentEstoque.Quisto:
                _logo.style.backgroundImage = new StyleBackground(_quistoSprite);
                break;
            default:
                _logo.style.display = DisplayStyle.None;
                break;
        }
    }
}
