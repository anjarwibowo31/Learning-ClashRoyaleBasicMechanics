using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SingleParticipantData
{
    public Dictionary<string, CardSO> CardContainerDictionary { get; private set; }
    public List<GameObject> RestrictionAreaList { get; set; }
    public List<IDamageable> DamageableList { get; set; }
    public List<Tower> TowerList { get; private set; }
    public CardSO[] CardOwnedArray { get => cardOwned; set => cardOwned = value; }
    public int Score { get; set; } = 0;
    public float TotalMaxHealth { get; set; }
    public float TotalCurrentHealth { get; set; }
    public float ElixirAmount { get; set; }

    public Participant partyName;
    public Material partyFlag;

    [SerializeField] private CardSO[] cardOwned;
    [SerializeField] private float startingElixir;

    public SingleParticipantData()
    {
        ElixirAmount = startingElixir;
        RestrictionAreaList = new();
        TowerList = new();
        CardContainerDictionary = new();
        DamageableList = new();
    }
}

public class ParticipantDataManager : MonoBehaviour
{
    public static ParticipantDataManager Instance { get; private set; }

    public event EventHandler OnDamageableRemoved;
    public event EventHandler OnDamageableAdded;

    public Dictionary<Participant, SingleParticipantData> ParticipantDictionary { get; private set; } = new();

    const float maxElixir = 10;

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

        foreach (SingleParticipantData participant in singleParticipantDataArray)
        {
            ParticipantDictionary.Add(participant.partyName, participant);

            foreach (CardSO card in participant.CardOwnedArray)
            {
                participant.CardContainerDictionary.Add(card.CardName, card);
            }
        }
    }

    public void AddDamageable(IDamageable damageable, Participant participant)
    {
        ParticipantDictionary[participant].DamageableList.Add(damageable);
        OnDamageableAdded?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveDamageable(IDamageable damageable, Participant participant)
    {
        ParticipantDictionary[participant].DamageableList.Remove(damageable);
        OnDamageableRemoved?.Invoke(this, EventArgs.Empty);
    }
}
