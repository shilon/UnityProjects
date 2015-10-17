using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {

		public void NextLevelButton(int index)
		{
			Application.LoadLevel(index);
		}
	}
	