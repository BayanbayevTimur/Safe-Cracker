using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTick : MonoBehaviour {

	public AudioClip ClickLockedClip;
	public AudioSource ClickLockedSource;

	public AudioClip ClickUnlockedClip;
	public AudioSource ClickUnlockedSource;


	public Material Green;
	public Material Grey;
	public bool MoveUp = false;
	public bool Unlock;
	private bool SoundPlayed;
	private float Speed = 1.0f;
	private Vector3 StartingPosition;
	private Vector3 EndPosition;


	void Start () 
	{
		SoundPlayed = false;
		Unlock = false;
		StartingPosition = transform.position;
		EndPosition = StartingPosition + transform.up * 0.15f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (MoveUp)
		{
			transform.position = Vector3.Lerp (StartingPosition, EndPosition, Speed);
			if (!SoundPlayed && !Unlock) 
			{
				ClickLockedSource.clip = ClickLockedClip;
				ClickLockedSource.Play ();
				SoundPlayed = true;
			}
		}
		if(!MoveUp && !Unlock)
		{
			GetComponent<Renderer> ().material = Grey;
			transform.position = Vector3.Lerp (EndPosition, StartingPosition, Speed);
			SoundPlayed = false;
		}
		if (Unlock) 
		{
			GetComponent<Renderer> ().material = Green;
			if (!SoundPlayed) 
			{
				ClickUnlockedSource.clip = ClickUnlockedClip;
				ClickUnlockedSource.Play ();
				SoundPlayed = true;
			}
		}
	}
}
