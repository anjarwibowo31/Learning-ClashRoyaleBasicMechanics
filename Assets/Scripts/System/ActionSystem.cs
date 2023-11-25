using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    public static ActionSystem Instance { get; private set; }

    private Participant ownSide = Participant.Player;
    private Participant opposite;
    private Camera mainCamera;

    [SerializeField] private Card cardSelected;
    [SerializeField] private LayerMask areaLayer;

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

        if (ownSide == Participant.Player)
        {
            opposite = Participant.Enemy;
        }
        else
        {
            opposite = Participant.Player;
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

        if (cardSelected == null)
        {

        }

        mainCamera = Camera.main;
    }

    private void Update()
    {
        DeployCard();
    }

    public void SetSelectedCard(Card cardSelected)
    {
        this.cardSelected = cardSelected;

        AreaManager.Instance.UpdateArea(cardSelected, opposite);
    }

    public void DeployCard()
    {
        if (GameplaySystem.Instance.GetCurrentState() == GameState.Battle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, areaLayer);

                if (hitInfo.transform.CompareTag("DropArea"))
                {
                    //DeployCard(Card selectedcard)
                    // TEST
                    GameObject g = ParticipantDataManager.Instance.ParticipantDictionary[Participant.Player].CardOwnedArray[0];
                    Instantiate(g, hitInfo.point, Quaternion.identity);

                }
            }
        }
    }

    public Card GetSelectedCard()
    {
        return cardSelected;
    }
}
