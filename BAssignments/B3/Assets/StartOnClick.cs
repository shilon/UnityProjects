using UnityEngine;
using System.Collections;

public class StartOnClick : MonoBehaviour {

	public void LoadScene (int level)
	{
		Application.LoadLevel (level);
	}
}
