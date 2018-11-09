////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : SkillTemplate.cs
//   Description :
//      Scriptable object to create new skills in-editor
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Player/Skills/New Skill")]
public class SkillTemplate : ScriptableObject
{
    [Space(10)]
    [Header("Skill Information")]
    public string skillName;
    public float damage;
    public SkillType skillType;

    [Space(10)]
    [Header("Skill Data")]
    public float range;
    public int skillCost;

    public Skill skillLogic;
}
