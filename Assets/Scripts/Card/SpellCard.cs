using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SpellCard : Card
{
    // Harus ada tag opposite dan ownSide untuk mencegah ally damage
    public SpellBehaviourType SpellBehaviourType { get => spellBehaviourType; set => spellBehaviourType = value; }
    public GroundAttackData GroundAttackData { get => groundAttackData; set => groundAttackData = value; }
    public SplashAreaAttackData SplashAreaAttackData { get => splashAreaAttackData; set => splashAreaAttackData = value; }
    public override CardType CardType { get => cardType; }
    public override string CardName { get => cardName; }
    public override int ManaCost { get => manaCost; }
    public override CardDeployLocation CardDeployLocation => cardDeployLocation;

    [SerializeField] private string cardName;
    [SerializeField] private int manaCost;

    [SerializeField] private CardDeployLocation cardDeployLocation;
    [SerializeField] private SpellBehaviourType spellBehaviourType;
    [SerializeField] private GroundAttackData groundAttackData;
    [SerializeField] private SplashAreaAttackData splashAreaAttackData;

    private const CardType cardType = CardType.Spell;
    private SpellBehaviour spellBehaviour;

    private void Awake()
    {
        switch (spellBehaviourType)
        {
            case SpellBehaviourType.GroundAttack:
                spellBehaviour = gameObject.AddComponent<GroundAttackSpell>();
                break;
            case SpellBehaviourType.SplashAreaAttack:
                spellBehaviour = gameObject.AddComponent<SplashAreaAttackSpell>();
                break;
        }
    }
}

public abstract class SpellBehaviour : MonoBehaviour
{
    public abstract void OnSpawn(Vector3 spawnPoint);
}

[Serializable]
public class GroundAttackData
{
    public int Range { get => range; set => range = value; }
    public float Damage { get => damage;}

    [SerializeField] private int range;
    [SerializeField] private float damage;
}

public class GroundAttackSpell : SpellBehaviour
{
    GroundAttackData spellData;

    private Vector3 endPos;
    private Collider spellCollider;

    float destroyTime = 0.5f;

    private void Start()
    {
        spellData = GetComponent<SpellCard>().GroundAttackData;
        OnSpawn(transform.position);
        spellCollider = GetComponent<Collider>();
    }

    public override void OnSpawn(Vector3 spawnPoint)
    {
        transform.position = spawnPoint;

        endPos = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z + spellData.Range);
    }

    private void Update()
    {

        transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * 1.0f);

        if (Vector3.Distance(transform.position, endPos) <= 0.1)
        {
            destroyTime -= Time.deltaTime;
            if (destroyTime < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<IDamageable>(out IDamageable damagable);
        damagable.GetDamage(spellData.Damage);
    }
}

[Serializable]
public class SplashAreaAttackData
{

}

public class SplashAreaAttackSpell : SpellBehaviour
{
    private void Start()
    {
        SplashAreaAttackData data = GetComponent<SpellCard>().SplashAreaAttackData;
    }

    public override void OnSpawn(Vector3 spawnPoint)
    {

    }
}

[CustomEditor(typeof(SpellCard))]
public class SpellEditor : Editor
{
    SerializedProperty cardNameProp;
    SerializedProperty manaCostProp;
    SerializedProperty cardDeployLocationProp;
    SerializedProperty splashAreaAttackDataProp;
    SerializedProperty spellBehaviourTypeProp;
    SerializedProperty groundAttackDataProp;

    void OnEnable()
    {
        cardNameProp = serializedObject.FindProperty("cardName");
        manaCostProp = serializedObject.FindProperty("manaCost");
        cardDeployLocationProp = serializedObject.FindProperty("cardDeployLocation");
        spellBehaviourTypeProp = serializedObject.FindProperty("spellBehaviourType");
        groundAttackDataProp = serializedObject.FindProperty("groundAttackData");
        splashAreaAttackDataProp = serializedObject.FindProperty("splashAreaAttackData");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(cardNameProp);
        EditorGUILayout.PropertyField(manaCostProp);
        EditorGUILayout.PropertyField(cardDeployLocationProp);
        EditorGUILayout.PropertyField(spellBehaviourTypeProp);

        SpellBehaviourType selectedType = (SpellBehaviourType)spellBehaviourTypeProp.enumValueIndex;

        switch (selectedType)
        {
            case SpellBehaviourType.GroundAttack:
                EditorGUILayout.PropertyField(groundAttackDataProp);
                break;
            case SpellBehaviourType.SplashAreaAttack:
                EditorGUILayout.PropertyField(splashAreaAttackDataProp);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}