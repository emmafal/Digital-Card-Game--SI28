using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.FilePathAttribute;


public class locationHandler : MonoBehaviour
{
    [SerializeField] private UI UI;
    [SerializeField] private Gm gm;
    [SerializeField] private se_reposer se_reposer;
    [SerializeField] private bureau bureau;
    [SerializeField] private point_bouffe point_bouffe;
    [SerializeField] private utc utc;
    [SerializeField] private espace_vert espace_vert;
    [SerializeField] private commissariat commissariat;
    [SerializeField] private chez_valou chez_valou;
    [SerializeField] private Image background_image;
    [SerializeField] private Image nextImage;
    [SerializeField] private List<string> ImageQueue = new List<string>();
    private string[] moments = { "Aube", "Matin", "Midi", "Après-midi", "Soir", "Nuit" };
    private string locStr = "se_reposer";
    private float nextImageIterator = 0f;


    private int[] cartesJouées = new int[100];


    private void OnEnable()
    {
        UpdateBackgroundImage(0);
    }

    private string UpdateBackgroundImage(int location)
    {
        String locationStr = "";
        switch (location)
        {
            case 0:
                locationStr = "se_reposer";
                break;
            case 1:
                locationStr = "bureau";
                break;
            case 2:
                locationStr = "point_bouffe";
                break;
            case 3:
                locationStr = "bf";
                break;
            case 4:
                locationStr = "espace_vert";
                break;
            case 5:
                locationStr = "police";
                break;
            case 6:
                locationStr = "chez_valou";
                break;
        }
        Sprite loadsprite;
        string s = "images/" + locationStr +"_"+ gm.getMoment() ;
        loadsprite = Resources.Load<Sprite>(s);
        //Debug.Log(s);
        if (loadsprite == null)
        {
            s = "images/" + locationStr;
        }
        ImageQueue.Add(s);
        //background_image.color = new Color(1, 1, 1, 1);
        return locationStr;
    }
    public void playCard(int location)
    {
        //Debug.Log("received  " + location);
        if(UI.queryCardCount(location) == 0) { return; }
        UI.decrCardCounter(location);
        UI.updateFullStoryBox();
        //Debug.Log("LOCATION HANDLER : Playing card " + location + "\n");
        string storyText = triggerLocation(location);
        UI.updateStoryBox(storyText);
        if (gm.getAdvancement() < 17)
        {
            UI.rushclock(gm.getAmountToAdvance());
        }
        int adv = gm.getAdvancement();
        cartesJouées[adv] = location;
        int currentMoment = gm.getMoment();
            //Debug.Log("AAAA"+ currentMoment);
        
        gm.endTurn();
        switch (gm.getDay()){
            case 1:
                UI.updateDay("Vendredi");
                break;
            case 2:
                UI.updateDay("Samedi");
                break;
            case 3:
                UI.updateDay("Dimanche");
                break;
        }
        //Debug.Log("BBBB"+ gm.getMoment());
        if (gm.getAdvancement() < 18)
        {
            UI.updateMoment(moments[gm.getMoment()]);
        }
        else
        {
            UI.updateMoment("The end");
        }
        transitionImage(locStr, currentMoment, gm.getMoment());

    }

    private void Update()
    {
        
        if (nextImageIterator > 0)
        {
            nextImageIterator -= Time.deltaTime;
            background_image.color = new Color(1, 1, 1, nextImageIterator); 
        }
        else if (ImageQueue.Count > 0)
        {

            background_image.sprite = nextImage.sprite;
            background_image.color = new Color(1, 1, 1, 1);
            nextImage.sprite = Resources.Load<Sprite>(ImageQueue[0]);
            nextImageIterator = 1f;
            ImageQueue.RemoveAt(0);

        }

        
    }
    private void transitionImage(string locationStr, int currentMoment, int targetMoment)
    {
        while (currentMoment != targetMoment)
        {
            currentMoment = (currentMoment + 1) % 6;
            //}

            string s = "images/" + locationStr + "_" + currentMoment;
            //test sprite
            Sprite loadsprite;
            loadsprite = Resources.Load<Sprite>(s);
            //Debug.Log(s);
            if (loadsprite == null)
            {
                s = "images/" + locationStr;
            }

            ImageQueue.Add(s);
        }
        /*
        string debug = "";
        for (int i = 0; i < ImageQueue.Count; i++)
        {
            debug += "\n"+ImageQueue[i];
            
        }
        Debug.Log(debug);
        */

        /*
        currentMoment = nextMoment;
        Debug.Log("next moment : " + nextMoment);
        Sprite loadsprite;
        string s = "images/" + locationStr + "_" + nextMoment;
        loadsprite = Resources.Load<Sprite>(s);
        Debug.Log(s);
        if (loadsprite == null)
        {
            loadsprite = Resources.Load<Sprite>("images/" + locationStr);
        }
        nextImage.sprite = loadsprite;
        nextImageIterator = 1f;
        */


    }
    //need to add turn handling here

    /// <summary>
    /// generates the story fragment of selected location.
    /// </summary>
    /// <param name="location">target location</param>
    /// <returns>string</returns>
    public string triggerLocation(int location)
    {
        string storyText = "Lorem ipsum";
        try
        {
            //Debug.Log("LOCATION HANDLER : triggering location " + location +"\n");
            locStr = UpdateBackgroundImage(location);
            switch (location)
            {
                case 0:
                    storyText = se_reposer.trigger();
                    break;
                case 1:
                    storyText = bureau.trigger();
                    break;
                case 2:
                    storyText = point_bouffe.trigger();
                    break;
                case 3:
                    storyText = utc.trigger();
                    break;
                case 4:
                    storyText = espace_vert.trigger();
                    break;
                case 5:
                    storyText = commissariat.trigger();
                    break;
                case 6:
                    storyText = chez_valou.trigger();
                    break;
            }
        }
        catch
        {
            //Debug.Log("LOCATION HANDLER : error triggering location " + location + "\n--> did not advance");
            storyText = "error at location "+location;
            gm.setAmountToAdvance(0);
        }
        return storyText.Trim();
    }

    public static string removeAllWhiteSpaces(string str)
    {
        return Regex.Replace(str, @"\s+", String.Empty);
    }


    //retourne le lieu (int) joué au temps i
    public int getDerniereCarte(int i){
        try {
            return cartesJouées[i];
        }
        catch(IndexOutOfRangeException) {
            // Retourner une valeur par défaut ou gérer l'erreur d'une autre manière
            return -1;
        }
    }
}

