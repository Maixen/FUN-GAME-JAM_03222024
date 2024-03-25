using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    // 1 Move
    // 2 Rotate
    // 3 Jump
    // 4 Crouch
    // 5 Slide
    // 6 R to reload scene

    // 7 Next to Spawn Point is a item
    // 8 Hold middle mouse button
    // 9 Bring to receiver
    // 10 go to enemy
    // 11 Kill enemy
    // 12 Bring back object
    // 13 Zones where player steps into
    // 14 Bring back object
    // 15 Red border
    // 16 Blue border
    // 17 Enemy Zone
    // 18 Bring back object

    private int step;

    [SerializeField]
    private TextMeshProUGUI instructionsText;

    [SerializeField]
    private float extraTime;

    [SerializeField]
    private float defaultTime;

    private bool inNextStep;

    // ------------------------

    [SerializeField]
    private LayerMask player;
    [SerializeField]
    private LayerMask enemy;

    [SerializeField]
    private float defaultRadius;

    [SerializeField]
    private Transform receiver;

    [SerializeField]
    private Transform enemy1;

    [SerializeField]
    private Transform enemy2;

    [SerializeField]
    private Transform playerZone;

    private void Start()
    {
        inNextStep = false;
        InitiateNextStep(extraTime);
    }

    private void Update()
    {
        switch (step)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.D))
                    InitiateNextStep(extraTime);
                break;
            case 2:
                if (Input.GetAxis("Mouse X") >= 1 | Input.GetAxis("Mouse Y") >= 1)
                    InitiateNextStep(extraTime);
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Space))
                    InitiateNextStep(extraTime);
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.C))
                    InitiateNextStep(extraTime);
                break;
            case 5:
                if (Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D)))
                    InitiateNextStep(extraTime);
                break;
            case 6:
                InitiateNextStep(defaultTime);
                break;
            case 7:
                InitiateNextStep(defaultTime);
                break;
            case 8:
                if (Input.GetMouseButtonDown(2))
                    InitiateNextStep(extraTime);
                break;
            case 9:
                if (Physics.OverlapSphere(receiver.position, defaultRadius, player).Length != 0)
                    InitiateNextStep(extraTime);
                break;
            case 10:
                if (Physics.OverlapSphere(enemy1.position, defaultRadius, player).Length != 0)
                    InitiateNextStep(extraTime);
                break;
            case 11:
                if (Physics.OverlapSphere(enemy1.position, defaultRadius, enemy).Length == 0)
                    InitiateNextStep(extraTime);
                break;
            case 12:
                if (Physics.OverlapSphere(receiver.position, defaultRadius, player).Length != 0)
                    InitiateNextStep(extraTime);
                break;
            case 13:
                if (Physics.OverlapSphere(playerZone.position, defaultRadius, player).Length != 0)
                    InitiateNextStep(extraTime);
                break;
            case 14:
                if (Physics.OverlapSphere(receiver.position, defaultRadius, player).Length != 0)
                    InitiateNextStep(extraTime);
                break;
            case 15:
                InitiateNextStep(defaultTime);
                break;
            case 16:
                InitiateNextStep(defaultTime);
                break;
            case 17:
                if (Physics.OverlapSphere(enemy2.position, defaultRadius, enemy).Length == 0)
                    InitiateNextStep(extraTime);
                break;
            case 18:
                InitiateNextStep(defaultTime);
                break;
            case 19:
                break;
        }
    }

    private void InitiateNextStep(float timeToWait)
    {
        if (inNextStep) { return; }

        inNextStep = true;

        Invoke(nameof(NextStep), timeToWait);
    }

    // 1 Move
    // 2 Rotate
    // 3 Jump
    // 4 Crouch
    // 5 Slide
    // 6 R to reload scene

    // 7 Next to Spawn Point is a item
    // 8 Hold middle mouse button
    // 9 Bring to receiver
    // 10 go to enemy
    // 11 Kill enemy
    // 12 Bring back object
    // 13 Zones where player steps into
    // 14 Bring back object
    // 15 Red border
    // 16 Blue border
    // 17 Enemy Zone
    // 18 Bring back object

    private void NextStep()
    {
        step++;

        switch (step)
        {
            case 1:
                instructionsText.text = $"Hey Soldier, welcome to training! You can move by using WASD";
                break;
            case 2:
                instructionsText.text = $"And you can look around by using your mouse";
                break;
            case 3:
                instructionsText.text = $"Press Space to jump";
                break;
            case 4:
                instructionsText.text = $"And you can use C to get through narrow obstacles";
                break;
            case 5:
                instructionsText.text = $"Press Ctrl and WASD to perform a power slide";
                break;
            case 6:
                instructionsText.text = $"Dont press it but if you ever want to reload the current level, just press R";
                break;
            case 7:
                instructionsText.text = $"Ok ok, lets get started! You see that important component next to your spawn point?";
                break;
            case 8:
                instructionsText.text = $"Pick it up through aiming at it and then holding middle mouse button, but you need to be close to it";
                break;
            case 9:
                instructionsText.text = $"On the other side of the map is the receiver! You need to deliver all components in order to get to the next level";
                break;
            case 10:
                instructionsText.text = $"Next to that blue wall is an enemy, go there";
                break;
            case 11:
                instructionsText.text = $"Kill the enemy by standing close to it and pressing your left mouse button";
                break;
            case 12:
                instructionsText.text = $"Ah, it dropped a component! Only some enemys drop one! Bring it back to the receiver";
                break;
            case 13:
                instructionsText.text = $"Good job! You sometimes get rewarded when you enter a certain region. There is a big red mushroom on the map, search for it";
                break;
            case 14:
                instructionsText.text = $"Lets get that component back to the receiver";
                break;
            case 15:
                instructionsText.text = $"Between, the red wall lets you pass through but a component cant pass through";
                break;
            case 16:
                instructionsText.text = $"The same with the blue one but reversed";
                break;
            case 17:
                instructionsText.text = $"We need one more component! Next to the receiver is a small island, kill the enemys! You can also switch your weapon to a pistol using Q. It has more reach, but deals less damage";
                break;
            case 18:
                instructionsText.text = $"You saw that? Ones there were no enemys in the area anymore, a component dropped! For your information, you can heal by going to one of these green heal drops";
                break;
            case 19:
                instructionsText.text = $"Good job, now, get that  component to the receiver as well";
                break;
        }

        inNextStep = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(receiver.position, defaultRadius);
        Gizmos.DrawWireSphere(enemy1.position, defaultRadius);
        Gizmos.DrawWireSphere(enemy2.position, defaultRadius);
        Gizmos.DrawWireSphere(playerZone.position, defaultRadius);
    }
}
