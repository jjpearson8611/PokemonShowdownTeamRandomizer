using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonShowdownTourneyTeamGenerator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            Dictionary<int, string> pokeInventory = new Dictionary<int, string>();
            Random myRand = new Random();

            if (File.Exists("PokemonList.txt"))
            {
                // Read file using StreamReader. Reads file line by line
                using (StreamReader file = new StreamReader("PokemonList.txt"))
                {
                    int counter = -1;
                    string ln;
                    string tempPokemon = "";

                    while ((ln = file.ReadLine()) != null)
                    {
                        if (ln.Length <= 1)
                        {
                            counter++;
                            pokeInventory.Add(counter, tempPokemon);
                            tempPokemon = "";
                        }
                        else
                        {
                            tempPokemon += ln + Environment.NewLine;
                        }
                    }
                    file.Close();
                }
            }

            Console.WriteLine("Total of " + pokeInventory.Count + " Pokemon to choose from!!");

            int Teams = 0;
            Console.WriteLine("How Many Teams would you like?");

            if (int.TryParse(Console.ReadLine(), out Teams))
            {
                if (Teams * 6 > pokeInventory.Count)
                {
                    Console.WriteLine("Need a bigger database to make these teams fun");
                }
                else
                {

                    for(int i = 0; i < Teams; i++)
                    {
                        HashSet<int> claimedMons = new HashSet<int>();
                        Console.WriteLine("Generating Team " + (i + 1));
                        
                        string myTeam = "";

                        for(int j = 0; j < 6; j++)
                        {
                            int myNum = myRand.Next(0, pokeInventory.Count - 1);
                            while(claimedMons.Contains(myNum))
                            {
                                myNum = (myNum + 1) % (pokeInventory.Count - 1);
                            }

                            claimedMons.Add(myNum);

                            myTeam += pokeInventory[myNum] + Environment.NewLine;
                        }

                        Clipboard.SetText(myTeam);
                        Console.WriteLine("Team " + (i + 1) + " has been copied to your clipboard");
                        Console.WriteLine("Press enter to get next team");
                        Console.ReadLine();
                        Console.Clear();
                    }

                }
            }
            else
            {
                Console.WriteLine("Enter an integer number of teams next time plz");
            }

            Console.WriteLine("Press Enter to exit the program");
            Console.ReadLine();
        }
    }
}
