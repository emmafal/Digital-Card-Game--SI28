using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bureau : MonoBehaviour
{
    [SerializeField] private Gm gm;
    [SerializeField] private locationHandler localisation;

    public string trigger()
    {
        // moment de 0 à 5 
        // avance l'histoire de x moments gm.increaseAvancement(5);
        // ne pas augmenter  avancement si redirection
        // getAvancementDay()
        int adv = gm.getAdvancement();
        int moment = gm.getMoment();
        int day = gm.getDay();
        string text = "";
        // ajouter des conditions pour les moments avec l'aide de gm
        switch (day)
        {
            case 1: // day 1
                switch (moment)
                {
                    case 0: // aube
                        //Debug.Log("3");
                        // fermé
                        text = "Il est trop tôt, le bureau est fermé. Tu déambules devant BF.\n";
                        // redirection vers BF & ne pas augmenter avancement
                        text += localisation.triggerLocation(3);
                        break;
                    case 1: // matin
                        text = directriceDemandeRefaireSujets();
                        break;
                    case 2: // midi
                        if (localisation.getDerniereCarte(1) == 1){
                            // on les fait sous stress si deja convoqué
                            text = refaireLesSujetsSousPression();
                        } else { // la directrice te convoque
                            text = directriceDemandeRefaireSujets();
                        }
                        break;
                    case 3: // aprem
                        // tu travailles 
                        text = travailler();
                        // il travaille donc il ne trouve rien (juste pour l'aprem)
                        gm.IncreaseFindNothing();
                        break;
                    case 4: //soir
                        // tu travailles 
                        text = travailler_bureau();
                        text += "Dernière ligne droite avant de rentrer à la maison, courage !";
                        break;
                    case 5: // nuit
                        text = "Il est tard, le bureau est fermé, tu dois aller ailleurs. Allons chez Valérie !\n";
                        // redirection chez valérie 
                        text += localisation.triggerLocation(6);
                        break;
                    default:
                    break;
                }
                break;
            case 2: // day 2
                switch (moment)
                {
                    case 0:
                        // fermé -> redirection se reposer
                        text = "Il est trop tôt, le bureau est fermé. D'ailleurs tu "
                        + "manques de sommeil, tu devrais aller te reposer.\n";
                        text += localisation.triggerLocation(0);
                        gm.IncreaseFindNothing();
                        break;
                    case 1:
                        // directrice te dit de refaire tes sujets
                        // si on a deja été convoqué ou que alors on est sous pression
                        if (localisation.getDerniereCarte(1) == 1 || localisation.getDerniereCarte(2) == 1){
                            text = travailler_bureau();
                        } else {
                            // sinon on les fait sous stress
                            text = directriceDemandeRefaireSujets();
                        }
                        break;
                    case 2:
                        // tu travailles 
                        text = travailler();
                        text += "Allez hop, au boulot ! ";
                        break;
                    case 3:
                        // tu travailles 
                        text = travailler();
                        text += "Quel fainéant tu es de ré-utiliser tes anciens cours ! "
                                + "Tu devrais les refaire quand même... 2010 ce n'est plus d'actualité ! ";
                        gm.IncreaseFindNothing();
                        break;
                    case 4:
                        // tu travailles 
                        text = travailler_bureau();
                        text += "Dernière ligne droite avant de rentrer à la maison, courage ! ";
                        break;
                    case 5:
                        // fermé
                        text = "Il est tard, le bureau est fermé. Oui fermé c'est fermé tu vas pas casser un carreau quand même. " 
                        + "Allons manger un bout !\n";
                        // redirection point bouffe
                        text += localisation.triggerLocation(2);
                        break;
                    default:
                    break;
                }
                break;
            case 3: // day3
                switch (moment)
                {
                    case 0:
                        // fermé
                        text = "Il est trop tôt, le bureau est fermé. D'ailleurs tu as une certaine flemme qui monte de travailler. Tu devrais aller manger un bout.\n";
                        // redirection point bouffe
                        text += localisation.triggerLocation(2);
                        break;
                    case 1:
                        text = dernierJourRefaireSujets();
                        break;
                    case 2:
                        text = dernierJourRefaireSujets();
                        break;
                    case 3:
                        text = dernierJourRefaireSujets();
                        break;
                    case 4:
                        text = dernierJourRefaireSujets();
                        break;
                    case 5:
                        text = refaireSujetsVite();
                        
                        break;
                    default:
                    break;
                }
                break;
            default:
            break;
        }
        //Debug.Log("4");
        //retourne la suite de l'histoire
        return text;
    }

    string directriceDemandeRefaireSujets()
    {
        string text = "La directrice te convoque dans son bureau. " 
                        + "Elle n'est pas contente du tout que tu aies ENCORE UNE FOIS perdu les sujets. "
                        + "Elle ne te laisse pas le choix, il faut que tu refasses les sujets sinon gare à toi... ";
        gm.IncreaseMakeNewSubjects();
        return text;
    }
    string refaireLesSujetsSousPression(){
        string text = "La directrice est là en face de toi, pas contente du tout, elle te regarde avec un air sévère. "
        + "Tu sens la pression monter en toi, tu as peur de ce qu'il va se passer si tu ne réussis pas à refaire les sujets. "
        + "'Mettez-vous au travail, bon sang !' te crie-t-elle dessus. Tu n'as pas le choix, tu dois refaire les sujets. ";
        gm.IncreaseMakeNewSubjects();
        return text;
    }
    string travailler(){
        string text ="Tes étudiants t'attendent avec impatience. "
        + "De quoi vas-tu parler aujourd'hui ? Il serait peut-être temps de préparer ce cours non ? ";
        return text;
    }
    string travailler_bureau(){
        string text = "Tu es dans ton bureau, tu as du travail à faire. "
        + "Tu as des cours à préparer, des sujets à refaire OUI OUI peut-être faut-il y penser ! "
        + "Tu as du pain sur la planche, il va falloir t'y mettre ! ";
        return text;
    }
    string dernierJourRefaireSujets(){
        string text = "";
        // tu refais les sujets 
        // if (pas deja demander par la directrice ou par Valérie) ->
        if(gm.getScoreNouvSujet() > 2){
            // if (tu as deja travaille sur les sujets 2 fois) fin avec nouveaux sujets
            text += "Tu as bien avancé sur tes nouveaux sujets. Ils sont très qualitatifs, tes étudiants seront ravis du QCM à point bonus ! ";
            gm.IncreaseMakeNewSubjects();
        } else if(localisation.getDerniereCarte(7) == 3 || localisation.getDerniereCarte(8) == 3 || localisation.getDerniereCarte(10) == 6){
            text = directriceDemandeRefaireSujets();
        }else{
            // sinon on les fait sous stress
            text = "Tu es stressé, tu n'as pas fini de préparer les sujets pour lundi. Tu ne veux pas être viré. Il faut vite que tu travailles dessus. Allez hop hop hop ! ";
        }        
        return text;
    }
    string refaireSujetsVite(){
        string text = "";
        // si tu n'as pas assez travaillé sur les sujets
        if(gm.getScoreNouvSujet() < 4){
            text = "Tu n'as pas assez travaillé sur les sujets, tu n'as pas eu le temps de les refaire correctement. "
            + "Tu as peur de la réaction de tes étudiants, tu sais que tu vas te faire lyncher. "
            + "Tu n'as pas le choix, tu dois travailler toute la nuit. Prends du café ça va bien se passer (ou pas).";
            gm.IncreaseMakeNewSubjects();
        }
        else {
            // si tu as bien travaillé sur les sujets
            text = "Tu as bien travaillé sur les sujets, tu as réussi à les refaire correctement. "
            + "Tes étudiants seront ravis de ce QCM à point bonus ! ";
        }
        return text;
    }
}
