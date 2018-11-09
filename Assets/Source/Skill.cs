////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : Skill.cs
//   Description :
//      Base skill class
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections.Generic;

public class Skill : MonoBehaviour
{
}
public enum SkillType
{
    MELEE,
    RANGED,
    AOE,
    DEFENSE,
    BUFF,
    CREATEOBJECT
}
