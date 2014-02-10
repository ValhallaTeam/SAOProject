using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour 
{
	// Player
	public int iPlayerIdleState;
	public int iPlayerMoveState;
	public int iPlayerMoveSpeed;

	public int iPlayerAttackState;
	public int iPlayerFire;
	public int iPlayerAttackCount;

	// Enemy
	public int iEnemyIdleState;
	public int iEnemyHurtState;
	public int iEnemyHurt;

	void Awake()
	{
		// Player
		iPlayerIdleState = Animator.StringToHash("Base Layer.test_human_idle");
		iPlayerMoveState = Animator.StringToHash("Base Layer.test_human_move");
		iPlayerMoveSpeed = Animator.StringToHash("Move Speed");

		iPlayerAttackState = Animator.StringToHash("PlayerAttackLayer.test_human_Atc02");
		iPlayerFire = Animator.StringToHash("Fire");
		iPlayerAttackCount = Animator.StringToHash("Attack Count");

		// Enemy
		iEnemyIdleState = Animator.StringToHash("base Layer.test_monster_idle");
		iEnemyHurtState = Animator.StringToHash("base Layer.test_monster_hurt");
		iEnemyHurt = Animator.StringToHash("Hurt");
	}

}
