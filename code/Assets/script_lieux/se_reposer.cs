using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class se_reposer : MonoBehaviour
{
    [SerializeField] private Gm gm;
    [SerializeField] private locationHandler locationHandler;
    [SerializeField] private string[] story = new string[100];


    private void OnEnable()
    {
        // histoire de se reposer qui est randomisée
        story[80] = "";
        //nuit classique
        story[87] = "Tu t'endors paisiblement, laissant ton esprit s'abandonner au sommeil réparateur d'une nuit ordinaire";
        story[88] = "Dans le silence réconfortant de la nuit, ton esprit vagabonde, revisitant les rencontres et les péripéties de la journée écoulée, cherchant des indices cachés qui pourraient t'aider à retrouver ce fichu coupable.";
        story[89] = "Alors que tu t'endors paisiblement, tu savoures le confort de cette nuit au sommeil régulier, où chaque heure qui s'écoule te rapproche doucement d'un nouveau jour, et de nouvelles opportunités pour résoudre cette situation dans laquelle tu te retrouves.";
        //grasse mat
        story[90] = "En choisissant de prolonger ton sommeil, tu te laisses envelopper par la quiétude de ce moment, t'offrant ainsi une bulle de détente dans laquelle le stress et les soucis s'évaporent peu à peu.";
        story[91] = "Faire la grasse matinée devient un doux remède pour apaiser ton esprit agité, te permettant de recharger tes batteries et de repartir de plus belle, prêt à affronter les défis à venir.";
        story[92] = "Tu décides de t'accorder une pause bien méritée en faisant la grasse matinée, savourant l'idée de laisser ton esprit se reposer tandis que le monde continue sa course effrénée.";
        //mini dodo
        story[93] = "Les premières lueurs de l'aube filtrent à travers les rideaux, te rappelant avec une pointe de regret que les nuits courtes laissent toujours leur empreinte. Les quelques heures de sommeil en moins pourraient se faire sentir demain.";
        //sieste
        story[94] = "Les minutes s'étirent avec une douce lenteur alors que tu te délectes de chaque instant de cette sieste bien méritée, te ressourçant et te préparant à affronter le reste de la journée avec une nouvelle vigueur.";
        story[95] = "Dans l'intimité de ton lit douillet, tu te laisses aller à une sieste bien méritée.";
        //nuit couche tot
        story[96] = "Ce soir, tu décides de faire une croix sur les veillées tardives et tu te livres ainsi à une nuit tranquille et revigorante.";
        story[97] = "Alors que la journée tire à sa fin, tu optes pour une soirée plus calme, te permettant de te jeter dans ton lit plus tôt que d'habitude, en quête d'un repos bien mérité qui te rendra plus fort pour les aventures à venir.";
        story[98] = "En écourtant ta soirée pour te mettre au lit plus tôt, tu t'assures une nuit plus longue, et tout autant plus reposante.";

        //option par defaut
        story[99] = "Problème d'histoire de se reposer, désolé(e) du bug"; 
    }
    public string trigger()
    {

        int adv = gm.getAdvancement();
        int moment = gm.getMoment();
        int day = gm.getDay();
        string storyOut = ""; //story output

        // partie où le texte est modifié en fonction de l'avancement
        // changez incr pour les cas particuliers (valeur par défaut = 1)
        //////////////////////////////////////////////////////////////////////////////////////////
        if (moment == 1 || moment == 2 || adv == 0)//grasse mat
        {
            storyOut = story[90 + UnityEngine.Random.Range(0, 3)];
            gm.setCardsToDraw(1);
        }else if(moment == 0) {//mini nuit
            storyOut = story[93];
            gm.setCardsToDraw(3);
        }else if(day != 3)
        {
            switch (moment) {
                case (3): //sieste
                    storyOut = story[94 + UnityEngine.Random.Range(0, 2)];
                    gm.setCardsToDraw(3);
                    break;
                case (4): //couche tôt
                    storyOut = story[87 + UnityEngine.Random.Range(0, 3)];
                    gm.setCardsToDraw(8);
                    gm.incrAmountToAdvance(2);
                    break;
                case(5): //nuit normale
                    storyOut = story[96 + UnityEngine.Random.Range(0, 3)];
                    gm.setCardsToDraw(6);
                    gm.incrAmountToAdvance(1);
                    break;
            }
        }
        else if (adv == 15 || adv == 16)
        {
            string quete = gm.getMaxAdvancement();
            Debug.Log( quete );
            if (quete == "rien")
            {
                if (adv == 15)
                {
                    gm.incrAmountToAdvance(1);

                }
                storyOut = "Tu te remémores ces heures passées sous les draps, la chaleur réconfortante de ton lit t’ayant retenu prisonnier de l’inertie. Les jours se sont succédés sans que tu fasses le moindre progrès, chaque minute t’éloignant un peu plus de ton objectif. Maintenant, la réalité te frappe : les sujets de l’examen final de SI28 sont toujours introuvables, et le moment tant redouté approche.";
            }
            else { storyOut = story[95]; }
            

        }else if (adv == 17)
        { 
            string quete = gm.getMaxAdvancement();
            Debug.Log(quete);
            switch (quete){
                case "paradis":
                    storyOut = "Valérie et toi c'est acté, vous vous aimez vraiment beaucoup. Comme vous êtes tous les deux fatigués, tu lui proposes d'aller chez toi pour que vous soyez tranquilles et pourquoi pas dormir aussi...";
                    gm.IncreaseGoToTaxHavenWithValerie();
                    break;
                case "rien":
                    storyOut = "C'est la fin du weekend. Tu as bien glandouillé tout le weekend, donc ce n'est pas le moment de s'arrêter ! Allez, encore un petit dodo, ça ne fait pas de mal.";
                    gm.IncreaseFindNothing();
                    break;
                case "nouveau":
                    storyOut = "C'est la fin du weekend. Tu décides de rester chez toi, tu es fatigué, tu as beaucoup travaillé sur tes sujets, il faut à présent te reposer. Tu te mets en pyjama et hop au lit !";
                    gm.IncreaseMakeNewSubjects();
                    break;
                case "detective":
                    storyOut = "C'est la fin du weekend. Tu décides de rester chez toi, tu es fatigué, tu as beaucoup enquêter sur tes sujets et le/la/les coupables, il faut à présent te reposer. Tu te mets en pyjama et hop au lit !";
                    gm.IncreaseBecomeDetective();
                    break;
                case "coupable":
                    storyOut = "Tu as récolté pas mal d'indices ou du moins tu as essayé. Tu décides de te coucher, tu as besoin de repos. Tu te mets en pyjama et hop au lit !";
                    gm.IncreaseFindCulpritEnd();
                    break;
                case "sujet":
                    storyOut = "Tu as récolté pas mal d'indices ou du moins tu as essayé. Tu décides de te coucher, tu as besoin de repos. Tu te mets en pyjama et hop au lit !";
                    gm.IncreaseClueSubjects();
                    break;
                case "tout":
                    storyOut = "Tu as récolté pas mal d'indices ou du moins tu as essayé. Tu décides de te coucher, tu as besoin de repos. Tu te mets en pyjama et hop au lit !";
                    gm.IncreaseFindEverything();
                    break;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        //fin des cafouillages

        //retour basique
        if (storyOut == "")
        {
            if (story[adv] != "")
            {
                storyOut = story[adv];
                gm.IncreaseFindNothing();
            }
            else
            {
                storyOut = story[99];
            }
        }
        gm.IncreaseFindNothing();
        storyOut += "\n";

        return storyOut;

    }
}
