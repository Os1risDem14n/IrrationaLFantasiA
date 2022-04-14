using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
	public bool INFINITE_LOOP = true; //If true, then duration is ignored and goes on and on.
	public float duration = 1f; //Fixed duration of the vfx.
	public bool DESTROY_ON_END = true;
	public GameObject[] all_particles;

	void Update()
	{
		duration -= Time.deltaTime;
		if (duration<=0)
			STOP_VFX();
	}


	public void STOP_VFX()
	{
		//for (int i = 0; i <= all_particles.Length; i++)
		//	all_particles[i].GetComponent<ParticleSystem>().Stop();

		if (DESTROY_ON_END)
			Destroy(gameObject, 1.0f); //A little time before destroying, to let remaining particles to die first. It looks better.
	}


	public void PLAY_VFX()
	{
		for (int i = 0; i < all_particles.Length; i++)
			all_particles[i].GetComponent<ParticleSystem>().Play();
	}
}
