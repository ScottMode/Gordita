using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingManager : Singleton<LoadingManager> 
{
	protected LoadingManager(){}
	
	void Awake () 
	{
        DontDestroyOnLoad(this);

        //Move through and load each scene
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; ++i)
        {
            SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
        }
    }
}