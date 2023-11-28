using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionSystem : MonoBehaviour
{
    public static PlayerActionSystem Instance { get; private set; }

    public float ElixirAmount { get; set; }

    public const float maxElixirAmount = 10;

    private Participant participant = Participant.Player;
    private Participant oppositeParticipant;
    private Camera mainCamera;

    [SerializeField] private CardSO cardSelected;
    [SerializeField] private LayerMask areaLayer;
    [SerializeField] private float startingElixir;

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

        if (participant == Participant.Player)
        {
            oppositeParticipant = Participant.Enemy;
        }
        else
        {
            oppositeParticipant = Participant.Player;
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

        mainCamera = Camera.main;

        ElixirAmount = startingElixir;
    }

    private void Update()
    {
        if (GameplaySystem.Instance.GameState == GameState.Battle)
        {
            DeployCard();
            ElixirManager();
        }
    }

    private void ElixirManager()
    {
        if (ElixirAmount < maxElixirAmount)
        {
            ElixirAmount += Time.deltaTime;
        }
        else
        {
            ElixirAmount = maxElixirAmount;
        }
    }

    public void SetSelectedCard(CardSO cardSelected)
    {
        this.cardSelected = cardSelected;

        AreaManager.Instance.UpdateArea(cardSelected, oppositeParticipant);
    }

    public void DeployCard()
    {
        if (GameplaySystem.Instance.GetCurrentState() == GameState.Battle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, areaLayer);

                if (hitInfo.transform.CompareTag("DropArea") && cardSelected != null)
                {
                    if (ElixirAmount > cardSelected.ElixirCost)
                    {
                        ElixirAmount -= cardSelected.ElixirCost;
                        Instantiate(cardSelected.CardObject, hitInfo.point, Quaternion.identity);
                    }
                }
            }
        }
    }

    public CardSO GetSelectedCard()
    {
        return cardSelected;
    }
}
