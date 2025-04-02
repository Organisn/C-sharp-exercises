using System.Collections.Generic;
//Istruzioni del metodo Main (.NET 6)
// See https://aka.ms/new-console-template for more information

//Immissione polinomio divisore

Console.Write("Il grado del polinomio dividendo: ");
//Grado del polinomio
int gdividendo;
/*Verifica: il valore scelto dall'utente dev'essere un intero positivo.
Finchè non è tale se ne richiede l'inserimento*/
bool verifica = int.TryParse(Console.ReadLine(), out gdividendo);
while (verifica != true && gdividendo < 0)
{
    Console.Write("Il grado dev'essere un intero positivo. Reinserire: ");
    verifica = int.TryParse(Console.ReadLine(), out gdividendo);
}
//Lista di coefficienti
List<int> dividendo = new List<int>();
//Stringa per la visualizzazione del polinomio scelto
string vdividendo = "";
/*Ad ogni membro del polinomio corrispondono un grado e un coefficiente
-> scorrimento dei gradi per il riempimento della lista di coefficienti in loro corrispondenza*/
for (int i = gdividendo; i >= 0; i--) 
{
    Console.Write($"Il coefficiente numero {i}: ");
    int cdividendo;
    /*Verifica: il valore scelto dall'utente dev'essere un intero Z.
    Finchè non è tale se ne richiede l'inserimento*/
    verifica = int.TryParse(Console.ReadLine(), out cdividendo);
    while (verifica != true)
    {
        Console.Write("Il coefficiente dev'essere un intero. Reinserire: ");
        verifica = int.TryParse(Console.ReadLine(), out cdividendo);
    }
    dividendo.Add(cdividendo);
    //Costituzione stringa per la visualizzazione
    if (i == 0) vdividendo += $"+({cdividendo})";
    else if (i == 1) vdividendo += $"+({cdividendo})x ";
    else vdividendo += $"+({cdividendo})x^{i} ";
    //Visualizzazione polinomio finora costruito
    Console.WriteLine($"Polinomio dividendo: {vdividendo}");
}

//Procedimento identico per il polinomio divisore

Console.Write("Il grado del polinomio divisore: ");
int gdivisore;
verifica = int.TryParse(Console.ReadLine(), out gdivisore);
while (verifica != true && gdivisore < 0)
{
    Console.Write("Il grado dev'essere un intero positivo. Reinserire: ");
    verifica = int.TryParse(Console.ReadLine(), out gdivisore);
}

List<int> divisore = new List<int>();
string vdivisore = "";
for (int i = gdivisore; i >= 0; i--)
{
    Console.Write($"Il coefficiente numero {i}: ");
    int cdivisore;
    verifica = int.TryParse(Console.ReadLine(), out cdivisore);
    /*Si vuole verificare anche che il coefficiente divisore possa assumere solo i valori {-1,0,1}
    o, nel caso del primo coefficiente, {-1,1}*/
    if (i == gdivisore)
    {
        while (verifica != true && cdivisore < -1 || cdivisore > 1 || cdivisore == 0)
        {
            Console.Write("Il primo coefficiente dev'essere un intero pari a 1 o -1. Reinserire: ");
            verifica = int.TryParse(Console.ReadLine(), out cdivisore);
        }
    }
    else
    {
        while (verifica != true && cdivisore < -1 || cdivisore > 1)
        {
            Console.Write("Il coefficiente dev'essere un intero compreso tra -1 e 1. Reinserire: ");
            verifica = int.TryParse(Console.ReadLine(), out cdivisore);
        }   
    }
    divisore.Add(cdivisore);
    if (i == 0) vdivisore += $"+({cdivisore})";
    else if (i == 1) vdivisore += $"+({cdivisore})x ";
    else vdivisore += $"+({cdivisore})x^{i} ";
    Console.WriteLine($"Polinomio divisore: {vdivisore}");
}

/*Divisione: p(x) = s(x)d(x) + r(x)
Affinchè si possa registrare e stampare il risultato dell'operazione
è necessario costruire altri due polinomi, ognuno con lista di coefficienti, grado e stringa di visualizzazione 
*/

List<int> quoziente = new List<int>();
//Il grado di s(x) dev'essere pari alla differenza tra i gradi di p(x) e d(x)
int gquoziente = gdividendo - gdivisore;
string vquoziente = "";

//r(x) uguale a p(x) in partenza
List<int> resto = new List<int>();
foreach (int item in dividendo) resto.Add(item);
resto.Reverse(); //Inversione dell'ordine dei coefficienti 
int gresto = gdividendo;
string vresto = "";

//Necessario discutere il caso in cui il grado di d(x) sia maggiore di quello di p(x)
if (gdividendo < gdivisore) Console.WriteLine($"Risultato: {vdividendo} = (0)*({vdivisore}) + ({vdividendo})");
else
{
    int gs = gquoziente;
    while (gresto >= gdivisore) //Finché il grado di r(x) è maggiore o uguale a quello di d(x) è possibile effettuarne la divisione per quest'ultimo
    {
        //Calcolo del coefficiente del monomio di grado gs del quoziente    
        int cquoziente = resto[gresto] / divisore[0];
        quoziente.Add(cquoziente);
        if (gs == 0) vquoziente += $"+({cquoziente})";
        else if (gs == 1) vquoziente += $"+({cquoziente})x ";
        else vquoziente += $"+({cquoziente})x^{gs} ";
        Console.WriteLine($"s(x): {vquoziente}");

        //Calcolo dei coefficienti e del grado del resto risultante
        int i = gresto;
        for (int pr = 0; pr <= gdivisore; pr++)
        {
            resto[i] -= cquoziente * divisore[pr];
            if (i == 0) vresto += $"+({resto[i]})";
            else if (i == 1) vresto += $"+({resto[i]})x ";
            else vresto += $"+({resto[i]})x^{i} ";
            if (resto[i] == 0) 
            {
                gresto --;
                gs--;
            }
            i--;
        }
        Console.WriteLine($"r(x): {vresto}");
    }

    //Verifica: qualora eventuali monomi finali di r(x) non vengano inclusi in vresto durante la procedura di divisione ?  
    switch (gresto)
    {
        case 0:
        if (!vresto.Contains($"+({resto[gresto]})"))
        {
            vresto += $"+({resto[gresto]})";
        };
        break;
        case 1:
        if (!vresto.Contains($"+({resto[gresto]})x "))
        {
            while (gresto >= 0)
            {
                if (gresto == 1) vresto += $"+({resto[gresto]})x ";
                else vresto += $"+({resto[gresto]})";
                gresto--;
            }
        };
        break;
        default:
        if (!vresto.Contains($"+({resto[gresto]})x^{gresto} "))
        {
            while (gresto >= 0)
            {
                if (gresto == 0) vresto += $"+({resto[gresto]})";
                else if (gresto == 1) vresto += $"+({resto[gresto]})x ";
                else vresto += $"+({resto[gresto]})x^{gresto} ";
                gresto--;
            }
        };
        break;
    }
    Console.WriteLine($"r(x) completo: {vresto}");
    Console.WriteLine($"Risultato: {vdividendo} = [{vquoziente}] * [{vdivisore}] + [{vresto}]");    
}