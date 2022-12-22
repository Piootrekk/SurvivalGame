using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarSlots : MonoBehaviour
{
    [SerializeField] Transform hotBarSlots;
    private InputManager inputManager;


    public void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }
    void Update()
    {
        HotBarChange();
    }

    public void HotBarChange()
    {
        for (int i = 1; i <= 10; i++)
        {
            if (inputManager.HotBarKey == (float)i)
            {
                //Color in HEX: 2E2626
                hotBarSlots.GetChild(i - 1).gameObject.GetComponent<Image>().color = new Color(0.18039216f, 0.145098f, 0.14901961f);
                hotBarSlots.GetChild(i - 1).gameObject.GetComponent<ActiveSlot>().IsActive = true;
            }
            // Color in HEX: #4F3E3E
            else
            {
                hotBarSlots.GetChild(i - 1).gameObject.GetComponent<Image>().color = new Color(0.3098039f, 0.24313726f, 0.24313726f);
                hotBarSlots.GetChild(i - 1).gameObject.GetComponent<ActiveSlot>().IsActive = false;
            }
                
            

        }

    }
}
