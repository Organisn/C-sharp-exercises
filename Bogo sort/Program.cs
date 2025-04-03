using Spectre.Console;
Random rnd = new Random(); // Randomizer

// Poiché la riuscita dell'algoritmo non è garantita, esso viene iterato un numero finito di volte a scelta dell'utente
Console.Write("Quanti bogo tentativi? ");
int n;
while (!int.TryParse(Console.ReadLine(), out n) || n < 1)
{
    Console.Write("Almeno una volta :*( : ");
}

int[] a = new int[16]; //Array da ordinare
string va = ""; //Stringa per la visualizzazione dell'array da ordinare
int[] b = new int[16]; //Array di appoggio per i tentativi di ordinamento
for (int i = 0; i < a.Length; i++)
{
    a[i] = rnd.Next(1, 101); //Array occupato da cifre generate randomicamente tra 1 e 100
    b[i] = a[i];
    va += $"{a[i]} "; //Riempimento della stringa di visualizzazione
}

int t = 1; // tentativi di ordinamento
while (true) {
    // Interruzione del processo con "ESC"
    Console.WriteLine("Premi ESC per interrompere il tentativo...");
    do {
        while (!Console.KeyAvailable &&
            t <= n &&
            (b[0] > b[1] || 
            b[1] > b[2] || 
            b[2] > b[3] || 
            b[3] > b[4] || 
            b[4] > b[5] || 
            b[5] > b[6] || 
            b[6] > b[7] || 
            b[7] > b[8] || 
            b[8] > b[9] || 
            b[9] > b[10] || 
            b[10] > b[11] || 
            b[11] > b[12] || 
            b[12] > b[13] || 
            b[13] > b[14] || 
            b[14] > b[15])) {
            //A ogni tentativo di ordinamento..
            string vb = ""; //.. viene creata una stringa per la visualizzazione..

            var table = new Table(); //.. una tabella..

            //.. viene aggiunta una riga contenente l'array da ordinare..
            table.AddColumn(new TableColumn($"{va}").Centered()); 
            table.AddColumn(new TableColumn("Array da ordinare").Centered());

            //.. viene azzerato l'array per i tentativi..
            for (int i = 0; i < b.Length; i++) b[i] = 0;

            int p; // indice array
            //.. per ogni elemento dell'array da ordinare..
            for (int i = 0; i < a.Length; i++)
            {
                do
                {
                    p = rnd.Next(0, 16); //.. viene individuata randomicamente una posizione di "b" da valorizzare con esso..
                } while (b[p] != 0);
                b[p] = a[i];
            }

            //.. viene riempita la stringa di visualizzazione
            for (int i = 0; i < b.Length; i++) vb += $"{b[i]} ";

            table.AddRow($"{vb}", $"Tentativo ordinamento n°{t}"); //.. viene aggiunta una riga contenente il tentativo di ordinamento

            AnsiConsole.Write(table);

            t++;
        }       
    } while (t <= n && Console.ReadKey(true).Key != ConsoleKey.Escape);

    //Se desiderato è possibile continuare
    if (!AnsiConsole.Confirm("Continuare ?")) break;
    else {   
        int i;
        Console.Write("Quanti tentativi ancora ? ");
        while (!int.TryParse(Console.ReadLine(), out i) || i < 1) Console.Write("Almeno un'altra volta :*( : ");
        t = n;
        n += i;
    }
}