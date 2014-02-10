using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	private Animator kAnim;
	private HashIDs kHash;
	private PlayerMovement kPlayer;
	private bool bIsHurt;
	private int iCurrentHurt;

	void Awake()
	{
		kAnim = GetComponent<Animator>();
		kHash = GameObject.FindGameObjectWithTag(Tags.strGameController).GetComponent<HashIDs>();
		kPlayer = GameObject.FindGameObjectWithTag(Tags.strPlayer).GetComponent<PlayerMovement>();
		bIsHurt = false;
		iCurrentHurt = 0;
	}

	void OnTriggerStay( Collider kCollider )
	{
		if (kCollider.gameObject.tag == "Weapon")
		{
			if(kPlayer.IsFire())
			{
				if(bIsHurt == false)
				{
					iCurrentHurt = kPlayer.GetAttackCount();
					kAnim.SetBool (kHash.iEnemyHurt, true);
				}else if(iCurrentHurt != kPlayer.GetAttackCount())
				{
					iCurrentHurt = kPlayer.GetAttackCount();
					kAnim.Play("test_monster_hurt", 0, 0.0f);
				}
			}
		}
	}

	void Hurt(int iIsHurt)
	{
		bIsHurt = System.Convert.ToBoolean(iIsHurt);
		if (!bIsHurt)
		{
			kAnim.SetBool (kHash.iEnemyHurt, bIsHurt);
		}
	}
}

