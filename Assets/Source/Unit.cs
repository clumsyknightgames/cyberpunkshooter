////////////////////////////////////////////////////////////////////////////
//
//   Project     : Cyberpunk Shooter
//   File        : Unit.cs
//   Description :
//      Class for all living object in the game to derrive from
//
//   Created On: 17/10/2018
//   Created By: Matt Ward <mailto:wardm17@gmail.com>
////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : Entity
{
    public bool isDead { get; set; }

    private float movSpeed_;
    public float movSpeed
    {
        get { return movSpeed_; }
        set { movSpeed_ = value; }
    }

    private float sprintModifier_;
    public float sprintModifier
    {
        get { return sprintModifier_; }
        set { sprintModifier_ = value; }
    }

    private void Start ()
    {
        isDead = false;
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            health.regenTick();
            energy.regenTick();
        }
    }

    //// Methods that can be overridden for more functionality
    //public override bool damage(float value) { }
    //public override bool heal(float value) { }
    //public override bool interact() { }
}
