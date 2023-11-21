using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardContainerUI : MonoBehaviour
{
    public static CardContainerUI Instance { get; private set; }

    [SerializeField] private GameObject cardButtonUIObject;

    private List<GameObject> cardOwnedList = new List<GameObject>();
    private List<CardButtonUI> cardButtonList = new List<CardButtonUI>();
    private SingleParticipantData participantData;

    // (DONE) CALL SEMUA CARD OWNED BY PLAYER
    // (DONE) BUAT UI OBJECT UNTUK TIAP CARD
    // (DONE) SET TIAP KOMPONEN DALAM CARD UI --> CALL METHOD DARI CARD BUTTON UI
    // (DONE) BUAT LIISTENER UNTUK CARD SELECTION KE ACTION SYSTEM

    // BUAT UPDATE VISUAL
    // MASUKAN KE POOLING

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
        participantData = ParticipantDataManager.Instance.ParticipantDictionary[Participant.Player];

        foreach (GameObject gameObject in participantData.CardOwned)
        {
            cardOwnedList.Add(gameObject);
        }

        foreach (GameObject gameObject in cardOwnedList)
        {
            GameObject g = Instantiate(cardButtonUIObject, this.transform);
            g.GetComponent<CardButtonUI>().SetCardButton(gameObject.GetComponent<Card>());
            cardButtonList.Add(g.GetComponent<CardButtonUI>());
        }
    }

    public void UpdateCardSelected(Card card)
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
