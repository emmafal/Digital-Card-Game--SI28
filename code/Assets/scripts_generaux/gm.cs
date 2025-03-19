using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

using Unity.Mathematics;
//using Unity.VisualScripting.ReorderableList;


public class Gm : MonoBehaviour
{
    [SerializeField] private int advancement = 0;
    [SerializeField] private int day = 1;
    [SerializeField] private int moment = 0;
    [SerializeField] private int amountToAdvance = 1; //nombre de moments avanc�s � la fin du tour. default 1
    [SerializeField] private int cardsToDraw = 0; //nombre de cartes � piocher � la fin du tour. default 0
    [SerializeField] private UI ui;
    //private string endScene = "";
    //[SerializeField] private float endTime = 5f;
    [SerializeField] private int maxCards = 5;
    [SerializeField] private endData ed;
    private int faillevel = 1;

    //compteurs des cartes //deprecated, il y a une fonction dans UI : ui.queryCardCount(int card) qui est ghetto mais marche
    //private Unity.Mathematics.Random rand;
    private int cpt_se_reposer = 0;
    private int cpt_bureau = 0;
    private int cpt_chez_valou = 0;
    private int cpt_commissariat = 0;
    private int cpt_BF = 0;
    private int cpt_bouffe = 0;
    private int cpt_espace_vert = 0;

    private int declenchelafinmec = -1;
    

    

    // POSSIBLE ENDS 
    // Find subjects
    [SerializeField] public int clueFindSubjects = 0;

    private void OnEnable()
    {
        drawCards(10);//cartes de depart
    }

    private void Update()
    {
        /*
        if (declenchelafinmec > -1)
        {
            vraifinJeu(declenchelafinmec);
        }
        */
    }





    public void IncreaseClueSubjects(){
        clueFindSubjects++; // increase the number of clues to find the subjects
        return;
    }
    // Find culprit
    [SerializeField] private int clueFindCulpritEnd = 0;
    public void IncreaseFindCulpritEnd(){
        clueFindCulpritEnd++; // increase the number of clues to find the culprit
        return;
    }
    // Find nothing
    [SerializeField] private int findNothing = 0;
    public void IncreaseFindNothing(){
        findNothing++; // increase the number of action that lead to nothing
        return;
    }
    // Find everything : find subjects and culprit
    [SerializeField] private int findEverything = 0;
    public void IncreaseFindEverything(){
        findEverything++; // increase the number of action that lead to find everything
        return;
    }
    // Make new subjects
    [SerializeField] private int makeNewSubjects = 0;
    public void IncreaseMakeNewSubjects(){
        makeNewSubjects++; // increase the number of action that lead to make new subjects
        return;
    }
    // Become a detective
    [SerializeField] private int becomeDetective = 0;
    public void IncreaseBecomeDetective(){
        becomeDetective++; // increase the number of action that lead to becoming a detective
        return;
    }
    // Go to jail
    [SerializeField] private int goToJail = 0;
    public void IncreaseGoToJail(){
        goToJail++; // increase the number of action that lead to going to jail
        return;
    }
    // Go to the hospital because you were beaten up
    [SerializeField] private int goToHospital = 0;
    public void IncreaseGoToHospital(){
        goToHospital++; // increase the number of action that lead to going to the hospital
        return;
    }
    // Go to a tax haven with Valérie
    [SerializeField] private int goToTaxHavenWithValerie = 0;
    public void IncreaseGoToTaxHavenWithValerie(){
        goToTaxHavenWithValerie++; // increase the number of action that lead to going to a tax haven
        return;
    }

    public int getScoreValou(){
        return goToTaxHavenWithValerie;
    }

    public int getScoreNouvSujet(){
        return makeNewSubjects;
    }

    /// <summary>
    /// returns advancement as int
    /// </summary>
    /// <returns></returns>
    public int getAdvancement()
    {
        return advancement;
    }

    /// <summary>
    /// returns current day (1,2 or 3)
    /// </summary>
    /// <returns></returns>
    public int getDay()
    {
        return day;
    }

    /// <summary>
    /// returns current moment as int (0-5)
    /// </summary>
    /// <returns></returns>
    public int getMoment()
    {
        return moment;
    }

    /// <summary>
    /// set the number of moments to advance at the end of the turn
    /// </summary>
    /// <param name="amount"> number of moments to advance</param>
    public void setAmountToAdvance(int amount)
    {
        amountToAdvance = amount;
        return;
    }

