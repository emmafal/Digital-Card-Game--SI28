using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chez_valou : MonoBehaviour
{
    [SerializeField] private Gm gm;
    [SerializeField] private locationHandler t_ou;
    public string trigger()
    {
        string texte = "";
        int adv = gm.getAdvancement();
        switch(adv){
            case 0:
                texte = appelFlics();
                gm.finJeu(1);
            break;
            case 1:
                texte = paslà(adv);
            break;
            case 2:
                texte = paslà(adv);
            break;
            case 3:
                texte = paslà(adv);
            break;
            case 4:
                texte = appelFlics();
                gm.finJeu(1);
            break;
            case 5:
                texte = appelFlics();
                gm.finJeu(1);
            break;
            case 6:
                texte = personne();
                gm.IncreaseFindEverything();
            break;
            case 7:
                texte = aider();
                gm.IncreaseFindCulpritEnd();
                gm.IncreaseGoToTaxHavenWithValerie();
            break;
            case 8:
                texte = discute(adv);
                gm.IncreaseGoToTaxHavenWithValerie();
            break;
            case 9:
                texte = discute(adv);
                gm.IncreaseGoToTaxHavenWithValerie();
            break;
            case 10:
                texte = sujet();
                gm.IncreaseMakeNewSubjects();
            break;
            case 11:
                texte = paslà(adv);
            break;
            case 12:
                texte = enerve();
                gm.IncreaseFindNothing();
            break;
            case 13:
                texte = discute(adv);
                gm.IncreaseGoToTaxHavenWithValerie();
            break;
            case 14:
                texte = aide_sujet();
                gm.IncreaseMakeNewSubjects();
                gm.IncreaseGoToTaxHavenWithValerie();
            break;
            case 15:
                texte = discute(adv);
                gm.IncreaseGoToTaxHavenWithValerie();
            break;
            case 16:
                texte = declaration();
            break;
            case 17:
                texte = fin();
            break;
            default:
            break;
        }
        //retourne la suite de l'histoire
        return texte;
    }
    private string appelFlics(){
        string texte = "Tu vas chez ta collègue Valérie. Elle ouvre la porte un peu confuse car qui pourrait bien frapper à cette heure-ci ? En te voyant, elle prend peur.\n " 
            +"Valérie : Mais qu'est-ce que tu fais là ? Comment tu connais mon adresse ? T'as vu l'heure ?\n"
            +"Dans sa panique, elle appelle la police. Tu n'as pas le temps de réagir ou de la rassurer. La police t'embarque.";
        return texte;
    }
    private string paslà(int adv){
        string texte = "Tu vas chez ta collègue Valérie. Tu toques chez elle mais personne ne répond. Elle n'a pas l'air d'être là.\n";
        switch(adv){
            case 1:
                texte += "A la place, tu te diriges vers le commissariat.\n";
                texte += t_ou.triggerLocation(5);
            break;
            case 2:
                texte += "A la place, tu décides d'aller à BF.\n";
                texte += t_ou.triggerLocation(3);
            break;
            case 3:
                texte += "A la place, tu vas au parc.\n";
                texte += t_ou.triggerLocation(4);
            break;
            case 11:
                texte+= "A la place, tu décides d'aller te sustenter.\n";
                texte += t_ou.triggerLocation(2);
            break;
            default:
            break;
        }
        return texte;
    }
    private string personne(){
        string texte = "Tu décides d'aller chez Valérie ta collègue. Tu frappes à sa porte. Tiens, personne ne répond. "
        + "Dommage ce n'est pas maintenant que tu pourras voir son doux visage.\n"
        +"Tu regardes quand même par la fenêtre en espérant la voir, elle te manque déjà... Oh mais c'est bizarre ce qu'il y a sur sa table. Elle a déjà préparé ses sujets de finaux ?\n";
        return texte;
    }
    private string aider(){
        string texte="Tu toques à la porte de chez Valérie ta collègue. Elle t'ouvre. Elle a l'air heureuse de te voir.\n"
        +"Valérie : Oh salut ! Entre. *Tu entres.* J'ai appris pour tes sujets... Je peux t'aider si tu veux.\n"
        +"Toi : Salut ! Oh je n'ai pas envie de t'embêter avec ça...\n"
        +"Valérie : Non ne t'inquiètes pas ! D'ailleurs il me semble que j'ai vu plusieurs personnes proche de ton bureau jeudi soir. Il me semble même qu'il y en avait 3...\n"
        +"Toi : Oh vraiment ? Merci pour l'info ! Mais ne t'inquiète pas pour ça, parlons d'autres choses...\n"
        +"Vous discutez de tout et de rien et passez une bonne soirée.";
        return texte;
    }
    private string discute(int adv){
        string texte = "Tu décides d'aller voir ta collègue Valérie.\n";
        switch(adv){
            case 8:
                texte += "Ensemble, vous discutez du beau temps.\n"
                + "Valérie : Et sinon des nouvelles de tes sujets ?\n";
                //si t'as vu la directrice
                if(t_ou.getDerniereCarte(7) == 3){
                    texte +="Toi : Je ne sais pas encore où ils sont ou qui est le coupable mais la directrice m'a demandé d'en refaire...\n"
                    +"Valérie : Oh d'accord... Donc tu n'as pas encore d'idée... Enfin bref encore un peu de thé ?\n"
                    +"Vous continuez de discuter tranquillement. Tu sens que tu te rapproches d'elle.";
                }else{
                    //sinon
                    texte += "Toi : Je ne sais pas encore qui me les as volé ni où ils sont mais je continues de chercher ! J'ai confiance.\n"
                    +"Valérie : Oh... donc tu ne sais pas encore qui c'est... Même pas une idée ?\n"
                    +"Toi : J'ai quelques indices mais ce n'est pas encore sûr.\n"
                    +"Valérie : Ah d'accord ! Super alors... Encore un peu de thé ?\n"
                    +"Vous continuez de discuter tranquillement ensemble. Tu sens que ta relation avec elle s'améliore.";
                }
            break;
            case 9:
                texte +="Elle s'inquiète pour toi car tu n'as toujours pas récupéré tes sujets.\n"
                +"Valérie : Mais qui ça pourrait bien être... Réfléchissons ensemble.\n"
                +"Toi : Ce pourrait être mes élèves qui ne voulaient pas passer le final... Comme ces trois-là par exemple. "
                +"Sinon la directrice n'a pas l'air très contente de mon travail. Peut-être cherchait-elle à trouver une excuse pour me remercier ? "
                +"Et après reflexion, c'est possible que ce soit l'agent de sécurité, il n'a pas l'air de beaucoup m'apprécier...\n"
                +"Vous continuez de discuter. Il semblerait que vous vous soyez beaucoup rapprochés.";
            break;
            case 13:
                if (t_ou.getDerniereCarte(adv-1)== 6){
                    //si tu l'as enerve juste avant
                    texte +="Elle n'est toujours pas contente de te voir. (Un peu rude la Valérie)\n"
                    +"Mais elle veut bien te pardonner et te laisse entrer. Ensemble vous discutez et malgré l'accrochage de ce matin vous vous rapprochez.";
                } else {
                    //sinon
                    texte +="Elle est très heureuse de te voir ! Peut-être que votre relation pourrait changer ??\n"
                    +"Vous passez un bon moment à discuter. A tel point que tu oublies toi même d'enquêter sur tes sujets perdus.";
                }
            break;
            case 15:
                if (gm.getScoreValou() > 5)
                    texte+="Il semblerait que tes pieds t'aient encore amené chez Valérie. A croire que tu es fou amoureux là. (A quand le mariage ??)\n"
                    +"Tu décides qu'il est temps de flirtouiller là.\n"
                    +"Toi : Heyyy coucou Valou...(Tu mordilles un peu trop ta lèvre là...)\n"
                    +"Elle éclate de rire. Bon au moins elle te trouve drôle... MAIS QUE VOIS-JE ? Elle a rougi ! On est sur le bon chemin !\n"
                    +"Vous continuez de dicuter tranquillement. Tu as l'impression qu'elle te lance des signaux mais tu n'es pas sûr donc tu les ignores.";

                else
                    texte +="Tu te diriges chez Valérie. Elle t'accueille gentiment.\n"
                    +"Toi : Coucou c'est moi ^^\n"
                    +"Valérie : Oui je vois bien haha\n"
                    +"Toi : Sinon ça va ?\n"
                    +"Malgré ce début très bizarre, vous continuez de discuter tranquillement chez elle. On dirait bien que votre relation s'améliore.";
            break;
            default:
            break;
        }
        return texte;
    }
    private string sujet(){
        string texte = "Tu décides de te rendre chez ta collègue Valérie. Elle t'accueille avec plaisir.\n"
        +"Valérie : Je voulais justement te voir ! J'ai une idée pour tes sujets ! Au lieu de perdre ton énergie et ta santé mentale en cherchant tes sujets, tu pourrais les refaire ?\n"
        +"Toi : C'est en effet une bonne idée ! Je ne sais pas pourquoi je n'ai pas eu cette idée avant !";
        return texte;
    }
    private string enerve(){
        string texte = "Tu te diriges de bon matin chez ta collègue Valérie. En arrivant, tu tambourines à sa porte, emporté par ta bonne humeur. "
        +"Cependant, Valérie ne semble pas partager ton humeur. Agacée en te voyant, elle te dit sèchement de partir. Ce n'était sûrement pas une bonne idée de venir si tôt.";
        return texte;
    }
    private string aide_sujet(){
        string texte = "Tu décides d'aller chez Mlle Valérie.\n"
        +"Valérie : Oh Serge ! Entre, entre.\n"
        +"Toi : Désolé d'arriver en improviste...\n"
        +"Valérie : Pas de soucis. Tu ne me déranges jamais ! (sauf si tu viens à 4h du mat)\n"
        +"Ensemble, vous travaillez sur tes sujets que tu dois vite finir car demain est le jour du final ! (tick tock) "
        +"Vous avez bien travaillé, peut-être que les sujets seront finis à temps ? ";
        return texte;
    }
    private string declaration(){
        string texte= "";
        //si score > jsp
        if(gm.getScoreValou() > 7){
            texte += "En te dirigeant vers la maison de Valérie ton crush, tu la vois au loin.\n"
            +"Toi : Valérie ? Qu'est-ce que tu fais dehors ? J'allais justement chez toi.\n"
            +"Valérie : Oh Serge ! Je te cherchais ! Il faut qu'on parle !\n"
            +"Toi : Pour une bonne chose j'espère...\n"
            +"Valérie : Serge ...! Cela fait 2 jours que je t'envoie des signaux digne du phare d'Alexandrie. Tu n'es tellement pas observateur que je deviens folle !\n"
            +"Toi : Qu'est-ce que tu veux dire exactement ?\n"
            +"Valérie : Je t'aime. Est-ce que tu veux sortir avec moi ?\n"
            +"Toi : ... Mais oui bien sûr ! Je ne peux pas y croire *larme de joie*\n"
            +"Vous décidez de ne pas rester dans la rue parce qu'on n'est pas dans un film. Vous passez une très bonne soirée xoxoxo";
            gm.IncreaseGoToTaxHavenWithValerie();
        }else{
            texte +="Tu décides d'aller chez Valérie. Peut-être que c'est le moment de lui déclarer ta flamme ? Tu as l'impression que ça matche bien entre vous...\n"
            +"Toi : Valérie il faut que je te dise quelque chose.\n"
            +"Valérie : Oui Serge ?\n"
            +"Toi : Alors en fait ces derniers jours on s'est beaucoup rapproché... Et je pense que je veux qu'on sorte ensemble.\n"
            +"Valérie : Oh... Euh comment dire... Tu es pour moi un ami et je ne vois pas cette relation changer...\n"
            +"Aïe. Après cette friendzone l'ambiance est bizarre et tu décides de partir.";
        }
        return texte;
    }
    private string fin(){
        string texte = "";
        int adv = gm.getAdvancement();
        //si pas utilise chez valou juste avant
        if (!(t_ou.getDerniereCarte(adv-1) == 6)){
            texte += declaration();
        }else{
            //si score > jsp
            if(gm.getScoreValou() > 7){
                texte +="Tu passes un agréable moment avec ta petite amie hihi\n";
                gm.IncreaseGoToTaxHavenWithValerie();
            }else{
                //sinon
                texte +="Tes pieds t'ammènent devant chez Valérie. Mais tu repenses encore à ta friendzone et décides de ne pas toquer et de juste partir.";
            }
        }
        return texte;
    }
}

