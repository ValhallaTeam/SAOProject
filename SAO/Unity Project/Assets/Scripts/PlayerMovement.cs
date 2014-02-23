using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float fTurnSmoothing = 15f;   // A smoothing value for turning the player.
	public float fDampTime = 0.1f;
	private Animator kAnim;
	private HashIDs kHash;
	private bool bIsHurtAttack;
	private int iAttackCount;
	private bool bIsComboNext;
	private bool bCanComboNext;

	void Awake()
	{
		kAnim = GetComponent<Animator>();
		kHash = GameObject.FindGameObjectWithTag(Tags.strGameController).GetComponent<HashIDs>();
		iAttackCount = 0;
		bIsComboNext = false;
		bCanComboNext = false;
	}

	void FixedUpdate()
	{
		float fH = Input.GetAxis("Horizontal");
		float fV = Input.GetAxis("Vertical");
		bool bIsAttack = Input.GetButtonDown("Fire1");

		MovementManagement(fH, fV);
		AttackManagement(bIsAttack);
	}

	void MovementManagement(float fH, float fV)
	{
		// If there is some axis input...
		if (fH != 0f || fV != 0f)
		{
			// ... set the players rotation and set the speed parameter to 5.5f.
			Rotating(fH, fV);
			kAnim.SetFloat (kHash.iPlayerMoveSpeed, 5.5f, fDampTime, Time.deltaTime);
		} else
		{
			// Otherwise set the speed parameter to 0.
			kAnim.SetFloat (kHash.iPlayerMoveSpeed, 0.0f);
		}
	}

	void Rotating(float fH, float fV)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 vTargetDir = new Vector3(fH, 0f, fV);

		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion vTargetRotation = Quaternion.LookRotation(vTargetDir, Vector3.up);

		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion vNewRotation = Quaternion.Lerp(rigidbody.rotation, vTargetRotation, fTurnSmoothing * Time.deltaTime);

		// Change the players rotation to this new rotation.
		rigidbody.MoveRotation(vNewRotation);
	}

	void AttackManagement( bool bIsAttack )
	{
		if(bIsAttack)
		{
			if(iAttackCount >= 1)
			{
				if( bCanComboNext )
				{
					bIsComboNext = true;
					bCanComboNext = false;
					++ iAttackCount;
				}
			}else
			{	
				++ iAttackCount;
				kAnim.SetInteger(kHash.iPlayerAttackCount, iAttackCount);
			}

		}
	}

	void AttackStart( int iIsAttack)
	{
		bIsHurtAttack = true;
	}

	void AttackEnd( int iIsAttack )
	{
		bIsHurtAttack = false;
		if (bIsComboNext)
		{
			if(iAttackCount > 2)
			{
				int iTest = 0;
			}
			kAnim.SetInteger (kHash.iPlayerAttackCount, iAttackCount);
		} else 
		{
			if(iAttackCount > 1)
			{
				int iTest = 0;
			}

			iAttackCount = 0;
			kAnim.SetInteger (kHash.iPlayerAttackCount, iAttackCount);
		}
		bIsHurtAttack = false;
		bIsComboNext = false;
		bCanComboNext = false;
	}

	void CanComboNext( int iCanCombo )
	{
		bCanComboNext = true;
	}

	public bool GetIsHurtAttack()
	{
		return bIsHurtAttack;
	}

	public int GetAttackCount()
	{
		return iAttackCount;
	}
}
