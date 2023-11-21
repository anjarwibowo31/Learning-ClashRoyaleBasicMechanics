using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    public static ActionSystem Instance { get; private set; }

    private Participant player = Participant.Player;

    private Card cardSelected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //foreach (GameObject card in ParticipantDataManager.Instance.ParticipantDictionary[player].CardOwned)
        //{

        //}
    }

    private void Update()
    {

    }

    public void SetSelectedCard(Card cardSelected)
    {
        this.cardSelected = cardSelected;
    }

    public Card GetSelectedCard()
    {
        return cardSelected;
    }
}
