using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour {

    public Texture2D cursor;

	// Use this for initialization
	void Start () {
        Cursor.SetCursor(cursor, new Vector2 (30,50), CursorMode.ForceSoftware);
	}
}
