using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	
	private int HP;
	private int count;
	private GameObject gameObject;

	Animator animator;

	void Start ()
	{
		HP = 100;
		animator = GetComponent<Animator>();
		gameObject = GameObject.Find("Player");
	}

	public void Update()
	{
		if(HP <= 0)
		{
			animator.SetInteger("EnemyState", 2);
			EffectPlayer.Instance.PlayEffect("EnergeBlast", this.transform.position, 1.0f);
			EffectPlayer.Instance.PlayEffect("EnergeBlast", this.transform.position, 1.0f);
			EffectPlayer.Instance.PlayEffect("EnergeBlast", this.transform.position, 1.0f);
		}

		count++;

		if(count % 200 == 0)
		{
			animator.SetInteger("EnemyState", 3);
			EffectPlayer.Instance.TrackingMagicEffect("ElekiBall1", this.transform.position, gameObject.transform.position);
		}

	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Effect")
        {
			EffectPlayer.Instance.PlayEffect("EnergeBlast", this.transform.position, 1.0f);
			HP -= 10;
        }
    }
}
