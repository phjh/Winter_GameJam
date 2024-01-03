using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GriffinOrb : PoolableMono
{
	public KeyBase OrbPos { get; set; }
	private GriffinPattern PatternParents;
	private ParticleSystem attackParticle;

	private void Awake()
	{
		PatternParents = FindAnyObjectByType<GriffinPattern>().GetComponent<GriffinPattern>();
		if(PatternParents == null) Debug.LogError("그리핀이 현재 존재하지 않습니다!");
	}

	public override void Init()
	{
		if(PatternParents == null) PatternParents = FindAnyObjectByType<GriffinPattern>().GetComponent<GriffinPattern>();
		OrbPos = null;
		PatternParents.griffinOrbs.Add(this);
	}

	

	public void SetPosition(KeyBase KeyPos)
	{
		OrbPos = KeyPos;
		KeyManager.Instance.DeleteConnectkeys(OrbPos);
		this.transform.position = OrbPos.transform.position;

		attackParticle.Play();
	}	
}
