using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomName : MonoBehaviour
{
    private string[] names = 
    {
        "[Computer-Bug]\r\n--MAIXEN--",
        "[Computer-Bug]\r\n--BUTTER--",
        "[Computer-Bug]\r\n--SY (Siege addict)--",
        "[Computer-Bug]\r\nname: Zig",
        "[Computer-Bug]\r\nname: Bob",
        "[Computer-Bug]\r\nname: Grim",
        "[Computer-Bug]\r\nname: Dave",
        "[Computer-Bug]\r\nname: Max",
        "[Computer-Bug]\r\nname: Sam",
        "[Computer-Bug]\r\nname: Ted",
        "[Computer-Bug]\r\nname: Ben",
        "[Computer-Bug]\r\nname: Jill",
        "[Computer-Bug]\r\nname: Jack",
        "[Computer-Bug]\r\nname: Luke",
        "[Computer-Bug]\r\nname: Seth",
        "[Computer-Bug]\r\nname: Cole",
        "[Computer-Bug]\r\nname: Finn",
        "[Computer-Bug]\r\nname: Will",
        "[Computer-Bug]\r\nname: Ty",
        "[Computer-Bug]\r\nname: Nate",
        "[Computer-Bug]\r\nname: Blake",
        "[Computer-Bug]\r\nname: Jake"
    };


    [SerializeField]
    private Transform player;
    [SerializeField]
    private float rangeToScale;
    [SerializeField]
    private float rangeToDisapear;

    private TextMeshPro targetText;
    private float startSize;

    private void Start()
    {
        targetText = GetComponent<TextMeshPro>();
        startSize = targetText.fontSize;
        targetText.text = names[Random.Range(0, names.Length)];
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > rangeToDisapear)
        {
            targetText.enabled = false;
        }
        else if (Vector3.Distance(transform.position, player.position) > rangeToScale)
        {
            targetText.enabled = true;
            targetText.fontSize = startSize / 2;
        }
        else
        {
            targetText.enabled = true;
            targetText.fontSize = startSize;
        }
    }
}