    public int getAmountToAdvance()
    {
        return amountToAdvance;
    }

    /// <summary>
    /// set the number of cards to draw at the end of the turn
    /// </summary>
    /// <param name="amount">number of cards to draw</param>
    public void setCardsToDraw(int amount)
    {
        cardsToDraw = amount;
        return;
    }

    /// <summary>
    /// increase or decrease the number of moments to advance at the end of the turn
    /// </summary>
    /// <param name="amount">amount to increase (can be negative)</param>
    /// <param name="protect">optionnal : when true, prevents amountToAdvance from going below 1. Default is true</param>
    public void incrAmountToAdvance(int amount, bool protect = true)
    {
        amountToAdvance += amount;
        if (amountToAdvance < 1 && protect) {
            amountToAdvance = 1;
        }
        return;
    }

    /// <summary>
    /// increase or decrease the number of cards to draw at the end of the turn
    /// </summary>
    /// <param name="amount"> number of cards to draw</param>
    public void incrCardsToDraw(int amount)
    {
        amountToAdvance += amount;
        return;
    }

    /// <summary>
    /// TODO : Loads selected end scene
    /// </summary>
    /// <param name="scene"> name of scene to load</param>
    public void triggerEnd(string fin)
    {
        Debug.Log(fin);
        ed = GameObject.Find("endDataObject").GetComponent<endData>();
        ed.background = Resources.Load<Sprite>("images/" + fin);
        ed.lastStory = ui.getStoryBox();
        ed.story = ui.getFullStoryBox()+ "\n\n" + ui.getStoryBox();
        SceneManager.LoadSceneAsync("fin");
    }


    /// <summary>
    /// end current turn. Called by locationhandler. If you call this function you probably did something wrong.
    /// </summary>
    public void endTurn() {
        //Debug.Log("GAME MASTER : ending turn" + "\n");
        //draw and advance
        increaseAdvancement(amountToAdvance);
        drawCards(cardsToDraw);
        //reset to default values
        amountToAdvance = 1;
        cardsToDraw = 0;

        //test si on a fini les 3 jours
        //skip pour la démo 
        if (advancement >= 18 ){
            //attendre 30s peut-être ?
            finJeu(0);
        }
        
        ////
        //save contents of storybox somewhere ?
        return;
    }

    private int quiRealiste(int[] multipleDrawTracker = null)
    {
        int[] cardCounts;
        if(multipleDrawTracker == null)
        {
            cardCounts = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        }
        else
        {
            cardCounts = multipleDrawTracker;
        }
        int cardsInHand = 0;
        for (int i = 0; i < 7; i++) {
            cardCounts[i]=ui.queryCardCount(i);
            cardsInHand += ui.queryCardCount(i);
        }
        int totalCards = 7 * maxCards;
        int cardsInDeck = totalCards - cardsInHand;
        int r = UnityEngine.Random.Range(0, cardsInDeck);
        int CardsCounted = 0;
        for(int i = 0; i<6; i++)
        {
            CardsCounted += maxCards - cardCounts[i];
            if (r < CardsCounted)
            {
                return i;
            }
        }
        return 6;
    }

    private void drawCards(int amount)
    {
        //Debug.Log("GAME MASTER : drawing " + amount + " cards" + "\n");
        //TODO
        int[] multipleDrawTracker = { 0, 0, 0, 0, 0, 0, 0 };
        if(amount > 0 && ui.queryCardCount(0) == 0)//pioche se reposer en priorité si y'en a pu
        {
            ui.animate_card(0);
            multipleDrawTracker[0]++;
            amount--;
        }
        for (int i =0; i<amount; i++){
            //int qui = UnityEngine.Random.Range(0, 7);//piochage aleatoire nul
            int qui = quiRealiste(multipleDrawTracker);//piochage aleatoire supérieur

            while (ui.queryCardCount(qui) >= maxCards)//empeche de piocher trop de la même carte
            {
                //qui = UnityEngine.Random.Range(0, 7);//piochage aleatoire nul
                qui = quiRealiste(multipleDrawTracker);//piochage aleatoire supérieur
            }
            ui.animate_card(qui);//pioche la carte
            multipleDrawTracker[qui]++;
            switch (qui){
                case 0:
                    cpt_se_reposer++;
                break;
                case 1:
                    cpt_bureau++;
                break;
                case 2:
                    cpt_bouffe++;
                break;
                case 3:
                    cpt_BF++;
                break;
                case 4:
                    cpt_espace_vert++;
                break;
                case 5:
                    cpt_commissariat++;
                break;
                case 6:
                    cpt_chez_valou++;
                break;
                default:
                break;
            }
        }
        return;
    }
    private void increaseAdvancement(int amount)
    {
        //Debug.Log("GAME MASTER : advancing by " + amount + " moments" + "\n");
        advancement += amount;
        moment = advancement % 6;
        if (advancement > 11)
        {
            day = 3;
        }
        else if (advancement > 5)
        {
            day = 2;
        }
        else
        {
            day = 1;
        }

    }

