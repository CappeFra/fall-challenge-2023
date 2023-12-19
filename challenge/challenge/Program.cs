using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Score points by scanning valuable fish faster than your opponent.
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        int creatureCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < creatureCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int creatureId = int.Parse(inputs[0]);
            int color = int.Parse(inputs[1]);
            int type = int.Parse(inputs[2]);
        }

        // game loop
        while (true)
        {
            int myScore = int.Parse(Console.ReadLine());
            int foeScore = int.Parse(Console.ReadLine());
            int myScanCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < myScanCount; i++)
            {
                int creatureId = int.Parse(Console.ReadLine());
            }
            int foeScanCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < foeScanCount; i++)
            {
                int creatureId = int.Parse(Console.ReadLine());
            }
            int myDroneCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < myDroneCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int droneId = int.Parse(inputs[0]);
                int droneX = int.Parse(inputs[1]);
                int droneY = int.Parse(inputs[2]);
                int emergency = int.Parse(inputs[3]);
                int battery = int.Parse(inputs[4]);
            }
            int foeDroneCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < foeDroneCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int droneId = int.Parse(inputs[0]);
                int droneX = int.Parse(inputs[1]);
                int droneY = int.Parse(inputs[2]);
                int emergency = int.Parse(inputs[3]);
                int battery = int.Parse(inputs[4]);
            }
            int droneScanCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < droneScanCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int droneId = int.Parse(inputs[0]);
                int creatureId = int.Parse(inputs[1]);
            }
            int visibleCreatureCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < visibleCreatureCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int creatureId = int.Parse(inputs[0]);
                int creatureX = int.Parse(inputs[1]);
                int creatureY = int.Parse(inputs[2]);
                int creatureVx = int.Parse(inputs[3]);
                int creatureVy = int.Parse(inputs[4]);
            }
            int radarBlipCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < radarBlipCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int droneId = int.Parse(inputs[0]);
                int creatureId = int.Parse(inputs[1]);
                string radar = inputs[2];
            }
            for (int i = 0; i < myDroneCount; i++)
            {

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                Console.WriteLine("WAIT 1"); // MOVE <x> <y> <light (1|0)> | WAIT <light (1|0)>

            }
        }
    }
}