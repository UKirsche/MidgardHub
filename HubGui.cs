using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HubGui : MonoBehaviour
{

	private class MenuSelector
	{
		public bool main = false;
		public bool scenes = false;
		public bool dice = false;
		public bool generate = false;

		public void setMenusFalse ()
		{
			this.main = false;
			this.scenes = false;
			this.dice = false;
			this.generate = false;
		}
	}

	private struct MenuBtn
	{
		public string Text;
		public string Link;
	}

	private enum MenuType
	{
		Main,
		Scenes,
		Dice,
		Generator
	}

	public GUISkin Skin;

	private Vector2 scrollPos = new Vector2 ();

	private string buttonTextMain = "Howto";
	private string buttonTextSzenen = "Abenteuer";
	private string buttonTextWuerfelDeck = "Würfeldeck";
	private string buttonTextGenerator = "Generator";



	private string mainDescription = "<color=black>MIDGARD</color>\n\n" +
									 "Das Hauptmenü erlaubt v.a. das Auswählen von Szenerien sowie das Öffnen des Würfeldecks.\n" +
									 "Aus den Szenerien wählen die Spieler die vom Meister angegebene Szene.\n" +
									 "Im Würfeldeck können die Teilnehmer ihren gewünschten Würfel auswählen und für alle sichtbar würfeln.\n\n\n" +
	                                 "<color=black>Abenteuer</color>\t Lädt das Abenteuer, das erkundet werden kann, evtl auch eine Zusammenfassung\n" +
	                                 "<color=black>Würfeldeck</color>\t Lädt den gemeinesamen Würfelbecher\n" +
	                                 "<color=black>Generator</color>\t Erlaubt das Erstellen eines Midgard Characters. Fürs Spiel kann Avatar gewählt werden\n";

	private string sceneDescription = "Wähle das Abenteuer";
	private string diceDescription = "Das Würfeldeck erlaubt für alle sichtbares Würfeln";


	private MenuSelector menuSelection = new MenuSelector();

	private MenuBtn scene1Button;
	private MenuBtn scene2Button;
	private MenuBtn wuerfelButton;
	private MenuBtn charGenButton;
	private MenuBtn charChoserButton;


	GUIStyle m_Headline;

	void Start ()
	{
		if (PhotonNetwork.connected || PhotonNetwork.connecting) {
			PhotonNetwork.Disconnect ();
		}

		scene1Button = new MenuBtn () { 
			Text = "<color=black>Mord im Kloster</color>", 
			Link = "MidgardGame-Scene" 
		};

		scene2Button = new MenuBtn () { 
			Text = "<color=black>Was bisher geschah...</color>", 
			Link = "MidgardBisher" 
		};

		wuerfelButton = new MenuBtn () { 
			Text = "<color=black>Zum Würfeldeck</color>", 
			Link = "MidgardWuerfel" 
		};

		charGenButton = new MenuBtn () { 
			Text = "<color=black>Zum Charaktergenerator</color>", 
			Link = "MidgardUICharGen" 
		};

		charChoserButton = new MenuBtn () { 
			Text = "<color=black>Avatar wählen</color>", 
			Link = "CharacterChoser" 
		};

		m_Headline = new GUIStyle (this.Skin.label);
		m_Headline.padding = new RectOffset (3, 0, 0, 0);
	}

	void OnGUI ()
	{

		GUI.skin = this.Skin;

		GUILayout.BeginHorizontal ();
		GUILayout.Space (10);
		scrollPos = GUILayout.BeginScrollView (scrollPos, GUILayout.Width (180));
		GUILayout.Space (30);
		#region linkes Menü
		if (GUILayout.Button (buttonTextMain)) {
			choseMenu (MenuType.Main);
		}

		if (GUILayout.Button (buttonTextSzenen)) {
			choseMenu (MenuType.Scenes);
		}

		if (GUILayout.Button (buttonTextWuerfelDeck)) {
			choseMenu (MenuType.Dice);
		}

		if (GUILayout.Button (buttonTextGenerator)) {
			choseMenu (MenuType.Generator);
		}

		GUILayout.EndScrollView ();
		#endregion


		GUILayout.Space (30);

		#region rechtes Menüs
		GUILayout.BeginVertical (GUILayout.Width (Screen.width-300));
		GUILayout.Space (30);

		fillRightMenu ();
			
		GUILayout.EndVertical ();
		#endregion

		GUILayout.EndHorizontal ();
	}


	/// <summary>
	/// Fills the right menu.
	/// </summary>
	void fillRightMenu ()
	{
		//Zeige  nur gewählte Elemente in der rechten Gruppe
		if (this.menuSelection.main == true) {
			GUILayout.Label (mainDescription);
		} else if (this.menuSelection.scenes == true) {
			GUILayout.Label (sceneDescription);
			if (GUILayout.Button (this.scene1Button.Text)) {
				SceneManager.LoadScene (this.scene1Button.Link);
			}
			if (GUILayout.Button (this.scene2Button.Text)) {
				SceneManager.LoadScene (this.scene2Button.Link);
			}
		} else if (this.menuSelection.dice == true) { 
				GUILayout.Label (diceDescription);
				if (GUILayout.Button (this.wuerfelButton.Text)) {
					SceneManager.LoadScene (this.wuerfelButton.Link);
				}
		
		} else if(this.menuSelection.generate==true){
			if (GUILayout.Button (this.charGenButton.Text)) {
				SceneManager.LoadScene (this.charGenButton.Link);
			}
			if (GUILayout.Button (this.charChoserButton.Text)) {
				SceneManager.LoadScene (this.charChoserButton.Link);
			}
		}
				
	}

	/// <summary>
	/// Choses the menu for the right side
	/// </summary>
	/// <param name="mType">M type.</param>
	private void choseMenu (MenuType mType)
	{
		this.menuSelection.setMenusFalse ();
		switch (mType) {
		case MenuType.Main: 
			this.menuSelection.main = true;
			break;
		case MenuType.Scenes: 
			this.menuSelection.scenes = true;
			break;
		case MenuType.Dice: 
			this.menuSelection.dice = true;
			break;
		case MenuType.Generator: 
			this.menuSelection.generate = true;
			break;
		default:
			break;
		}
	}
}