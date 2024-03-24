using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideUnhideMenu : MonoBehaviour
{
    private string hiddenText = "H                   - Show Keybinds";
    private string unhiddenText = "Alt + F4          - Quit\r\nR                   - Reload Scene\r\nWASD            - Move\r\nLeft Mouse     - Attack\r\nMiddle Mouse - Pickup\r\nSpace            - Jump\r\nC                   - Crouch\r\nCtrl                - Slide\r\nShift               - Sprint\r\nH                   - Hide this menu";

    private bool hidden;

    private void Start()
    {
        hidden = true;

        HandleHidden();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hidden = !hidden;

            HandleHidden();
        }
    }

    private void HandleHidden()
    {
        if (hidden)
        {
            GetComponent<TextMeshProUGUI>().text = hiddenText;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = unhiddenText;
        }
    }
}
