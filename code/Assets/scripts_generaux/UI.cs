using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    [SerializeField] private locationHandler locationHandler;
    [SerializeField] private Gm gm;
    [SerializeField] private float clockFlickerSpeed= 1.2f;
    [SerializeField] private float clockSpeed = 1f;
    [SerializeField] private float clockRushSpeed = 0.01f;
    private float clockFlickerElapsed = 0f;
    private float clockElapsed = 0f;
    private bool clockFlicker = true;

    private VisualElement root;
    private TextField storyBox;
    private TextField fullStoryBox;
    private Label dayLabel;
    private Label momentLabel;
    private Label clockLabel;
    private int clockhours = 4;
    private int clockminutes = 0;
    private int clockMax = 8;
    private int clockRushTimes = 0;
    private float clockRushElapsed = 0f;
    //private float innerclock = 0f;
    private float[] animdata = new float[0];
    private VisualElement carte;
    private Sprite animverso;
    private List<float[]> animQueue = new List<float[]>();
    private List<Sprite> versoQueue = new List<Sprite>();


    // Start is called before the first frame update
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        carte = root.Q<VisualElement>("carte");
        storyBox = root.Q<TextField>("storyBox");
        updateStoryBox(
            "Une douce lueur pénètre ta chambre. Tu te réveilles avec détermination : cette fois, les sujets de l'examen final de ton cours SI28 ne disparaîtront pas sans laisser de traces. Chaque semestre, la mystérieuse disparition de ces précieux documents te force à annuler l'évaluation pour tes élèves. Mais aujourd'hui, tu en as assez. Ta mission est claire : retrouver ces sujets avant le jour de l'examen, coûte que coûte.\nUn rapide coup d'oeil au calendrier, te confirme ce que tu craignais le plus, nous sommes vendredi et il ne reste que 3 jours avant lundi, le jour de l'examen ! Les prochaines 72 heures seront cruciales. Lundi tout doit être retrouvé !\nL'aventure commence maintenant. Le destin de SI28 est entre tes mains."
            );
        storyBox.SetVerticalScrollerVisibility(ScrollerVisibility.Auto);
        fullStoryBox = root.Q<TextField>("fullStoryBox");
        fullStoryBox.SetVerticalScrollerVisibility(ScrollerVisibility.Auto);

        dayLabel = root.Q<Label>("Jour");
        momentLabel = root.Q<Label>("Moment");
        clockLabel = root.Q<Label>("Heure");

        //init boutons
        Button[] buttons = new Button[7];
        for(int i = 0; i < 7; i++) {
            buttons[i] = root.Q<Button>("Button" + (i+1));
        }
        buttons[0].clicked += () => locationHandler.playCard(0);
        buttons[1].clicked += () => locationHandler.playCard(1);
        buttons[2].clicked += () => locationHandler.playCard(2);
        buttons[3].clicked += () => locationHandler.playCard(3);
        buttons[4].clicked += () => locationHandler.playCard(4);
        buttons[5].clicked += () => locationHandler.playCard(5);
        buttons[6].clicked += () => locationHandler.playCard(6);

        root.Q<Button>("finir").clicked += () => gm.declenchefin();

        root.Q<Button>("vrai").clicked += () => gm.retourquizz(true);
        root.Q<Button>("faux1").clicked += () => gm.retourquizz(false,"la directrice");
        root.Q<Button>("faux2").clicked += () => gm.retourquizz(false,"l'agent de sécurité");
        root.Q<Button>("faux3").clicked += () => gm.retourquizz(false, "mes étudiants");

        for (int i = 0;i < 7;i++) { // init sprites
            decrCardCounter(i);        
        }

        /*
        animate_card(0);
        animate_card(1);
        animate_card(2);
        animate_card(3);
        animate_card(4);
        animate_card(5);
        animate_card(6);
        animate_card(3);
        animate_card(4);
        animate_card(5);
        animate_card(0);
        animate_card(1);
        animate_card(2);
        */
    }

    public void affichequizz()
    {
        root.Q<VisualElement>("quizz").style.display = DisplayStyle.Flex;
    }

    public void afficheboutonfin()
    {
        root.Q<VisualElement>("fin").style.display = DisplayStyle.Flex;
    }
    public string getFullStoryBox()
    {
        return fullStoryBox.value;
    }

    public string getStoryBox()
    {
        return storyBox.value;
    }
    public void updateStoryBox(string text) {
        storyBox.value = text;
    }

    public void updateFullStoryBox()
    {
        fullStoryBox.value += "\n\n" + storyBox.value;
    }

    public void updateDay(string day) {
        dayLabel.text = day;
    }

    public void updateMoment(string moment)
    {
        momentLabel.text = moment;
    }


    public void animate_card(int card)
    {
        float startleft = 2.25f;
        float starttop = 47.25f;
        float startheight = 281f;
        float stoptop = starttop;
        float stopleft = startleft;
        float stopheight = startheight;
        Sprite a = Resources.Load<Sprite>("sprites/carte_dos");
        switch (card)
        {
            case 0:
                stopleft = 22.23f;
                a = Resources.Load<Sprite>("sprites/carte_se_reposer");
                break;
            case 1:
                stopleft = 32.5f;
                a = Resources.Load<Sprite>("sprites/carte_bureau");
                break;
            case 2:
                stopleft = 45f;
                a = Resources.Load<Sprite>("sprites/carte_point_bouffe");
                break;
            case 3:
                stopleft = 56.5f;
                a = Resources.Load<Sprite>("sprites/carte_utc");
                break;
            case 4:
                stopleft = 68f;
                a = Resources.Load<Sprite>("sprites/carte_espace_vert");
                break;
            case 5:
                stopleft = 79.5f;
                a = Resources.Load<Sprite>("sprites/carte_commissariat");
                break;
            case 6:
                stopleft = 91f;
                a = Resources.Load<Sprite>("sprites/carte_chez_valou");
                break;

        }
        stopheight = 143f;
        stoptop = 80.7f;
        versoQueue.Add(a);
        animQueue.Add(new float[] { 0f, startleft, starttop, startheight, stopleft, stoptop, stopheight, (float)card });
    }

    private float lerp(float a, float b, float f)
    {
        return a * (1.0f - f) + (b * f);
    }

    private float cerp(float a, float b, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        float h00 = 2 * t3 - 3 * t2 + 1;
        float h10 = t3 - 2 * t2 + t;
        float h01 = -2 * t3 + 3 * t2;
        float h11 = t3 - t2;

        float m0 = (b - a) * 0.5f;
        float m1 = (b - a) * 0.5f;

        return h00 * a + h10 * m0 + h01 * b + h11 * m1;
    }

    private void incrCardCounter(int card)
    {
        Label l = root.Q<Label>("ind_"+(card+1));
        if (int.Parse(l.text) == 0)
        {
            //update image sprite
            Button b = root.Q<Button>("Button" + (card + 1));
            b.style.unityBackgroundImageTintColor = new Color(1, 1, 1, 1f);
        }
        l.text = (int.Parse(l.text) + 1).ToString();
    }
    public void decrCardCounter(int card)
    {
        Label l = root.Q<Label>("ind_" + (card+1));
        if (int.Parse(l.text)> 1)
        {
            l.text = (int.Parse(l.text) - 1).ToString();
        }
        else
        {
            // update image sprite
            Button b = root.Q<Button>("Button" + (card + 1));
            b.style.unityBackgroundImageTintColor = new Color(1, 1, 1, 0.2f);
            l.text = "0";

        }
        
    }

    public int queryCardCount(int card)
    {
        Label l = root.Q<Label>("ind_" + (card + 1));
        return int.Parse(l.text);
    }
    private void Update()
    {
        /*
        innerclock += Time.deltaTime;

        if(innerclock%1f<=Time.deltaTime)
        {
            animate_card(jjj%7);
            jjj++;
        }
        */
        if(animdata.Length != 0)
        {
            float d = 0.9f; //anim duration
            float f = 5f; //fade duration = d * 1/f
            float t = 2.5f; // turn duration = d * 1/t
            
            if (animdata[0] > d)
            {
                incrCardCounter((int)animdata[7]);
                animdata = new float[0];
                carte.style.unityBackgroundImageTintColor = new Color(1, 1, 1, 0);
                carte.style.left = new Length(0, UnityEngine.UIElements.LengthUnit.Percent);
                carte.style.top = new Length(0, UnityEngine.UIElements.LengthUnit.Percent);
                //fin d'anim
            }
            else
            {
                if (animdata[0] == 0)
                {
                    carte.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("sprites/carte_dos"));
                    //carte.style.unityBackgroundImageTintColor = new Color(1, 1, 1, 1);

                }
                else
                {
                    //fade
                    if (animdata[0] < d/f) {
                        carte.style.unityBackgroundImageTintColor = new Color(1, 1, 1, lerp(0f, 1f, animdata[0]/ (d / f)));
                    }
                    else if(animdata[0] > (f-1) * d / f)
                    {
                        carte.style.unityBackgroundImageTintColor = new Color(1, 1, 1, lerp(1f, 0f, (animdata[0]- (f-1) * d / f)/(d/f)));
                    }
                    else
                    {
                        carte.style.unityBackgroundImageTintColor = new Color(1, 1, 1, 1);
                    }
                    //turn
                    if (animdata[0]> (d/2)-(d/t) && animdata[0] < d / 2)
                    {
                        float wmod = lerp(1f, 0f, (animdata[0]-(d/2 - d/t))/(d/t));
                        //carte.style.width = new Length(lerp(animdata[3], animdata[6], animdata[0] / d) * 0.7592f * wmod, UnityEngine.UIElements.LengthUnit.Pixel);
                        carte.style.scale = new Scale(new Vector2(wmod, 1f));
                    }
                    else if (animdata[0] < (d/2) + (d/t) && animdata[0] > (d/2))
                    {
                        float wmod = lerp(0f, 1f, ((animdata[0] - (d / 2)) / (d / t)));
                        //carte.style.width = new Length(lerp(animdata[3], animdata[6], animdata[0] / d) * 0.7592f * wmod, UnityEngine.UIElements.LengthUnit.Pixel);
                        carte.style.scale = new Scale(new Vector2(wmod,1f));
                    }
                    else
                    {
                        carte.style.scale = new Scale(new Vector2(1,1f));
                    }
                    if (animdata[0] > d / 2)
                    {
                        carte.style.backgroundImage = new StyleBackground(animverso);
                    }
                    carte.style.left = new Length(cerp(animdata[1], animdata[4], animdata[0] / d), UnityEngine.UIElements.LengthUnit.Percent);
                    carte.style.top = new Length(cerp(animdata[2], animdata[5], animdata[0] / d), UnityEngine.UIElements.LengthUnit.Percent);
                    carte.style.height = new Length(cerp(animdata[3], animdata[6], animdata[0] / d), UnityEngine.UIElements.LengthUnit.Pixel);
                    carte.style.width = new Length(cerp(animdata[3], animdata[6], animdata[0] / d) * 0.7592f, UnityEngine.UIElements.LengthUnit.Pixel);
                    ;
                }
                animdata[0] += Time.deltaTime;
            }
            ;
        }
        else
        {
            if (animQueue.Count > 0)
            {
                animdata = animQueue[0];
                animverso = versoQueue[0];
                animQueue.RemoveAt(0);
                versoQueue.RemoveAt(0);
            }
        }

        clockFlickerElapsed += Time.deltaTime;
        if (clockFlickerElapsed >= clockFlickerSpeed)
        {
            clockFlickerElapsed = 0;
            if (clockFlicker)
            {
                clockLabel.text = clockhours.ToString("D2") + " " + clockminutes.ToString("D2");
                clockFlicker = false;
            }
            else
            {
                clockLabel.text = clockhours.ToString("D2") + ":" + clockminutes.ToString("D2");
                clockFlicker = true;
            }
        }

        if (clockRushTimes > 0)
        {
            if (clockhours == clockMax)
            {
                clockRushTimes--;
                incrClockMax();
                clockElapsed = 0;
            }
            else
            {
                clockRushElapsed += Time.deltaTime;
                if(clockRushElapsed >= clockRushSpeed)
                {
                    int ntoincr = Mathf.RoundToInt(clockRushElapsed / clockRushSpeed);
                    //Debug.Log(ntoincr);
                    clockRushElapsed = 0;
                    tickClock(ntoincr);
                }
            }
        }
        else
        {
            clockElapsed += Time.deltaTime;
            if (clockElapsed >= clockSpeed)
            {
                tickClock();
                clockElapsed = 0;
            }
        }
    }

   

    private void updateClock()
    {
        if (clockFlicker)
        {
            clockLabel.text = clockhours.ToString("D2") + ":" + clockminutes.ToString("D2");
        }
        else
        {
            clockLabel.text = clockhours.ToString("D2") + " " + clockminutes.ToString("D2");
        }
    }

    private void incrClockHours()
    {
        clockhours += 1;
        if (clockhours >= 24)
        {
            clockhours = 0;
        }
    }

    private void incrClockMinutes(int n = 1)
    {
        for (int i = 0; i < n; i++) {
            clockminutes += 1;
            if (clockminutes >= 60)
            {
                clockminutes = 0;
                incrClockHours();
            }
        }
        
    }

    private void incrClockMax()
    {
        clockMax += 4;
        if(clockMax >= 24)
        {
            clockMax = 0;
        }
    }

    private void tickClock(int n = 1)
    {
        if (clockhours != clockMax)
        {
            incrClockMinutes(n);
        }
        updateClock();
    }
    public void rushclock(int times)
    {
        clockRushTimes += times;
    }
}
