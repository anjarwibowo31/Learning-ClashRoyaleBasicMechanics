using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SingleParticipantData
{
    public Participant partyName;
    public Material partyFlag;

    public List<Tower> TowerList { get; private set; }

    public int Score { get; set; } = 0;

    public float TotalMaxHealth { get; set; }
    public float TotalCurrentHealth { get; set; }

    public SingleParticipantData()
    {
        TowerList = new List<Tower>();
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
        }
    }
}