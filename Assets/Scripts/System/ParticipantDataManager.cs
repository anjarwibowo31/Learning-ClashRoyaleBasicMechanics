﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SingleParticipantData
{
    public Dictionary<string, GameObject> CardContainerDictionary { get; private set; }
    public List<GameObject> RestrictionAreaList { get; set; }
    public List<IDamageable> DamageableList { get; set; }
    public List<Tower> TowerList { get; private set; }
    public GameObject[] CardOwnedArray { get => cardOwned; set => cardOwned = value; }
    public int Score { get; set; } = 0;
    public float TotalMaxHealth { get; set; }
    public float TotalCurrentHealth { get; set; }

    public Participant partyName;
    public Material partyFlag;

    [SerializeField] private GameObject[] cardOwned;

    public SingleParticipantData()
    {
        RestrictionAreaList = new List<GameObject>();
        TowerList = new List<Tower>();
        CardContainerDictionary = new Dictionary<string, GameObject>();
        DamageableList = new List<IDamageable>();
    }
}

public class ParticipantDataManager : MonoBehaviour
{
    public static ParticipantDataManager Instance { get; private set; }

    public Dictionary<Participant, SingleParticipantData> ParticipantDictionary { get; private set; } =
    new Dictionary<Participant, SingleParticipantData>();

    [SerializeField] private SingleParticipantData[] singleParticipantDataArray;

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

        foreach (var participant in singleParticipantDataArray)
        {
            ParticipantDictionary.Add(participant.partyName, participant);

            foreach (GameObject card in participant.CardOwnedArray)
            {
                participant.CardContainerDictionary.Add(card.GetComponent<Card>().CardName, card);
            }
        }
    }
}
