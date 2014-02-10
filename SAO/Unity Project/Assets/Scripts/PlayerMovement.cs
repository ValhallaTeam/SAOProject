using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float fTurnSmoothing = 15f;   // A smoothing value for turning the player.
	public float fDampTime = 0.1f;
	private Animator kAnim;
	private HashIDs kHash;
	private bool bIsFire;
	private int iAttackCount;
	private int iComboNext;

	void Awake()
	{
		kAnim = GetComponent<Animator>();
		kHash = GameObject.FindGameObjectWithTag(Tags.strGameController).GetComponent<HashIDs>();
		iAttackCount = 0;
		iComboNext = 0;
	}

	void FixedUpdate()
	{
		float fH = Input.GetAxis("Horizontal");
		float fV = Input.GetAxis("Vertical");
		bool bFire = Input.GetButton("Fire1");

		MovementManagement(fH, fV);
		AttackManagement(bFire);
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

	void AttackManagement( bool bFire )
	{
		if(bFire)
		{
			if(iComboNext == 0)
			{
				iComboNext = 1;
			}

			kAnim.SetInteger(kHash.iPlayerAttackCount, iComboNext);
		}
	}

	void Attack1( int iIsAttack)
	{
		bIsFire = System.Convert.ToBoolean(iIsAttack);
		if(bIsFire)
		{
			iAttackCount = 1;
		}else
		{
			iComboNext = 0;
			kAnim.SetInteger(kHash.iPlayerAttackCount, iComboNext);
		}
	}

	void Attack2( int iIsAttack )
	{
		bIsFire = System.Convert.ToBoolean(iIsAttack);
		if(bIsFire)
		{
			iAttackCount = 2;
		}else
		{
			iComboNext = 0;
			kAnim.SetInteger(kHash.iPlayerAttackCount, iComboNext);
		}
	}

	void Attack3( int iIsAttack )
	{
		bIsFire = System.Convert.ToBoolean(iIsAttack);
		if(bIsFire)
		{
			iAttackCount = 3;
		} else
		{
			iComboNext = 0;
			kAnim.SetInteger(kHash.iPlayerAttackCount, iComboNext);
		}

	}

	void CanComboNext( int iNext )
	{
		iComboNext = iNext;
		if(iComboNext == 0)
		{
			// I should into combo idle
		}
	}

	public bool IsFire()
	{
		return bIsFire;
	}

	public int GetAttackCount()
	{
		return iAttackCount;
	}
}
