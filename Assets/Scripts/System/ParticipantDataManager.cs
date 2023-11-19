using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SingleParticipantData
{
    public GameObject[] CardOwned { get => cardOwned; set => cardOwned = value; }
    public List<Tower> TowerList { get; private set; }
    public int Score { get; set; } = 0;
    public float TotalMaxHealth { get; set; }
    public float TotalCurrentHealth { get; set; }
    public Dictionary<string, GameObject> CardContainer { get; private set; }

    public Participant partyName;
    public Material partyFlag;

    [SerializeField] private GameObject[] cardOwned;

    public SingleParticipantData()
    {
        TowerList = new List<Tower>();
        CardContainer = new Dictionary<string, GameObject>();
    }
}

public class ParticipantDataManager : MonoBehaviour
{
    public static ParticipantDataManager Instance {  get; private set; }

    public Dictionary<Participant, SingleParticipantData> ParticipantDictionary { get; private set; }

    [SerializeField] private SingleParticipantData[] participantDataArray;


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

        ParticipantDictionary = new Dictionary<Participant, SingleParticipantData>();

        foreach (var participant in participantDataArray)
        {
            ParticipantDictionary.Add(participant.partyName,participant);

            foreach (GameObject card in participant.CardOwned)
            {
                participant.CardContainer.Add(card.GetComponent<Card>().CardName, card);
            }
        }
    }
}
