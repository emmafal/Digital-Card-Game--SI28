using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class uifin : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image bg;
    private VisualElement root;
    private TextField storyBox;
    private TextField fullStoryBox;
    
    //fbeuibvdqibg
    // Start is called before the first frame update

    private void Restartgame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    private void togglevoir()
    {
        if (fullStoryBox.style.display == DisplayStyle.Flex)
        {
            fullStoryBox.style.display = DisplayStyle.None;
        }
        else
        {
            fullStoryBox.style.display = DisplayStyle.Flex;
            
        }
    }
    void OnEnable()
    {
        GameObject go = GameObject.Find("endDataObject");
        endData d = (endData)go.GetComponent(typeof(endData));
        Debug.Log(d);
        bg.sprite = d.background;

        root = GetComponent<UIDocument>().rootVisualElement;
        storyBox = root.Q<TextField>("storyBox");
        updateStoryBox(
            d.lastStory
            );
        storyBox.SetVerticalScrollerVisibility(ScrollerVisibility.Auto);
        fullStoryBox = root.Q<TextField>("fullStoryBox");
        updateFullStoryBox(d.story /*+ "\n\n" + d.lastStory*/);
        fullStoryBox.SetVerticalScrollerVisibility(ScrollerVisibility.Auto);

        //init boutons

        UnityEngine.UIElements.Button restart = new UnityEngine.UIElements.Button();
        UnityEngine.UIElements.Button voir = new UnityEngine.UIElements.Button();

        voir = root.Q<UnityEngine.UIElements.Button>("Voir");
        voir.clicked += () => togglevoir();

        restart = root.Q<UnityEngine.UIElements.Button>("Rejouer");
        restart.clicked += () => Restartgame();

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateStoryBox(string text)
    {
        storyBox.value = text;
    }

    public void updateFullStoryBox(string text)
    {
        fullStoryBox.value = text;
    }
}
