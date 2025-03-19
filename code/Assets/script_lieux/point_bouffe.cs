using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point_bouffe : MonoBehaviour
{
    [SerializeField] private Gm gm;
    [SerializeField] private locationHandler locationHandler;
    [SerializeField] private string[] story = new string[100];

    private void OnEnable()
    {
        story[0] = "Tu te rends au RU en face de BF. A cette heure-ci, il est fermé... Ce n'est pas le meilleur début à cette enquête. Finalement, tu décides de rentrer chez toi pour prolonger ta nuit.\n";
        story[1] = "Tu vas au crous au 2e étage, tu racontes ta vie à la dame du crous. Ton désespoir face à ta situation... Ton sentiment d'impuissance... Ton désarroi... Bon elle est sympa de rester d'écouter quoi.\n";
        story[2] = "C'est la pause déjeuner et tu croises ta collègue Valérie. Elle est au courant de ta situation et semble inquiète. C'est sympa de sa part sachant que tu ne lui as jamais beaucoup parlé. Elle t'indique que si besoin elle pourra toujours t'aider. Vraiment une chic dame.\n";
        story[3] = "Tu décides de te rendre au crous avant de manger bien évidemment. Cependant ! La dame du crous te dit qu'il y a plus rien à manger. C'est vraiment pas de bol... A la place tu décides de lui parler. Tu lui demandes si elle n'avait rien entendu à propos de ton vol. Elle te révèle que d'après les dire, la sécu aurait laissé les (3) étudiants tranquilles alors qu'ils rodaient autour de ton bureau. Tiens, tiens, tiens étrange...\n";
        story[4] = "Petite pause très sympatoche avec tes amis/collègues. Quand tout d'un coup ! Tu remarques au loin la directrice te regardant d'un mauvais oeil... Tu n'y prêtes pas d'importance mais c'est tout de même étrange...\n";
        story[5] = "Tu es toujours en train de squatter le crous sauf que l'heure de la fermeture était il y a 30 minutes. Les employés commencent vraiment à en avoir marre de toi. Ils appellent la sécu pour te virer.\n";
        story[6] = "Ptite faim du matin donc time to petit déjeuner quoi (sauf si tu dis souper pour le diner auquel cas time to déjeuner dédicace aux Québécois/es)\n";
        story[7] = "Oulala tu as un petit creux. Tu décides d'aller acheter un pain au chocolatine à la boulangerie la plus proche.\n";
        story[8] = "Te voilà encore une fois au crous. 3 de tes étudiants viennent vers toi.\nAgathe : Bonjour Monsieur ! Vous allez bien ? (vous pouvez me payer mon repas svp)\nToi : Euuuh bonjour\nEmma et Louis : Vous avez fini les sujets pour lundi ? Si vous changez d'avis on peut avoir un QCM à la place...\nToi : Aah non haha les sujets sont complètement finis\n Louis : Ah bon !?\nAgathe : On doit y aller au revoir Monsieur\nEtrange interaction...\n";
        story[9] = "Petit ou gros goûter ? Oh qu'il a l'air bon le muffin ! Il y a aussi un donut qui te fait de l'oeil... Que vas-tu prendre hmm hmmm ? Pendant ton hésitation, tu remarques que la directrice aussi prend son goûter. C'est sans doute un bon moment pour l'amadouer, elle ne te porte pas trop dans son coeur...\n";
        story[10] = "Assis à une table, tu refléchis au sens de la vie autour d'une assiette de raviolis. Tu te demandes si tu as bien fait de choisir ce métier. Tu te demandes si tu as bien fait de choisir cette vie. Tu te demandes si tu as bien fait de choisir ce repas. Tu te demandes si tu as bien fait de choisir cette table. En définitif, tu te demandes si tu as bien fait de choisir ce crous. Bon après cette phase de remise en question, tu tente de rassembler tes idées pour la suite de l'enquête. Qu'as-tu récolté comme indices ? Peut-être serait-il temps de reprendre l'enquête ?\n";
        story[11] = "C'est la nuit, mais ça n'empêche pas d'avoir faim ! Allons voir si le crous est ouvert, on sait jamais, tu peux peut-être soudoyer le personnel de nettoyage pour qu'ils te fournissent à manger...\n";
        story[12] = "La personne du crous t'ouvre malgré l'heure prématurée. La dame est très gentille, voire vraiment très très agréable, elle te propose un croissant. Comme vous vous entendez bien, vous decidez d'aller manger votre croissant ensemble devant le beau lever de soleil sur le pont neuf. La journée commence bien !\n";
        story[13] = ""; // story écrite après
        story[14] = story[13];
        story[15] = story[13];
        story[16] = story[13];
        story[17] = ""; // story écrite après
        
        story[99] = "Problème d'histoire, désolé(e) du bug"; //option par defaut
    }
    public string trigger()
    {

        int adv = gm.getAdvancement();
        int moment = gm.getMoment();
        int day = gm.getDay();
        string storyOut = ""; //story output
        // string textePrefixe = ""; // prefixe a storyOut
        // string texteSuffixe = ""; // suffixe a storyOut
        string redirectionVers = ""; // si redirection, le texte va ici;

        //////////////////////////////////////////////////////////////////////////////////////////
        if (adv == 14 || adv == 15 || adv == 16)
        {
            adv = 13;
        }
        switch (adv)
        {
            case 0:
               redirectionVers = locationHandler.triggerLocation(0);
            break;
            case 1:
                gm.setCardsToDraw(2);
            break;
            case 2:
                gm.IncreaseGoToTaxHavenWithValerie();
            break;
            case 3:
                gm.IncreaseFindCulpritEnd();
            break;
            case 6:
                gm.setCardsToDraw(2);
            break;
            case 7:
                gm.setCardsToDraw(2);
            break;
            case 8:
                gm.IncreaseFindEverything();
                gm.IncreaseFindCulpritEnd();
            break;
            case 10:
                gm.IncreaseBecomeDetective();
            break;
            case 11:
                gm.setCardsToDraw(2);
            break;
            case 13 :  // jusque 16
                // juste checker sur 3 possibilités, ne pas tout faire (valérie, nouveaux_sujets, detective)
                string quete = gm.getMaxAdvancement();
                switch (quete){
                    case "paradis": // date ? 
                        story[13] = "Hmm ça sent bon ici, tu te sens bien. Tu décides de rester un peu plus longtemps pour profiter de l'ambiance. Tu te dis que tu pourrais peut-être même y revenir pour un date... avec... Valérie ! Oui pourquoi après tout, elle te plait et ça a l'air réciproque. Allez first date à la pizzeria d'en bas ! Plus qu'à aller lui demander (ça va le faire t'inquiète paaas).\n";
                        gm.IncreaseGoToTaxHavenWithValerie();
                    break;
                    case "nouveau": // Sinon, peut travailler sujets en mangeant
                        story[13] = "Allez tu y es presque, tu as quasiment fini de préparer les sujets pour lundi. Manger et travailler c'est la bonne combinaison. Tu te sens bien au kebab, tu te sens inspiré alors bon appétit et surtout charbonne bien !\n";
                        gm.IncreaseMakeNewSubjects();
                    break;
                    case "detective": // detectiver dans un café ? 
                        story[13] = "Slurp slurp, tu bois ton café en réfléchissant à la suite de l'enquête. Tu as rassemblé plusieurs indices, il faut maintenant réussir à les assembler. Qui a bien pu te voler les sujets et où sont-ils ??? Tu réfléchis durant ta longue pause tel Sherlock Holmes dans son bureau\n";
                        gm.IncreaseBecomeDetective();
                    break;
                    default: // les autres fins
                        story[13] = "C'est bientôt la fin du weekend. Tu restes au café tout l'aprem en te demandant ce que tu as fait ces 3 derniers jours et comment tu as avancé ton cas (ou pas).";
                    break;
                }
            break;
            case 17: // denouement
                quete = gm.getMaxAdvancement();
                switch (quete){
                    case "paradis": 
                    // si le mystere avance pas trop, boite de nuit et croise valérie (last chance) ? 
                    story[17] = "C'est la fin du weekend. Tu décides de sortir en boite, pour évacuer tout ce stress accumulé autour de tes sujets. C'est bon tu es entré, le videur a faillit ne pas te laisser passer mais c'est bon. Petit move sur le dance floor. Oh mais ! OOOH C'EST VALLLÉRRIIIIE !! Tu la rejoins et vous dansez ensemble (bien joué).\n";
                    gm.IncreaseGoToTaxHavenWithValerie();
                    break;
                    default:
                    // ou blackout et c'est la fin nulle / echec
                    story[17] = "C'est la fin du weekend. Tu décides de décompresser chez toi devant ta télé et un verre de vin. Allez un autre petit petit verre ça ne fait pas de mal. Et tu t'écroules dans ton canapé, c'est un gros dodo et surtout gros mal de tête en vu pour demain...\n";                        
                    break;
                }
            break;
            default: 
            break;
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        //fin des cafouillages


        // mettre story[99] si il y a un problème
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
        // ajouter la redirection si il y en a une à l'histoire
        storyOut += redirectionVers;
        return storyOut;

    }
}
