using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MenuTypes : byte {
MainMenu = 0,
OptionMenu =1,
PauseMenu =2,
GameOverMenu =3,
	
}

public class GameManagerScript : MonoBehaviour
{

	public bool isMenuActive { get; set; }
	public AudioClip ClickSound;
	private AudioSource m_SoundSource;
	public MenuTypes ActiveMenu { get; set; }
	private readonly GUI.WindowFunction[] MenuFunctions = null;

	private Settings m_Settings = new Settings();

	private readonly string[] MenuNames = new string[]
	{
		"Main Menu",
		"Options Menu",
		"Pause Menu",
		"Game Over Menu",
	};

	public GameManagerScript()
	{
		MenuFunctions = new GUI.WindowFunction[]
		{
			MainMenu,
			OptionsMenu,
			PauseMenu,
			GameOverMenu,
		};
	}





void Awake()
	{
		ActiveMenu = MenuTypes.MainMenu;
		isMenuActive = false
			;
		Application.runInBackground = true;
		DontDestroyOnLoad((gameObject)); 
		m_SoundSource = Camera.main.transform.FindChild("Sound").GetComponent<AudioSource>();
		m_Settings.Load(Camera.main.transform.FindChild("Music").GetComponent<AudioSource>(),m_SoundSource);
			


	}

	void Start () {
		
	}
	

	void Update () {
		
	}

	private void OnGUI()
	{
		const int Width = 400;
		const int Height = 300;
		
		if (isMenuActive)
		{ 
			Rect windowRect = new Rect((Screen.width - Width) / 2,
				(Screen.height - Height) / 2,
					Width,Height);
			GUILayout.Window
				(0, windowRect, MenuFunctions[(byte)ActiveMenu], MenuNames[(byte)ActiveMenu]);
		}
	}

	private void MainMenu(int id)
	{
		if (GUILayout.Button("Start Game"))
		{
			m_SoundSource.PlayOneShot(ClickSound);
			isMenuActive = false;
		}
		
		if(GUILayout.Button("Options"))
		{
			m_SoundSource.PlayOneShot(ClickSound);
			//m_SourceMenu = MenuType.MainMenu;
			ActiveMenu = MenuTypes.OptionMenu;
		}

		if (Application.isEditor)
		{
			if (GUILayout.Button("Exit"))
			{
				m_SoundSource.PlayOneShot(ClickSound);
				Application.Quit();
			}
		}
	}

	private void OptionsMenu(int id)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label("Music Volume: ", GUILayout.Width(90));
		m_Settings.MusicVolume = GUILayout.HorizontalSlider(m_Settings.MusicVolume, 0.0f, 1.0f);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Sound Volume: ", GUILayout.Width(90));
		m_Settings.SoundVolume = GUILayout.HorizontalSlider(m_Settings.SoundVolume, 0.0f, 1.0f);
		GUILayout.EndHorizontal();

		if (GUILayout.Button("Reset High Score"))
		{
			m_SoundSource.PlayOneShot(ClickSound);
			m_Settings.HighScore = 0;
		}

		if (GUILayout.Button("Back"))
		{
			m_SoundSource.PlayOneShot(ClickSound);
			m_Settings.Save();
			ActiveMenu = MenuTypes.MainMenu;
		}


}

	private void PauseMenu(int id)
	{
	}

	private void GameOverMenu(int id)
	{
	}
}
