using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPointArray;
    [SerializeField] private CardSO[] cardSOArray;
    [SerializeField] private float spawnTime;

    private float initialSpawnTime;

    private void Start()
    {
        initialSpawnTime = spawnTime;
    }

    void Update()
    {
        if (GameplaySystem.Instance.GameState == GameState.Battle)
        {
            spawnTime -= Time.deltaTime;

            if (spawnTime <= 0)
            {
                SpawnCard();
                spawnTime = initialSpawnTime;
            }
        }
    }

    private void SpawnCard()
    {
        int randomIndex = Random.Range(0, spawnPointArray.Length);
        Transform spawnPoint = spawnPointArray[randomIndex];

        randomIndex = Random.Range(0, cardSOArray.Length);
        CardSO card = cardSOArray[randomIndex];

        GameObject gameObject = ParticipantDataManager.Instance.ParticipantDictionary[Participant.Enemy].SpawnObjectDictionary[card];
        GameObject spawnObject = Instantiate(gameObject, spawnPoint.position, Quaternion.Euler(ParticipantDataManager.Instance.ParticipantDictionary[Participant.Enemy].PartyDirection));
        spawnObject.SetActive(true);
    }
}
