using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestKnob : MonoBehaviour 
{
	public bool GameStart;
	public GameObject LockTick;
	[SerializeField]private int Ticks;

	public GameObject Light1;
	public GameObject Light2;
	public GameObject Light3;
	public GameObject Light4;
	public GameObject Light5;
	public List<GameObject> Lights = new List<GameObject>();

	public Material Green;

	public Text CountDown;
	public Button Easy;
	public Button Medium;
	public Button Hard;

	public Toggle TimeSkill;
	public Toggle TurnSkill;

	public List<GameObject> LockTicks = new List<GameObject> ();
	private int Difficulty;
	public int[] LockCombo;
	private bool _isRotating;
	public float TurnRate;
	public float TurnThreshold;
	public float TempThreshold;
	public int CurrentLock;
	public float Direction;
	public int TimeLeft;
	private bool TimeActive;

	// Use this for initialization
	void Start ()
	{
		TimeActive = false;
		GameStart = false;
		Direction = -1;
		CurrentLock = 0;
		TurnThreshold = 0;
		TurnRate = 0.7f;

		for (int i = 0; i < Ticks; i++)
		{
			GameObject _LockTick = Instantiate (LockTick, LockTick.transform.position, Quaternion.identity) as GameObject;
			_LockTick.transform.RotateAround (gameObject.transform.position, Vector3.forward, -i * 360 / Ticks);
			LockTicks.Add (_LockTick);
		}

		Button button1 = Easy.GetComponent<Button> ();
		button1.onClick.AddListener (EasyClick);

		Button button2 = Medium.GetComponent<Button> ();
		button2.onClick.AddListener (MedClick);

		Button button3 = Hard.GetComponent<Button> ();
		button3.onClick.AddListener (HardClick);

		Toggle toggle1 = TimeSkill.GetComponent<Toggle> ();
		toggle1.onValueChanged.AddListener (delegate{SetDoubleTime();});

		Toggle toggle2 = TurnSkill.GetComponent<Toggle> ();
		toggle2.onValueChanged.AddListener (delegate{SetDoubleTurn();});

		Lights.Add (Light1);
		Lights.Add (Light2);
		Lights.Add (Light3);
		Lights.Add (Light4);
		Lights.Add (Light5);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameStart) 
		{
			if (Input.GetKey (KeyCode.RightArrow)) 
			{
				transform.RotateAround (transform.position, -Vector3.forward, TurnRate);
				TurnThreshold -= TurnRate;
				TempThreshold = TurnThreshold - 5.0f;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) 
			{
				transform.RotateAround (transform.position, Vector3.forward, TurnRate);
				TurnThreshold += TurnRate;
				TempThreshold = TurnThreshold + 5.0f;
			}

			CountDown.text = "Time left: " + TimeLeft;

			if (TimeLeft <= 0)
			{
				GameStart = false;
				CountDown.text = "You failed to open the safe.";
			}
		}

		if (Difficulty == 4) 
		{
			Lights [4].GetComponent<Renderer> ().enabled = false;
		}

		if (Difficulty == 3) 
		{
			Lights [4].GetComponent<Renderer> ().enabled = false;
			Lights [3].GetComponent<Renderer> ().enabled = false;
		}
	}

	void FixedUpdate()
	{
		if(GameStart)
		{
			Vector3 fwd = transform.TransformDirection (Vector3.forward);
			RaycastHit Hit;
			if (Physics.Raycast(transform.position, -fwd, out Hit))
			{
				foreach (GameObject Tick in LockTicks)
				{
					if (Hit.collider.gameObject == Tick) 
					{
						if ((Tick == LockTicks [LockCombo [CurrentLock]]))
						{
							Tick.GetComponent<LockTick> ().Unlock = true;
							Lights [CurrentLock].GetComponent<Renderer> ().material = Green;
							if (CurrentLock != LockCombo.Length - 1) 
							{
								CurrentLock++;
							} 
							else
							{
								GameStart = false;
								CountDown.text = "You've unlocked the safe!";
							}
						}
						if (CurrentLock > 0)
						{
							;
						}
						Tick.GetComponent<LockTick> ().MoveUp = true;
					} 
					else
					{
						Tick.GetComponent<LockTick> ().MoveUp = false;
					}
				}
			}
		}
	}

	public void EasyClick()
	{
		Difficulty = 3;
		GameStart = true;
		StartCoroutine (StartCountdown ());
		SetDifficulty ();
		Easy.interactable = false;
		Medium.interactable = false;
		Hard.interactable = false;
		TimeSkill.interactable = false;
		TurnSkill.interactable = false;
	}

	public void MedClick()
	{
		Difficulty = 4;
		GameStart = true;
		StartCoroutine (StartCountdown ());
		SetDifficulty ();
		Easy.interactable = false;
		Medium.interactable = false;
		Hard.interactable = false;
		TimeSkill.interactable = false;
		TurnSkill.interactable = false;
	}

	public void HardClick()
	{
		Difficulty = 5;
		GameStart = true;
		StartCoroutine (StartCountdown ());
		SetDifficulty ();
		Easy.interactable = false;
		Medium.interactable = false;
		Hard.interactable = false;
		TimeSkill.interactable = false;
		TurnSkill.interactable = false;
	}

	public IEnumerator StartCountdown(int value = 15)
	{
		TimeLeft = value;

		if (TimeActive) 
		{
			TimeLeft += 15;
		}

		while (TimeLeft > 0) 
		{
			yield return new WaitForSeconds (1.0f);
			TimeLeft--;
		}
	}

	void SetDifficulty()
	{
		LockCombo = new int[Difficulty];

		List <int> Numbers = new List<int> ();
		for (int i = 0; i < Ticks; i++) 
		{
			Numbers.Add (i);
		}

		for (int i = 0; i < LockCombo.Length; i++)
		{
			int ThisNumber = Random.Range (0, Numbers.Count);
			LockCombo [i] = Numbers [ThisNumber];
			Numbers.Remove (ThisNumber);
		}
	}

	void SetDoubleTime()
	{
		TimeActive = true;
	}

	void SetDoubleTurn()
	{
		TurnRate *= 2;
	}
}
