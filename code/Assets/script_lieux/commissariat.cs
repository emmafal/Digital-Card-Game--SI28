using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commissariat : MonoBehaviour
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
                        // fermé redirection à l'espace vert
                        text = "Il est tôt, les fonctionnaires ne commencent pas à 5h quand même. Tu vas te promener dans le parc d'à-côté.\n";
                        // redirection vers espace vert
                        text += localisation.triggerLocation(4);
                        break;
                    case 1: // matin
                        // tu portes plainte pour vol de sujets mais ils s'en foutent
                        // ah mais voila machin
                        text = portePlainteMaisSenFoutent();
                        text += rencontreDesEtudiants();
                        // parce qu'il fait l'effort alors il peut tout retrouver 
                        gm.IncreaseFindEverything();
                        break;
                    case 2: // midi
                        // tu portes plainte pour vol de sujets mais ils s'en foutent
                        text = portePlainteMaisSenFoutent();
                        // tu fais ta propre enquête
                        text += "Comme ils ne veulent pas t'aider, tu décides de mener ta propre enquête. Parce qu'après tout, tu es un professeur à l'UTC, tu as plus de diplômes qu'eux !\n";
                        gm.IncreaseFindEverything();
                        gm.IncreaseBecomeDetective();
                        break;
                    case 3: // aprem
                        // tu portes plainte, ils acceptent et t'aident
                        text = portePlainteAcceptentEtAident();
                        gm.IncreaseClueSubjects();
                        gm.IncreaseBecomeDetective();
                        break;
                    case 4: //soir
                        // fermé
                        text = "Il est tard, il ne faut pas abuser sur les horaires des fonctionnaires. Tu te dis que tu devrais retourner au bureau pour travailler.\n";
                        // redirection bureau
                        text += localisation.triggerLocation(1);
                        break;
                    case 5: // nuit
                        // fermé
                        // redirection point bouffe
                        text = "Il est tard, il ne faut pas abuser sur les horaires des fonctionnaires. Tu as une petite faim, tu vas au RU.\n";
                        text += localisation.triggerLocation(2);
                        break;
                    default:
                    break;
                }
                break;
            case 2: // day 2
                switch (moment)
                {
                    case 0:
                        // fermé -> redirection à bf
                        text = "Il est trop tôt, le commissariat est fermé. Tu décides d'aller travailler à ton bureau même s'il est méga tôt.\n";
                        text += localisation.triggerLocation(3);
                        break;
                    case 1:
                        // tu portes plainte pour vol de sujets mais ils s'en foutent
                        text = portePlainteMaisSenFoutent();
                        // ah mais voila machin
                        text += rencontreDesEtudiants();
                        // parce qu'il fait l'effort alors il peut tout retrouver 
                        gm.IncreaseFindEverything();
                        break;
                    case 2:
                        // porte plainte, ils acceptent
                        text = portePlainteAcceptentEtAident();
                        // mais te disent que c'est mort
                        text += "Malheureusement la policère a parlé trop vite, elle t'envoie un sms 30 min plus tard pour te dire que ça va être compliqué de les retrouver. "
                            + "Par contre elle te propose de venir boire un verre avec elle ce soir. Tu acceptes, de toute façon qu'as-tu à faire de mieux ?\n";
                        gm.IncreaseClueSubjects();
                        break;
                    case 3:
                        // porte plainte ils acceptent et t'aident
                        text = portePlainteAcceptentEtAident();
                        gm.IncreaseFindEverything();
                        gm.IncreaseBecomeDetective();
                        break;
                    case 4:
                        // fermé
                        text = "Il est tard, le commissariat est fermé. Allons se promener dans le parc d'à-côté, de nuit c'est toujours sympa.\n";
                        // redirection à l'espaces vert
                        text += localisation.triggerLocation(4);
                        break;
                    case 5:
                        // fermé -> redirection ?
                        text = "Il est trop tard, le commissariat est fermé. Allons se promener dans le parc d'à-côté, de nuit c'est toujours sympa.\n";
                        // redirection à l'espace vert
                        text += localisation.triggerLocation(4);
                        break;
                    default:
                    break;
                }
                break;
            case 3: // day3
                switch (moment)
                {
                    case 0:
                        // fermé -> redirection chez valou
                        text = "Il est trop tôt, le bureau est fermé. Pourquoi ne pas aller voir Valérie chez elle ?\n";
                        text += localisation.triggerLocation(6);
                        break;
                    case 1:
                        // porte plainte ils acceptent et t'aident
                        text = portePlainteAcceptentEtAident();
                        gm.IncreaseBecomeDetective();
                        gm.IncreaseFindEverything();
                        break;
                    case 2:
                        // tu portes plainte pour vol de sujets mais ils s'en foutent
                        text = portePlainteMaisSenFoutent();
                        // ah mais voila machin
                        text += rencontreDesEtudiants();
                        gm.IncreaseFindEverything();
                        break;
                    case 3:
                        // porte plainte, ils acceptent
                        text = portePlainteAcceptentEtAident();
                        // mais te disent que c'est mort
                        text += "Malheureusement la policère a parlé trop vite, elle t'envoie un sms 30 min plus tard pour te dire que ça va être compliqué de les retrouver. "
                            + "Par contre elle te propose de venir boire un verre avec elle ce soir. Tu acceptes, de toute façon qu'as-tu à faire de mieux ?\n";
                        gm.IncreaseClueSubjects();
                        break;
                    case 4:
                        // porte plainte ils s'en foutent totalement et rient
                        text = portePlainteMaisSeFoutentDeToi();
                        // tu veux leur prouver que tu n'es pas ridicule -> devenir detective
                        gm.IncreaseBecomeDetective();
                        break;
                    case 5:
                        // porte plainte ils s'en foutent totalement et rient
                        text = portePlainteMaisSeFoutentDeToi();
                        // tu veux leur prouver que tu n'es pas ridicule -> devenir detective
                        gm.IncreaseBecomeDetective();
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
    string portePlainteMaisSenFoutent()
    {
        string text = "Tu vas au commissariat pour porter plainte pour vol de sujets. Tu expliques la situation aux policiers, mais ils ne semblent pas vraiment concernés. "
                     + "Ils te disent que ce n'est pas leur problème et tu insistes mais ils commencent à te rire au nez. "
                     + "Tu es déçu, tu pensais qu'ils t'aideraient.";
        return text;
    }
    string rencontreDesEtudiants(){
        string text = "Oh mais qui vois-tu assis sur un banc ? Ce sont 3 de tes étudiants ! Mais que font-ils ici ? "
        + "Agathe : Oh bonjour Monsieur Serge, comment allez-vous ?\n "
        + "Toi : Moi ça va mais vous qu'est-ce que vous faites ici ? Vous êtes menottés !!\n"
        + "Tu es gravement surpris ce sont tes 3 meilleurs élèves...\n "
        + "Emma : Ah heu oui cela... heu... ce n'est rien Monsieur Serge\n" 
        + "Louis : Ce n'est qu'un malententu !\n" 
        + "Le policier arrive : Un malentendu les jeunes !! Vous êtes en garde à vue pour avoir fait des graffitis sur votre propre université ! En plus, c'est quoi ce tag affreux ?!\n"
        + "Le policier te montre une photo du tag et tu te rends compte que C'EST TA TÊTE SUR CE MUR !\n";
        return text;
    }
    string portePlainteAcceptentEtAident(){
        string text ="Tu vas au commissariat pour porter plainte pour vol de sujets. Tu expliques la situation au secrétaire du commisariat, il te regarde incrédule mais comprend ta situation.\n"
        + "La policière chargée de te recevoir te sourit et accepte de t'aider dans cette recherche. Elle te dit qu'elle va faire son possible pour retrouver les sujets.\n"
        + "Et qu'elle te tiendra au courant du moindre indice. Tu lui donnes ton numéro. Tu as peut-être une touche avec elle qui sait.\n";
        return text;
    }
    string portePlainteMaisSeFoutentDeToi(){
        string text = "Tu vas au commissariat pour porter plainte pour vol de sujets. Tu expliques la situation au secrétaire du commisariat, il te regarde incrédule et ramène ses collègues policiers.\n"
            + "Ils se mettent à rire de toi, ils te disent que tu es ridicule et que tu ferais mieux de retourner chez toi. Vraiment tu fais perdre du temps à tout le monde, tu abuses.\n";
        return text;
    }
}
