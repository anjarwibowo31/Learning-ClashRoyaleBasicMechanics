using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    Card card;
    private void Start()
    {
        card = ParticipantDataManager.Instance.ParticipantDictionary[Participant.Player].CardContainer["Log Launcher"].GetComponent<Card>();
    }

    private void Update()
    {
        print(card.CardName);
        print(card.CardType);
    }
}
