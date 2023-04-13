using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] HelpButtonManager helpButtonManager;
    [SerializeField] Animator animator;

    private void OnEnable()
    {
        if(helpButtonManager == null)
        {
            helpButtonManager = GetComponentInParent<HelpButtonManager>();
        }
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if(helpButtonManager.animationToPlay == "HelpMovement")
        {
            animator.Play("HelpMovement");
        }
        else if(helpButtonManager.animationToPlay == "HelpMovement2")
        {
            animator.Play("HelpMovement2");
        }
        else
        {
            animator.Play("HelpMovement");
        }
    }
}
