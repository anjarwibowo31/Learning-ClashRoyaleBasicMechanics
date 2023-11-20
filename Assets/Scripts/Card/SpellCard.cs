using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class SpellCard : Card
{
    public SpellBehaviourType SpellBehaviourType { get => spellBehaviourType; set => spellBehaviourType = value; }
    public GroundAttackData GroundAttackData { get => groundAttackData; set => groundAttackData = value; }
    public SplashAreaAttackData SplashAreaAttackData { get => splashAreaAttackData; set => splashAreaAttackData = value; }
    public TowerTargetingAttackData TowerTargetingAttackData { get => towerTargetingAttackData; set => towerTargetingAttackData = value; }
    public override CardType CardType { get => cardType; }
    public override string CardName { get => cardName; }

    [SerializeField] private string cardName;
    [SerializeField] private string manaCost;

    [SerializeField] private SpellBehaviourType spellBehaviourType;
    [SerializeField] private GroundAttackData groundAttackData;
    [SerializeField] private SplashAreaAttackData splashAreaAttackData;
    [SerializeField] private TowerTargetingAttackData towerTargetingAttackData;

    public const CardType cardType = CardType.Spell;
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
            case SpellBehaviourType.TowerTargetingAttack:
                spellBehaviour = gameObject.AddComponent<TowerTargetingAttackSpell>();
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
    [SerializeField] private int range;
    public int Range { get { return range; } set { range = value; } }
}

public class GroundAttackSpell : SpellBehaviour
{
    GroundAttackData spellData;

    private Vector3 endPos;

    private void Start()
    {
        spellData = GetComponent<SpellCard>().GroundAttackData;
        OnSpawn(transform.position);
    }

    public override void OnSpawn(Vector3 spawnPoint)
    {
        transform.position = spawnPoint;

        endPos = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z + spellData.Range);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * 4.0f);
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

[Serializable]
public class TowerTargetingAttackData
{

}

public class TowerTargetingAttackSpell : SpellBehaviour
{
    private void Start()
    {
        TowerTargetingAttackData data = GetComponent<SpellCard>().TowerTargetingAttackData;
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
    SerializedProperty spellBehaviourTypeProp;
    SerializedProperty groundAttackDataProp;
    SerializedProperty splashAreaAttackDataProp;
    SerializedProperty towerTargetingAttackDataProp;

    void OnEnable()
    {
        cardNameProp = serializedObject.FindProperty("cardName");
        manaCostProp = serializedObject.FindProperty("manaCost");
        spellBehaviourTypeProp = serializedObject.FindProperty("spellBehaviourType");
        groundAttackDataProp = serializedObject.FindProperty("groundAttackData");
        splashAreaAttackDataProp = serializedObject.FindProperty("splashAreaAttackData");
        towerTargetingAttackDataProp = serializedObject.FindProperty("towerTargetingAttackData");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(cardNameProp);
        EditorGUILayout.PropertyField(manaCostProp);
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
            case SpellBehaviourType.TowerTargetingAttack:
                EditorGUILayout.PropertyField(towerTargetingAttackDataProp);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}