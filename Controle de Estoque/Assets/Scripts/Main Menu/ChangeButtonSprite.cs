using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonSprite : MonoBehaviour
{
    private Button button;
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    public void ChangeSprite()
    {
        if(button.image.sprite == sprite1)
        {
            button.image.sprite = sprite2;
        }
        else if(button.image.sprite == sprite2)
        {
            button.image.sprite = sprite1;
        }
    }
    
}
