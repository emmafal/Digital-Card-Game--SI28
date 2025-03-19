using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endData : MonoBehaviour
{
    public Sprite background;
    public string story;
    public string lastStory;
    public static endData Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //SceneManager.LoadSceneAsync("fin");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