    public void defTexteFin(string texte)
    {
        ui.updateFullStoryBox();
        ui.updateStoryBox(texte);
    }
    public void finJeu(int fin) {
        declenchelafinmec = fin;
        ui.afficheboutonfin();
    }

    public void declenchefin()
    {
        vraifinJeu(declenchelafinmec);
    }

    public void retourquizz(bool success, string acc = "___")
    {
        Debug.Log(success);

        string finCoupable = "Ca y est, j'ai trouvé ! Le coupable c'est toi *pointe du doigt + musique stressante*\n"
        + "Le coupable enfin je veux dire LA coupable c'est toi Valérie ! *OMG*\n"
        + "Mais bon vu que je n'ai pas retrouvé les sujets je vais sûrement me faire virer MAIS je t'entraine dans ma TOMBE VALERIE !!!\n";

        string finTout = "C'est une victoire complète ! J'ai retrouvé les sujets et EN PLUS je détiens la coupable....\n"
        + "Hé oui... Je parle bien de toi... VALERIE !!! *OMG*\n"
        + "Bref je suis un beau gosse, personne ne peut me tester. *dab*\n";

        string finFaux1 = "Après tout ça... je n'ai pas réussi a retrouver qui est responsable de la disparition de mes sujets. J'ai accusé "
            +acc+" mais ce n'était pas eux, et je suis passé pour un idiot devant tout le monde... J'étais si proche, je le sentais !";

        string finFaux0 = "Après tout ça... je n'ai pas réussi a retrouver qui est responsable de la disparition de mes sujets. J'ai accusé "
            + acc + " mais ce n'était pas eux, et je suis passé pour un idiot devant tout le monde... Mais heureusement, j'ai retrouvé mes sujets ! Le final est sauvé !"
            +" Mais le coupable reste au large...";


        if (faillevel == 1) {
            //poser question c'est qui qui le coupable
            //si bon
            if(success)
            {
                defTexteFin(finCoupable);
                triggerEnd("coupable");
            }
            else
            {
                defTexteFin(finFaux1);
                //si faux
                triggerEnd("trompe");
            }

            
        }
        else
        {
            if (success)
            {
                defTexteFin(finTout);
                triggerEnd("tout");
            }
            else
            {
                defTexteFin(finFaux0);
                triggerEnd("retrouve_sujet");
            }
            //poser question c'est qui qui le coupable
            //si bon
            //triggerEnd("tout");
            //defTexteFin(finTout);
            //si faux
            //defTexteFin(finFaux);
            //triggerEnd("sujet2");
        }



    }


