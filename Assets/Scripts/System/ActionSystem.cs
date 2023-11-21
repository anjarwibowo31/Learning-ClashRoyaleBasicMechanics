using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    public static ActionSystem Instance { get; private set; }

    private Participant ownSide = Participant.Player;
    private Participant opposite;

    [SerializeField] private Card cardSelected;

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

        if (ownSide == Participant.Player)
        {
            opposite = Participant.Enemy;
        }
        else
        {
            opposite = Participant.Player;
        }
    }

    private void Start()
    {
        foreach (SingleParticipantData singleParticipantData in ParticipantDataManager.Instance.ParticipantDictionary.Values)
        {
            foreach (GameObject gameObject in singleParticipantData.RestrictionArea)
            {
                gameObject.SetActive(false);
            }
        }

        if (cardSelected == null)
        {

        }
    }

    public void SetSelectedCard(Card cardSelected)
    {
        this.cardSelected = cardSelected;

        AreaManager.Instance.UpdateArea(cardSelected, opposite);
    }

    public Card GetSelectedCard()
    {
        return cardSelected;
    }
}
