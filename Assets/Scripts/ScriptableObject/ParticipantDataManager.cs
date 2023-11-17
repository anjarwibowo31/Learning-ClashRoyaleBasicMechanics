using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticipantData", menuName = "ParticipantData")]
public class ParticipantDataManager : MonoBehaviour
{
    public static ParticipantDataManager Instance {  get; private set; }

    [Serializable]
    public class SingleParticipantData
    {
        public Participant partyName;
        public Material partyFlag;
    }

    public SingleParticipantData[] participantDataArray;

    public Dictionary<Participant, SingleParticipantData> participantDictionary = new Dictionary<Participant, SingleParticipantData>();

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

        foreach (var participant in participantDataArray)
        {
            participantDictionary.Add(participant.partyName,participant);
            Debug.Log(participantDictionary[participant.partyName]);
        }
    }
}