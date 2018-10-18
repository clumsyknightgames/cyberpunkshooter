////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : SkillTemplate.cs
//   Description :
//      Class used to create new skills for the game
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

    public string skillName;

    public BaseSkill skillScript;

    public float damage;
    public float range;

    public SkillType skillType;
}
