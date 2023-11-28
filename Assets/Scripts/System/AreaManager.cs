using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance { get; private set; }

    public Dictionary<Participant, List<GameObject>> ParticipantAreaDictionary { get => participantAreaDictionary; set => participantAreaDictionary = value; }

    private Dictionary<Participant, List<GameObject>> participantAreaDictionary = new Dictionary<Participant, List<GameObject>>();

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
        participantAreaDictionary.Add(Participant.Player, ParticipantDataManager.Instance.ParticipantDictionary[Participant.Player].RestrictionAreaList);
        participantAreaDictionary.Add(Participant.Enemy, ParticipantDataManager.Instance.ParticipantDictionary[Participant.Enemy].RestrictionAreaList);
    }

    public void UpdateArea(CardSO cardSelected, Participant opposite)
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


        if (cardSelected.CardDeployLocation == CardDeployLocation.Limited)
        {
            foreach (GameObject gameObject in AreaManager.Instance.ParticipantAreaDictionary[opposite])
            {
                gameObject.SetActive(true);
            }
        }
        else if (cardSelected.CardDeployLocation == CardDeployLocation.Any)
        {
            foreach (List<GameObject> gameObjectList in AreaManager.Instance.ParticipantAreaDictionary.Values)
            {
                foreach (GameObject gameObject in gameObjectList)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
