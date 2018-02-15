using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour {
    [Range(0, 100)]public float health = 100f;

    Animator animator;
    ZombieAI zombieAI;
    ZombieAudio zombieAudio;
    ZombieMovement zombieMovement;
    CharacterController characterController;
    private Rigidbody[] bodyParts;
    public Respawner thisRespwaner;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        zombieAI = GetComponent<ZombieAI>();
        zombieAudio = GetComponent<ZombieAudio>();
        bodyParts = GetComponentsInChildren<Rigidbody>();
        zombieMovement = GetComponent<ZombieMovement>();
        characterController = GetComponent<CharacterController>();

        EnableRagdoll(false);
	}
	
	// Update is called once per frame
	void Update () {
        health = Mathf.Clamp(health, 0, 100);
        
	}

    public void ApplyDamage(float number,int direction =0)
    {
        health -= number;
        if (health < 0)
        {
            health = 0;
            ZombieDie();
        }
        if (health > 100)
        {
            health = 100;
        }
    }

    void ZombieDie()
    {
        zombieAI.enabled = false;
        zombieMovement.enabled = false;
        characterController.enabled = false;//这个东西自带碰撞器，会和ragdoll产生互动，很怪异
        EnableRagdoll(true);
        animator.enabled = false;
        zombieAudio.PlayZombieDyingSound();
        Destroy(this.gameObject, 10f);
        if(thisRespwaner)
        {
            thisRespwaner.AmountOne();
        }
    }

    void EnableRagdoll(bool value)
    {
        for(int i = 0;i<bodyParts.Length;i++)
        {
            bodyParts[i].isKinematic = !value;
        }
    }
}
