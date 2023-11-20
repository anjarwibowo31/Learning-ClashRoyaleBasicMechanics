using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    private Participant player = Participant.Player;

    Card card;

    private void Start()
    {
        foreach (GameObject card in ParticipantDataManager.Instance.ParticipantDictionary[player].CardOwned)
        {

        }
    }

    private void Update()
    {

    }
}
