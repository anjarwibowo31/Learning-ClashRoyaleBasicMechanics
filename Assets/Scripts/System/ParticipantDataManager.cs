using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticipantDataManager : MonoBehaviour
{
    public static ParticipantDataManager Instance {  get; private set; }

    [Serializable]
    public class SingleParticipantData
    {
        public Participant partyName;
        public Material partyFlag;
    }

    public Dictionary<Participant, SingleParticipantData> participantDictionary { get; private set; }

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

        participantDictionary = new Dictionary<Participant, SingleParticipantData>();

        foreach (var participant in participantDataArray)
        {
            participantDictionary.Add(participant.partyName,participant);
        }
    }
}