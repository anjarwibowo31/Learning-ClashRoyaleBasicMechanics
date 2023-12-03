using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance { get; private set; }

    [SerializeField] private Material dropAreaMaterial;
    [SerializeField] private Material grassMaterial;
    [SerializeField] private MeshRenderer battleArea;

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
        foreach (SingleParticipantData singleParticipantData in ParticipantDataManager.Instance.ParticipantDictionary.Values)
        {
            foreach (GameObject gameObject in singleParticipantData.RestrictionAreaList)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void UpdateArea(CardSO cardSelected, Participant thisParty, Participant opposite)
    {
        if (cardSelected == null)
        {
            battleArea.material = grassMaterial;
            return;
        }
        else
        {
            battleArea.material = dropAreaMaterial;
        }

        if (cardSelected.CardDeployLocation == CardDeployLocation.Any)
        {
            foreach (Participant participant in ParticipantDataManager.Instance.ParticipantDictionary.Keys)
            {
                foreach (GameObject gameObject in ParticipantDataManager.Instance.ParticipantDictionary[participant].RestrictionAreaList)
                {
                    gameObject.SetActive(true);
                }
            }
        }
        else if (cardSelected.CardDeployLocation == CardDeployLocation.Limited)
        {
            foreach (GameObject gameObject in ParticipantDataManager.Instance.ParticipantDictionary[opposite].RestrictionAreaList)
            {
                gameObject.SetActive(true);
            }

            foreach (GameObject gameObject in ParticipantDataManager.Instance.ParticipantDictionary[thisParty].RestrictionAreaList)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