    private void vraifinJeu(int fin){
        declenchelafinmec = -1;
        //
        string finRien = "C'est donc ça ma vie... Remplie d'échecs, de difficultés infranchissables et de négativité...\n"
        + "A quoi bon finalement ? Pourquoi ...? C'est une très bonne question ma foi.\n"
        + "Enfin bref je pleure dans mon lit là, laisse-moi tranquille petite voix dans ma tête avec tes cartes là.\n";

        string finTrouverSujets = "C'est un miracle. Mes sujets... Bon dans la poubelle... MAIS MES SUJETS !!\n"
        +"Je ne vais pas perdre mon travail. Mes élèves vont passer leur final sans encombre.\n"
        +"MES SUJJJETTTTS JE SUIS TELLEMENT CONTENT *crie de joie + danse la macarena*\n ";

        string finNouveauxSujets = "JE L'AI FAIT !!! J'ai refait mes sujets dans les temps !!! Quel homme incroyable je suis !!\n"
        +"Mon job est sauvé !! Mais quand-même je me demande qui a bien pu me les voler... et où sont-ils...?\n"
        +"Ce n'est pas grave PARCE QUE J'AI MES SUJJJETTTTSS !!! *pose de winner*\n";

        string finDetective="J'ai trouvé ma vrai voie.... Inspecteur Gadget... Scooby-Doo... Détective Pikachu... Je sais maintenant pourquoi j'en étais si fan....\n"
        +"Mon rêve insoupçonné va être réalisé. Faites attention à moi les chenapans car me voilà DETECTIVE !\n";

        string finPrison = "OULA ce n'était pas prévu ça. Bon bah rip.\n";

        string finHopital = "Je ouille. Bon c'est pas ouf comme situation. Je n'avais pas prévu que ces racailles puissent me faire mal comme ça !\n"
        +"C'est vraiment pas gentil :(\n"
        +"Ah mais malin ! Je peux peut-être réussir à décaler le final...\n"
        +"(Spoil : non.)\n";

        string finParadisFiscal = "Suis-je en train de rêver ? Valérie, moi... Moi, Valérie....\n"
        +"Dans cet avion direction les Caraïbes afin de profiter de ce climat et de l'argent...\n"
        +"C'est donc ça d'être heureux ? Plus de sujets à préparer... plus de sujets à se faire voler... plus de sujets à recommencer....\n"
        +"En compagnie de ma douce. <3";

        if (fin == 0){ //si fin déclenchée par temps écoulé
            string max = getMaxAdvancement();
            switch (max)
            {
                case "sujet":
                    defTexteFin(finTrouverSujets);
                    triggerEnd("retrouve_sujet");
                break;
                case "coupable":
                    faillevel = 1;
                    ui.affichequizz();
                    
                break;
                case "rien":
                    defTexteFin(finRien);
                    triggerEnd("pleure");
                break;
                case "nouveau":
                    defTexteFin(finNouveauxSujets);
                    triggerEnd("nouv_sujet");
                break;
                case "tout":
                    faillevel = 0;
                    ui.affichequizz();
                    

                break;
                case "detective":
                    defTexteFin(finDetective);
                    triggerEnd("détective");
                break;
                case "paradis":
                    defTexteFin(finParadisFiscal);
                    triggerEnd("caraibes");
                break;
                default:
                    defTexteFin(finRien);
                    triggerEnd("pleure");
                break;
            }
        }else{
            switch(fin){
                case 1: //prison
                    defTexteFin(finPrison);
                    triggerEnd("prison");
                break;
                case 2: //trouve les sujets dans la poub
                    defTexteFin(finTrouverSujets);
                    triggerEnd("retrouve_sujet");
                break;
                case 3:
                    defTexteFin(finHopital);
                    triggerEnd("hopital_chambre");
                break;

                default:
                    defTexteFin(finRien);
                    triggerEnd("pleure");
                break;

            }
        }
  
    }

    private string getMax_suj_ou_rien(){
        if(findNothing-2 > clueFindSubjects){
            if(findNothing-2 > clueFindCulpritEnd){
                return "rien";
            }else{
                return "coupable";
            }
        }else{
            if(clueFindSubjects> clueFindCulpritEnd){
                return "sujet";
            } else{
                return "coupable";
            }
        }
        //return "";
    }

    private string getMax_tout_nouv_detect(){
        if(makeNewSubjects > findEverything){
            if(makeNewSubjects > becomeDetective){
                return "nouveau";
            }else{
                return "detective";
            }
        }else{
            if(findEverything> becomeDetective){
                return "tout";
            } else{
                return "detective";
            }
        }
        //return "";
    }

    public string getMaxAdvancement(){
        string max1 = getMax_suj_ou_rien();
        string max2 = getMax_tout_nouv_detect();
        
        int test1, test2;

        switch(max1){
            case "sujet":
                test1 = clueFindSubjects;
            break;
            case "coupable":
                test1 = clueFindCulpritEnd;
            break;
            case "rien":
                test1 = findNothing;
            break;
            default:
                test1 = -1;
            break;
        }

        switch(max2){
            case "tout":
                test2 = findEverything;
            break;
            case "nouveau":
                test2 = makeNewSubjects;
            break;
            case "detective":
                test2 = becomeDetective;
            break;
            default:
                test2 = -1;
            break;
        }

        if(test1 > test2){
            if(test1 > goToTaxHavenWithValerie){
                return max1;
            }else{
                return "paradis";
            }
        }else{
            if(test2> goToTaxHavenWithValerie){
                return max2;
            }else{
                return "paradis";
            }
        }
        //return "";

    }




}


