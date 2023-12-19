using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

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
            Fauna.Creatures.Add(new Creature(creatureId, color, type));
            Fauna.UnscannedCreatures.Add(new Creature(creatureId, color, type));
        }

        User me = new User();
        User foe = new User();

        while (true)
        {
            int myScore = int.Parse(Console.ReadLine());
            int foeScore = int.Parse(Console.ReadLine());
            int myScanCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < myScanCount; i++)
            {
                int creatureId = int.Parse(Console.ReadLine());
                if (!me.Bestiary.Creatures.Any(c => c.Id == creatureId))
                {
                    me.Bestiary.Creatures.Add(Fauna.Creatures.Where(c => c.Id == creatureId).Single());
                }
                if (Fauna.UnscannedCreatures.Any(c => c.Id == creatureId))
                {
                    Fauna.UnscannedCreatures.Remove(Fauna.UnscannedCreatures.Where(c => c.Id == creatureId).Single());
                }
            }

            foreach (var creature in Fauna.UnscannedCreatures)
            {
                Console.Error.WriteLine($"{creature.Id}: {creature.X} {creature.Y} {creature.VelX} {creature.VelY} {creature.GetNextPos()}");
            }

            int foeScanCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < foeScanCount; i++)
            {
                int creatureId = int.Parse(Console.ReadLine());
                if (!foe.Bestiary.Creatures.Any(c => c.Id == creatureId))
                {
                    foe.Bestiary.Creatures.Add(Fauna.Creatures.Where(c => c.Id == creatureId).Single());
                }
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
                
                var drone = me.Drones.Where(d => d.Id == droneId).SingleOrDefault();
                if (drone == null)
                {
                    drone = new Drone(droneId);
                    me.Drones.Add(drone);
                }
                drone.SetBattery(battery);
                drone.SetPosition(droneX, droneY);
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

                var drone = foe.Drones.Where(d => d.Id == droneId).SingleOrDefault();
                if (drone == null)
                {
                    drone = new Drone(droneId);
                    foe.Drones.Add(drone);
                }
                drone.SetBattery(battery);
                drone.SetPosition(droneX, droneY);
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
                Fauna.Creatures.Where(c => c.Id == creatureId).Single().SetPosAndVelocity(creatureX, creatureY, creatureVx, creatureVy);
                Fauna.UnscannedCreatures.Where(c => c.Id == creatureId).SingleOrDefault()?.SetPosAndVelocity(creatureX, creatureY, creatureVx, creatureVy);
            }
            int radarBlipCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < radarBlipCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int droneId = int.Parse(inputs[0]);
                int creatureId = int.Parse(inputs[1]);
                string radar = inputs[2];
            }

            foreach (var decision in Decision.Execute(me))
            {
                Console.WriteLine(decision);
            }
        }
    }
}

public static class Decision
{
    public static List<string> Execute(User user)
    {
        var decisions = new List<string>();
        foreach (var drone in user.Drones)
        {
            var target = drone.GetNearestCreature();
            var command = MoveToTarget(target.GetNextPos());
            command += $" {DroneBatteryDecision(drone)}";
            decisions.Add(command);
        }

        return decisions;
    }

    private static string MoveToTarget(Point target)
    {
        return $"MOVE {target.X} {target.Y}";
    }

    private static string MoveToSurface(Drone drone)
    {
        return $"MOVE {drone.X} 0";
    }

    private static string DroneBatteryDecision(Drone drone)
    {
        Console.Error.WriteLine("Battery is " + drone.Battery);
        return drone.Battery > 25
            || drone.Battery == 23
            || drone.Battery == 17
            || drone.Battery == 11
            || drone.Battery == 5
            ? "1" : "0";
    }
}

public class User
{
    public List<Drone> Drones = new List<Drone>();
    public int Score = 0;
    public Bestiary Bestiary = new Bestiary();
}

public class Drone : Point
{
    public int Id { get; set; }
    public int Battery { get; set; }

    public Drone(int id) : base(0, 0)
    {
        Id = id;
    }

    public void SetBattery(int battery)
    {
        Battery = battery;
    }

    public Creature GetNearestCreature()
    {
        return Fauna.UnscannedCreatures.OrderBy(c => SquareDistanceTo(c)).First();
    }
}

public static class Fauna
{
    public static List<Creature> Creatures = new List<Creature>();
    public static List<Creature> UnscannedCreatures = new List<Creature>();
}

public class Bestiary
{
    public List<Creature> Creatures = new List<Creature>();
}

public class Creature : Point
{
    public int Id { get; set; }
    public int Color { get; set; }
    public int Type { get; set; }
    public int VelX { get; set; }
    public int VelY { get; set; }

    public Creature(int id, 
        int color, int type) : base(0, 0)
    {
        Id = id;
        Color = color;
        Type = type;
    }

    public void SetPosAndVelocity(int x, int y, int vx, int vy)
    {
        SetPosition(x, y);
        VelX= vx;
        VelX= vy;
    }

    public Point GetNextPos()
    {
        return new Point(X + VelX, Y + VelY);
    }

    public int GetValue()
    {
        return Type+1;
    }
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetPosition(int x, int y)
    {
        X = x; 
        Y = y;
    }

    public double DistanceTo(Point dest)
    {
        return Math.Sqrt(SquareDistanceTo(dest));
    }

    public int SquareDistanceTo(Point dest)
    {
        int deltaX = dest.X - X;
        int deltaY = dest.Y - Y;

        return deltaX * deltaX + deltaY * deltaY;
    }
}