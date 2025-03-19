using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class espace_vert : MonoBehaviour
{
    [SerializeField] private Gm gm;
    [SerializeField] private locationHandler t_ou;
    public string trigger()
    {
        string texte ="";
        int adv = gm.getAdvancement();
        switch(adv){
            case 0:
                texte = croiser(adv);
                // les sujets sont dans le parc donc il chauffe
                gm.IncreaseClueSubjects();
                break;
            case 1:
                texte = croiser(adv);
                // les sujets sont dans le parc donc il chauffe
                gm.IncreaseClueSubjects();
            break;
            case 2:
                texte = ressource();
                gm.setCardsToDraw(1);
                gm.IncreaseFindNothing();
            break;
            case 3:
                texte = ressource();
                gm.setCardsToDraw(1);
                gm.IncreaseFindNothing();
            break;
            case 4:
                texte =caca(adv);
                gm.IncreaseClueSubjects();
            break;
            case 5:
                texte =caca(adv);
                gm.IncreaseFindEverything();
            break;
            case 6:
                texte =poubelle();
                // il aura essayer de trouver les sujets dans les poubelles
                gm.IncreaseClueSubjects();
            break;
            case 7:
                texte =poubelle();
                // il aura essayer de trouver les sujets dans les poubelles
                gm.IncreaseClueSubjects();
            break;
            case 8:
                texte =croiser(adv);
                gm.IncreaseGoToTaxHavenWithValerie();
            break;
            case 9:
                texte =croiser(adv);
                gm.IncreaseGoToTaxHavenWithValerie();
            break;
            case 10:
                texte =bonne_poubelle();
                gm.IncreaseClueSubjects();
                //trigger fin retrouver sujets
                gm.finJeu(2);
            break;
            case 11:
                texte =rip();
                gm.finJeu(3);
            break;
            case 12:
                texte =ressource();
                gm.setCardsToDraw(1);
                gm.IncreaseFindNothing();
            break;
            case 13:
                texte =poubelle();
                // il aura essayer de trouver les sujets dans les poubelles
                gm.IncreaseClueSubjects();
            break;
            case 14:
                texte =poubelle();
                // il aura essayer de trouver les sujets dans les poubelles
                gm.IncreaseClueSubjects();
            break;
            case 15:
                texte =croiser(adv);
                gm.IncreaseGoToTaxHavenWithValerie();
            break;
            case 16:
                texte =bonne_poubelle();
                gm.IncreaseClueSubjects();
                //trigger fin retrouver sujets
                gm.finJeu(2);
            break;
            case 17:
                texte =pleurer();
                gm.IncreaseFindNothing();
            break;
            default:
            break; 
        }
        //retourne la suite de l'histoire
        return texte;
    }
    private string croiser(int adv){
        string texte = "";
        switch(adv){
            case 0: 
                texte +="Comme tu es beaucoup stressé pour tes sujets, tu décides de te détendre au parc. Là-bas tu croises ta collègue Valérie avec son chien. "
                +"Après une petite discussion, tu restes au parc pour respirer et réfléchir sur ta situation.\n";
            break;
            case 1: 
                texte +="Comme tu es beaucoup stressé pour tes sujets, tu décides de te détendre au parc. Là-bas tu croises ta collègue Valérie avec son chien. "
                +"Après une petite discussion, tu restes au parc pour respirer et réfléchir sur ta situation.\n";
            break;
            case 8:
                texte +="Tu décides de te détendre en allant au parc.\n"
                +"Tiens, c'est Valérie qui se promène avec son chien. C'est agréable de discuter un peu avec elle.\n";
            break;
            case 9:
                texte +="Tu décides de te détendre en allant au parc.\n"
                +"Tiens, c'est Valérie qui se promène avec son chien. C'est agréable de discuter un peu avec elle.\n";
            break;
            case 15:
                texte +="Tu décides de te détendre en allant au parc.\n"
                +"Tiens, c'est Valérie qui se promène avec son chien. C'est agréable de discuter un peu avec elle.\n";
            break;
            default:
            break;
        }
        return texte;
    }
    private string ressource(){
        string texte ="Tu décides d'aller au parc pour te ressourcer. Le chant des oiseaux est très réconfortant.\n";
        return texte;
    }
    private string caca(int adv){
        string texte="Tu décides de te dégourdir les jambes au parc le plus proche.\n"
        +"Malheureusement, tu marches dans du caca de chien ! (pas de bol)\n";
        switch(adv){
            case 4:
                texte +="Tiens, tiens, tiens.... Ce ne serait pas ton stylo que tu as perdu avec tes sujets ? C'est sûrement un indice !\n";
            break;
            case 5:
                texte +="Tes élèves (le trio infernal) te voient.\n"
                +"Louis : Oh bonjour Monsieur vous allez bien ?\n"
                +"Agathe : Ah Monsieur vous ferez attention vous avez marché dans du caca là.\n"
                +"Toi : Ah oui bonjour ! (j'ai vu Agathe MDR TG) Qu'est-ce que vous faites là ?\n"
                +"Emma : Ah oui en fait on a perdu quelque chose ici donc on le recherche ^^\n"
                +"Vous vous séparez. C'est bizarre, ils ne semblaient pas très à l'aise...";
            break;
            default:
            break;
        }    
        return texte;
    }
    private string poubelle(){
        string texte = "Tu vas au parc pour te détendre. Tu remarques que les poubelles de tri n'ont pas encore été ramassées... et si ???\n"
        +"Tu fouilles les poubelles dans l'espoir que la personne ayant volé les sujets soit assez bête pour les avoir jetés dans les poubelles du parc. "
        +"Malheureusement, tu n'as rien trouvé.\n";
        return texte;
    }
    private string rip(){
        string texte = "Tu deviens fou et décides de t'infiltrer au parc alors qu'il est fermé. Soudainement, tu rencontres des délinquants que ta maman t'a toujours dit d'éviter.\n"
        +"Malheureusement, ils n'aiment pas le fait que tu sois sur leur territoire. Du coup tu te fais taper dessus et ça fait mal. rip.\n";
        return texte;
    }
    private string bonne_poubelle(){
        string texte = "Tu passes au parc peut-être une dernière fois avant la fin du week-end. Tu aperçois encore une fois les poubelles de tri. Est-ce que je tombe aussi bas ? Après tout, ma dignité est déjà partie.\n"
        +"Tu décides de fouiller les poubelles.....\n"
        +"OOHH! ILS SONT LÀ ! LES SUJETS !\n"
        +"Pris par une joie débordante, tu fais du breakdance. (pas mal le niveau)\n";
        return texte;
    }
    private string pleurer(){
        string texte = "Ce fut de long 3 jours... Pour décompresser de cette période intense (mais sans fièvre), tu décides d'aller dans ton jardin.\n"
        +"Là, tu procèdes à te mettre en position latérale de sécurité afin de pleurer car tu es un VRAI HOMME A EMOTIONS <3";
        return texte;
    }
}
