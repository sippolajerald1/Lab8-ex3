using System;

namespace delegatesAndEvents
{
    public delegate void Notify(int n, int laps);// create a delegate


    public class Race
    {
        public int laps;

        public event Notify ?ContestEnd; // create a delegate event object

        public void Racing(int contestants, int laps)
        {
            Console.WriteLine("Ready\nSet\nGo!");
            Random r = new Random();
            int[] participants = new int[contestants];
            bool done = false;
            int champ = -1;
            // first to finish specified number of laps wins
            while (!done)
            {
                for (int i = 0; i < contestants; i++)
                {

                    if (participants[i] <= laps)
                    {
                        participants[i] += r.Next(1, 5);
                    }
                    else
                    {
                        champ = i;
                        done = true;
                        continue;
                    }
                }

            }

            TheWinner(champ);
        }
        private void TheWinner(int champ)
        {
            Console.WriteLine("We have a winner!");
            ContestEnd(champ, laps);       // invoke the delegate event object and pass champ to the method

        }
    }
    class Program
    {
        public static void Main()
        {
            // create a class object
            Race round1 = new Race();
            // register with the footRace event
            round1.ContestEnd += footRace;
            // trigger the event
            round1.Racing(20, 30);
            // register with the carRace event
            round1.ContestEnd -= footRace;
            round1.ContestEnd += carRace;
            //trigger the event
            round1.Racing(25, 50);
            // register a bike race event using a lambda expression
            round1.ContestEnd -= carRace;
            round1.ContestEnd += (winner, laps) => Console.WriteLine($"The bike competition winner is {winner}");
            // trigger the event
            round1.Racing(25, 35);

        }
        
        // event handlers
        public static void carRace(int winner, int v)
        {
            Console.WriteLine($"Car number {winner} is the winner.");
        }
        public static void footRace(int winner, int v)
        {
            Console.WriteLine($"Foot racer number {winner} is the winner.");
        }
    }
}
