/*SaveState.cs
 * Created By: Phillip Buckreis 10/4/17
 * 
 * This manages savestate.
 * 
 * Last modified: 11/20/17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance {set; get; }
    public SaveState state;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();
        InvokeRepeating("Save", 1.0f, 5.0f);

    }

    //save the state of this SaveState script to the player pref
    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    //load the state of a SaveState
    public void Load()
    {
        if(PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();

            for (int i = 0; i < 30; i++)
                state.savedItems[i].ID = -1;

            Save();
            Debug.Log("No save file found, creating a new one");
        }
    }

    //reset save file
    public void Reset()
    {
        PlayerPrefs.DeleteKey("save");
    }

}


