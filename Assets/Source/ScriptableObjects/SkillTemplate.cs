using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Player/Skills/New Skill")]
public class SkillTemplate : ScriptableObject
{

    public string skillName;

    public BaseSkill skillScript;

    public float damage;
    public float range;

    public SkillType skillType;
}
