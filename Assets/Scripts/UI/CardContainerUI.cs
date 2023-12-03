using System.Collections.Generic;
using UnityEngine;

public class CardContainerUI : MonoBehaviour
{
    public static CardContainerUI Instance { get; private set; }

    private List<CardSO> cardOwnedList = new List<CardSO>();
    private List<CardButtonUI> cardButtonList = new List<CardButtonUI>();
    private SingleParticipantData participantData;

    [SerializeField] private GameObject cardButtonUIObject;

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
        participantData = ParticipantDataManager.Instance.ParticipantDictionary[Participant.Enemy];

        foreach (CardSO card in participantData.CardOwnedArray)
        {
            cardOwnedList.Add(card);
        }

        foreach (CardSO card in cardOwnedList)
        {
            GameObject g = Instantiate(cardButtonUIObject, this.transform);
            g.GetComponent<CardButtonUI>().SetCardButton(card);
            cardButtonList.Add(g.GetComponent<CardButtonUI>());
        }
    }

    public void UpdateCardSelected(CardSO card)
    {
        foreach (CardButtonUI cardButtonUI in cardButtonList)
        {
            if (cardButtonUI.Card.CardName == card.CardName)
            {
                cardButtonUI.SelectedImage.enabled = true;
            }
            else
            {
                cardButtonUI.SelectedImage.enabled = false;
            }
        }
    }
}
