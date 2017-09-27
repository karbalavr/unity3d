using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public GameObject TextObject;
    public Transform Player;

	private bool  StartTimer = false;
	private float invoke = 2.0f;
	private float timer  = 0.0f;
	private int   Num = 0;


	public void btn_Restart ()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void btn_Quit()
	{
		Application.Quit();
	}

	public void Pointer_Enter (int n)
	{
        Num = n;
		StartTimer = true;
		timer = Time.time + invoke;
	}

	public void Pointer_Exit ()
	{
        Num = 0;
		StartTimer = false;
		//TextObject.SetActive(false);
	}

	public void Update ()
	{
		if (StartTimer) 
		{
			//if ( !TextObject.activeSelf )
			//	TextObject.SetActive(true);

			if (Time.time > timer) 
			{
				StartTimer = false;
				//TextObject.SetActive(false);

                if (Num == 10)
					btn_Quit();
                else
                if (Num == 12)
                    Player.SendMessage("ActiveJoystickMode", SendMessageOptions.DontRequireReceiver);
                else
                if (Num == 11)
                    Player.SendMessage("AutoScene", SendMessageOptions.DontRequireReceiver);
				else
                    Player.SendMessage("Scene", Num, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
    /*
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 50, 30), "0"))
            Player.SendMessage("Scene", 0, SendMessageOptions.DontRequireReceiver);

        if (GUI.Button(new Rect(55, 0, 50, 30), "1"))
            Player.SendMessage("Scene", 1, SendMessageOptions.DontRequireReceiver);

        if (GUI.Button(new Rect(110, 0, 50, 30), "2"))
            Player.SendMessage("Scene", 2, SendMessageOptions.DontRequireReceiver);

        if (GUI.Button(new Rect(165, 0, 50, 30), "3"))
            Player.SendMessage("Scene", 3, SendMessageOptions.DontRequireReceiver);

        if (GUI.Button(new Rect(220, 0, 50, 30), "4"))
            Player.SendMessage("Scene", 4, SendMessageOptions.DontRequireReceiver);

        if (GUI.Button(new Rect(285, 0, 50, 30), "Auto"))
            Player.SendMessage("AutoScene", SendMessageOptions.DontRequireReceiver);


        if (GUI.Button(new Rect(Screen.width - 50, 0, 50, 30), "Menu"))
            Player.SendMessage("ActiveMenuMode", SendMessageOptions.DontRequireReceiver);

        if (GUI.Button(new Rect(Screen.width - 110, 0, 50, 30), "Joystick"))
            Player.SendMessage("ActiveJoystickMode", SendMessageOptions.DontRequireReceiver);

    }
    */
}
