using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    public static ActionSystem Instance { get; private set; }

    public float ElixirAmount { get; set; }

    public const float maxElixirAmount = 10;

    [SerializeField] private Participant participant;
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
    }

    private void Start()
    {
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
        // FOR TEST, should be in awake
        if (participant == Participant.Player)
        {
            oppositeParticipant = Participant.Enemy;
        }
        else
        {
            oppositeParticipant = Participant.Player;
        }

        this.cardSelected = cardSelected;

        AreaManager.Instance.UpdateArea(cardSelected, participant, oppositeParticipant);
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

                        GameObject gameObject = ParticipantDataManager.Instance.ParticipantDictionary[participant].SpawnObjectDictionary[cardSelected];

                        GameObject spawnObject = Instantiate(gameObject, hitInfo.point, Quaternion.Euler(ParticipantDataManager.Instance.ParticipantDictionary[participant].PartyDirection));

                        spawnObject.SetActive(true);
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
