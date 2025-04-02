using System.IO; //Classe di metodi per l'accesso ai file in memoria

using Spectre.Console;

//File .txt contenente le prime 1000000 cifre del pi
//Il file si trova nella directory bin\Debug\net6.0 del progetto

string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

//In una stringa viene ricreato il percorso della directory..

string exePath = Path.GetDirectoryName(appPath);

//.. Concatenato al nome del file

string path = Path.Combine(exePath, "pi_dec_1m.txt");

//Metodo per accedere al contenuto del file

string content = File.ReadAllText(path);

//Conversione del contenuto del file in array per poterlo confrontare carattere per carattere con la data in input

content.ToCharArray();

AnsiConsole.WriteLine("La tua data di nascita");

//Al fine di escludere date non valide vengono salvati in un array il formato e i parametri della data odierna

string[] aa = DateTime.Today.ToString().Split("/");

string[] aa2 = aa[2].ToString().Split(" ");

string d = "";

string a = AnsiConsole.Ask<string>("Inserisci l'anno:");

int anno;

//Finché l'anno non è un valore numerico compreso tra 0 e 2022 (anno attuale) viene richiesta l'immissione

while (!int.TryParse(a, out anno) || anno < 0 || anno > Convert.ToInt32(aa2[0]))
{
    a = AnsiConsole.Ask<string>("L'anno non può essere non trascorso o precedente allo 0:");
}

Convert.ToString(anno);

string m = AnsiConsole.Ask<string>("Inserisci il mese:");

int mese;

//Se l'anno inserito è quello attuale è necessario verificare che il mese in input sia trascorso..

if (Convert.ToInt32(anno) == Convert.ToInt32(aa2[0]))
    {
        //.. E comunque un valore numerico maggiore di 1
        while (!int.TryParse(m, out mese) || mese < 1 || mese > Convert.ToInt32(aa[1]))
        {
            m = AnsiConsole.Ask<string>("Il mese non può non essere trascorso:");
        }
    }
//In caso l'anno sia già trascorso il mese può essere uno qualsiasi dei 12
else
{
    while (!int.TryParse(m, out mese) || mese < 1 || mese > 12)
    {
        m = AnsiConsole.Ask<string>("Inserisci un intero compreso tra 1 e 12:");
    }
}

Convert.ToString(mese);

string g = AnsiConsole.Ask<string>("Inserisci il giorno:");

int giorno;

//Come per il mese, anche il giorno dev'essere trascorso o in corso..
if (Convert.ToInt32(anno) == Convert.ToInt32(aa2[0]) && Convert.ToInt32(mese) == Convert.ToInt32(aa[1]))
{
    //.. E un valore numerico maggiore di 1
    while (!int.TryParse(g, out giorno) || giorno < 1 || giorno > Convert.ToInt32(aa[0]))
    {
        g = AnsiConsole.Ask<string>("Il giorno non può non essere trascorso:");
    }
}
//In caso sia anno che mese siano già trascorsi é necessario verificare che il mese sia un valore numerico maggiore di 1 e che..
else
{
    switch (Convert.ToInt32(mese))
    {
        //.. Se è febbraio il giorno sia..
        case 2:
        //.. Non superiore a 29 nel caso dell'anno bisestile..
            if (Convert.ToInt32(anno) % 4 == 0)
            {
                while (!int.TryParse(g, out giorno) || giorno < 1 || giorno > 29)
                {
                    g = AnsiConsole.Ask<string>("Inserisci un intero compreso tra 1 e 29:");
                }
            }
            //.. Altrimenti a 28..
            else
            {
                while (!int.TryParse(g, out giorno) || giorno < 1 || giorno > 28)
                {
                    g = AnsiConsole.Ask<string>("Inserisci un intero compreso tra 1 e 28:");
                }
            }
        break;
        //.. Se è aprile, giugno, settembre o novembre non superiore a 30..
        case 4: case 6: case 9: case 11:
            while (!int.TryParse(g, out giorno) || giorno < 1 || giorno > 30)
            {
                g = AnsiConsole.Ask<string>("Inserisci un intero compreso tra 1 e 30:");
            }
        break;
        //.. Altrimenti a 31
        default:
            while (!int.TryParse(g, out giorno) || giorno < 1 || giorno > 31)
            {
                g = AnsiConsole.Ask<string>("Inserisci un intero compreso tra 1 e 31:");
            }
        break;
    }
}

Convert.ToString(giorno);

//Viene concatenata la stringa della data di nascita con i valori validati

d += $"{giorno}{mese}{anno}";

//Viene effettuato uno scorrimento di tutti i caratteri del file

for (int i = 0; i < content.Length; i++)
{
    //A ogni carattere viene concatenata una stringa di tanti caratteri consecutivi quanti quelli della data di nascita
    string dp = "";

    for (int j = i; j < i + d.Length; j++)
    {
        dp += $"{content[j]}";
    }
//Se la stringa combacia con quella della data di nascita viene stampata la relativa posizione nell'array e quindi nel numero del pi greco
    if (dp == d)
    {
        Console.WriteLine($"{d} trovata in {i + 1}a posizione !");
        break;
    }
}