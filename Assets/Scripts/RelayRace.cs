using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelayRace : MonoBehaviour
{
    public Transform[] runners;
    public GameObject relayObjectPrefab;
    public float passDistance;
    public float runningSpeed;
    public Button startStopButton;

    public float yOffset = 1.0f;

    private int currentRunnerIndex = 0;
    private bool isRunning = false;
    private GameObject relayObjectInstance;

    private Vector3[] initialRunnerPositions;


    void Start()
    {
        initialRunnerPositions = new Vector3[runners.Length];
        for (int i = 0; i < runners.Length; i++)
        {
            initialRunnerPositions[i] = runners[i].position;
        }

        SetInitialRunnerPositions();

        relayObjectInstance = Instantiate(relayObjectPrefab, runners[currentRunnerIndex].position + new Vector3(0f, yOffset, 0f), Quaternion.identity);
        relayObjectInstance.transform.SetParent(runners[currentRunnerIndex]);
        startStopButton.onClick.AddListener(ToggleMovement);
    }

    void Update()
    {
        if (isRunning)
        {
            float distanceToNextRunner = Vector3.Distance(runners[currentRunnerIndex].position, runners[(currentRunnerIndex + 1) % runners.Length].position);

            if (distanceToNextRunner < passDistance)
            {
                relayObjectInstance.transform.SetParent(runners[(currentRunnerIndex + 1) % runners.Length]);
                relayObjectInstance.transform.localPosition = new Vector3(0f, yOffset, 0f);
                currentRunnerIndex = (currentRunnerIndex + 1) % runners.Length;
            }

            float step = runningSpeed * Time.deltaTime;
            runners[currentRunnerIndex].position = Vector3.MoveTowards(runners[currentRunnerIndex].position, runners[(currentRunnerIndex + 1) % runners.Length].position, step);

            if (currentRunnerIndex < runners.Length - 1)
            {
                runners[currentRunnerIndex].LookAt(runners[currentRunnerIndex + 1]);
            }
            else
            {
                runners[currentRunnerIndex].LookAt(runners[0]);
            }
        }
    }

    void ToggleMovement()
    {
        isRunning = !isRunning;
    }

    void SetInitialRunnerPositions()
    {
        for (int i = 0; i < runners.Length; i++)
        {
            runners[i].position = initialRunnerPositions[i];
        }
    }
}