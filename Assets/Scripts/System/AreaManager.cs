using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance { get; private set; }

    public Dictionary<Participant, List<GameObject>> ParticipantAreaDictionary { get => participantAreaDictionary; set => participantAreaDictionary = value; }

    private Dictionary<Participant, List<GameObject>> participantAreaDictionary = new Dictionary<Participant, List<GameObject>>();


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
        participantAreaDictionary.Add(Participant.Player, ParticipantDataManager.Instance.ParticipantDictionary[Participant.Player].RestrictionArea);
        participantAreaDictionary.Add(Participant.Enemy, ParticipantDataManager.Instance.ParticipantDictionary[Participant.Enemy].RestrictionArea);
    }
}
