using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utc : MonoBehaviour
{
    [SerializeField] private Gm gm;
    [SerializeField] private locationHandler locationHandler;
    [SerializeField] private string[] story = new string[100];

    private void OnEnable()
    {   
        story[0] = "Tu vas à BF et tu croises un membre de la sécurité, tu discutes du vol de tes sujets. Il te dit qu'il a vu des gens venir en dehors des heures de cours. Il te conseille de chercher des indices, ils doivent être trouvables ces voleurs quand même.";
        story[1] = "Pendant que tu donnes ton cours, tu repenses au vol de tes sujets. Pourquoi ne pas demander à tes élèves s'ils ont vu quelque chose ou entendu une rumeur ? Tu sais qu'à l'UTC tout va très vite et que tes étudiants te sont très fidèles. Alors tu leur demandes, certains disent que non ils ne savent pas du tout de quoi tu parles, d'autres te disent qu'ils ont vu des gens venir en dehors des heures de cours. Tu te dis que c'est peut-être une piste à suivre. Tu décides de chercher des indices. ";
        story[2] = "Tu vas à BF, tu essaies de discuter avec la sécurité mais le monsieur ne t'aime pas, tu insistes parce que tu voudrais bien avoir des infos. Mais il s'énerve et te dit de te barrer. Alors tu décides de partir (un peu contraint).";
        story[3] = "En allant à BF tu vois la directrice dans le couloir, elle commence à te parler puis soudain elle te demande de venir dans son bureau samedi matin. Elle veut te parler de tes sujets volés. Elle n'est pas très agréable aujourd'hui...";
        story[4] = "Tu te balades autour de BF et en repensant à tes sujets, tu te dis que tu devrais peut-être chercher des indices. Il doit y avoir des personnes qui ont vu le vol se produire ou alors des choses étranges non ? Tu cherches donc des indices et tu interroges des étudiants et passants.";
        story[5] = story[2]; // + condition -> on change texte et il se fait tabasser
        story[6] = story[2]; // + condition -> on change texte et il se fait tabasser
        story[7] = "Tu vas à BF et tu vois un membre de la sécurité, tu lui demandes s'il a vu quelque chose de suspect. Il te dit qu'il a vu des gens venir en dehors des heures de cours jeudi. Il te conseille de chercher des indices, ils doivent être trouvables ces voleurs quand même.";
        story[8] = story[7];
        story[9] = story[7];
        story[10] = story[2]; // + condition -> on change texte et il se fait tabasser
        story[11] = story[4];
        story[13] = "En allant à BF, tu vois des étudiants faire du BKB (le jeu utcéen qui est un mélange de volley et ping pong grosso modo). Ils te proposent de jouer avec eux, alors évidemment tu dis oui, mais c'est vite fatiguant, tu n'as plus 20 ans après tout. Tu décides donc de prendre une pause et tu les regardes depuis le banc de touche."; 
        story[14] = story[13];
        story[15] = "Tu es à BF, tu croises un monsieur de la sécurité. Vous discutez ensemble de vos vies, il te parle de Valérie. QUOI VALÉRIE ?? Il a apparamment un crush sur elle, jaloussiiiieeee.";
        story[16] = "Tu es à BF, tu regardes autour de toi et tu te dis que c'est sans doute la fin de ta carrière. Tu flippes grave et tu pleures.";
        // sinon c'est la merde, tu décides de tagguer tout bf prsk t'es viré de toutes façons
        story[17] = "C'est la merde, tu es dans la merde, que vas-tu faire pour te sortir de cette merde ? Comme tu sens que tu vas te faire virer, tu décides de tagguer tout BF. Bah oui pourquoi pas, tu te sens un peu mieux après ça.";
        story[99] = "Problème d'histoire, désolé(e) du bug"; //option par defaut
    }
    public string trigger()
    {

        int adv = gm.getAdvancement();
        int moment = gm.getMoment();
        int day = gm.getDay();
        string storyOut = ""; //story output

        // partie où le texte est modifié en fonction de l'avancement
        // changez incr pour les cas particuliers (valeur par d�faut = 1)
        //////////////////////////////////////////////////////////////////////////////////////////
        if (adv == 6 || adv == 10)
        {
            adv = 5;
        }
        switch (adv)
        {
            case 0:
                gm.IncreaseBecomeDetective();
                gm.IncreaseFindCulpritEnd();
            break;
            case 1:
                gm.IncreaseBecomeDetective();
                gm.IncreaseFindCulpritEnd();
            break;
            case 4:
                gm.IncreaseBecomeDetective();
                gm.IncreaseFindEverything();
            break;
            case 5: // et 6 et 10
                if(locationHandler.getDerniereCarte(2) == 3 || locationHandler.getDerniereCarte(5) == 3 || locationHandler.getDerniereCarte(6) == 3 || locationHandler.getDerniereCarte(10) == 3){
                    storyOut = "Tu vas à BF et tu essaies de discuter avec la sécurité mais le monsieur ne t'aime pas, tu insistes parce que tu voudrais bien avoir des infos. Le monsieur de la sécurité te regarde vraiment énervé, il regarde autour de lui, et d'un coup se jette sur toi. Il te mets de bonnes droites et des coups là où c'est interdit. Tu te dis que tu n'aurais pas dû insister.";
                    gm.finJeu(3);
                }
            break;
            case 7:
            // si deja convoqué precedemment : - refait sujet (story[3] utc et (case 1 ou 2) jour 1 sur bureau)
            if (locationHandler.getDerniereCarte(1) == 1 || locationHandler.getDerniereCarte(2) == 1 || locationHandler.getDerniereCarte(3) == 3)
            {
                storyOut = "Tu vas à BF et tu vois de loin la directrice. Tu flippes grave et tu te caches. Elle t'a déjà convoqué dans son bureau, tu as peur pour ton avenir à l'UTC. Tu préfères te mettre à réfléchir pour refaire tes sujets. C'est la solution la plus sage.";
                gm.IncreaseMakeNewSubjects();
            } else { // sinon  tu discutes avec la sécu : nouveaux indices (deja écrit)
                gm.IncreaseBecomeDetective();
                gm.IncreaseFindCulpritEnd ();
            }
            break;
            case 8:
                gm.IncreaseBecomeDetective();
                gm.IncreaseFindCulpritEnd();
            break;
            case 9 :
                gm.IncreaseBecomeDetective();
                gm.IncreaseFindCulpritEnd();
            break;
            case 11:
                gm.IncreaseBecomeDetective();
                gm.IncreaseFindEverything();
            break;
            case 13:
                // incrémentation de l'avancement de +2 prsk il est fatigué
                gm.incrAmountToAdvance(1); 
                gm.IncreaseFindNothing();
            break;
            case 14: 
                gm.IncreaseFindNothing();
            break;
            case 15 :
            // Si tu as fait du bkb avant -> secu te vire et si en + elle ta deja viré -> fin tabasser
            // sinon -> tu discutes avec la sécu : l'un des monsieur de la sécu a un crush sur valérie, jalousiiie (deja écrit)
            bool bkb = locationHandler.getDerniereCarte(13) == 3 || locationHandler.getDerniereCarte(14) == 3;
            bool deja_vire = locationHandler.getDerniereCarte(2) == 3;
            if(bkb){
                storyOut = "Tu vas à BF et tu vois un membre de la sécurité, tu lui demandes s'il a vu quelque chose de suspect. Il te dit qu'il a vu des gens venir en dehors des heures de cours jeudi. Il te conseille de chercher des indices, ils doivent être trouvables ces voleurs quand même.";
                gm.IncreaseBecomeDetective();
                gm.IncreaseFindCulpritEnd();
            }
            else if(bkb && deja_vire){
                storyOut = "Tu es à BF, la sécurité te voit, elle sait que tu as fait du BKB en dérangeant beaucoup de personnes. Elle est très très énervée. Le monsieur de la sécurité regarde autour de lui, et d'un coup se jette sur toi. Il te mets de bonnes droites et des coups là où c'est interdit.";
                gm.finJeu(3);
            }
            else gm.IncreaseGoToTaxHavenWithValerie();
            break;
            case 16:
            string quete = gm.getMaxAdvancement();
            // si quete sujet ou nouveau -> tu regardes bf avec fierté prsk tu sais que tu ne seras pas viré
            // sinon tu flippes grave et tu pleures car c'est sans doute la fin de ta carrière
            if (quete == "sujet" || quete == "nouveau")
            {
                storyOut = "Tu es à BF, tu regardes autour de toi avec fierté. Tu sais que tu as fait du bon boulot, que tu vas continuer et que tu ne seras pas viré. Tu es content de toi. Il faut que tu continues comme ça";
            }
            break;
            case 17 :
            quete = gm.getMaxAdvancement();
            // si quete sujets ou nouveau -> tu danses avec le bro de la secu prsk finalement c'est trop un pote
            if (quete == "sujet" || quete == "nouveau"){
                storyOut = "Tu vas à BF et tu vois un membre de la sécurité, comme tu es content de toi, tu décides de danser avec lui. C'est trop un bon pote en fait !";
            }
            break;
        }

        /////////////////////////////////////////////////////////////////////////////////////////

        //retour basique
        if (storyOut == "")
        {
            if (story[adv] != "")
            {
                storyOut = story[adv];
            }
            else
            {
                storyOut = story[99];
            }
        }        
        storyOut += "\n";

        return storyOut;
    }
}
