using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {


	private void OnTriggerEnter(Collider other)
	{
		if(other.name == "Enemy")
		{
			
			Destroy(other.gameObject);
			Destroy(this.gameObject);
		}
	}
}
