using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButtonManager : MonoBehaviour
{
    [SerializeField] GameObject helpPanel = null;
    [SerializeField] Image buttonImage;
    [SerializeField] Sprite litSprite;
    [SerializeField] Sprite unlitSprite;
    [SerializeField] Animator animator;
    [HideInInspector] public string animationToPlay = "";

    private void Start()
    {
        if(animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void OnEnable()
    {
        EventHandler.ChangeAnimation += GetAnimationName;
    }

    private void OnDisable()
    {
        EventHandler.ChangeAnimation -= GetAnimationName;
    }

    public void ShowHidePanel()
    {
        helpPanel.SetActive(!helpPanel.activeInHierarchy);
        if(buttonImage.sprite == litSprite)
        {
            buttonImage.sprite = unlitSprite;
        }
        else
        {
            buttonImage.sprite = litSprite;
        }
    }

    private void GetAnimationName(string animationToPlay)
    {
        this.animationToPlay = animationToPlay;   
    }
}
