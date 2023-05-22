using UnityEngine;
using UnityEngine.UIElements;

public class LogoManager : MonoBehaviour
{
    [SerializeField] private Sprite esfSprite;
    [SerializeField] private Sprite fumsoftSprite;
    [SerializeField] private Sprite testingSprite;
    [SerializeField] private Sprite clientesSprite;
    [SerializeField] private Sprite concerttSprite;
    [SerializeField] private Sprite quistoSprite;

    private VisualElement logo;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        logo = root.Q<VisualElement>("ExtraLogo");
    }

    /// <summary>
    /// Show or hide an extra logo depending of which "estoque" is being used
    /// </summary>
    private void Start()
    {
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                logo.style.display = DisplayStyle.None;
                break;
            case CurrentEstoque.Fumsoft:
                logo.style.backgroundImage = new StyleBackground(fumsoftSprite);
                break;
            case CurrentEstoque.ESF:
                logo.style.backgroundImage = new StyleBackground(esfSprite);
                break;
            case CurrentEstoque.Testing:
                logo.style.backgroundImage = new StyleBackground(testingSprite);
                break;
            case CurrentEstoque.Clientes:
                logo.style.backgroundImage = new StyleBackground(clientesSprite);
                break;
            case CurrentEstoque.Concert:
                logo.style.backgroundImage = new StyleBackground(concerttSprite);
                break;
            case CurrentEstoque.Quisto:
                logo.style.backgroundImage = new StyleBackground(quistoSprite);
                break;
            default:
                logo.style.display = DisplayStyle.None;
                break;
        }
    }
}
