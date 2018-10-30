using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataCollector : MonoBehaviour {

    public static DataCollector Instance;

    string currentFilePath;

    [Tooltip("Selected user image filepath")]
    public string imagePath;
    [Tooltip("Selected hair index")]
    public int hairIndex;
    [Tooltip("Gender selected")]
    public GENDER gender;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void Submit()
    {
        SceneManager.LoadScene("MainScene");
    }
}
