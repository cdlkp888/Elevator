using System;
using System.Threading;

namespace Elevators
{

	class Program
	{
		public class Elevator
		{
			//Defaults and Declarations
			//building has n floors

			private bool[] floorReady;
			public sbyte CurrentFloor = 1;
			private sbyte topfloor;
			private sbyte bottomfloor;
			public ElevatorStatus Status = ElevatorStatus.STOPPED;

			public Elevator(sbyte NumberOfFloors = 10)
			{
				floorReady = new bool[NumberOfFloors + 1];
				topfloor = NumberOfFloors;
			}

			private void Stop(sbyte floor)
			{
				Status = ElevatorStatus.STOPPED;
				CurrentFloor = floor;
				floorReady[floor] = false;
				Console.WriteLine("Stopped at floor {0}", floor);
			}

			private void Descend(sbyte floor)
			{
				Console.WriteLine("Elevator going down");
				for (sbyte i = CurrentFloor; i >= 1; i--)
				{
					if (floorReady[i])
						Stop(floor);
					else
						continue;
				}
				Status = ElevatorStatus.STOPPED;
				Console.WriteLine("Waiting..");
			}

			private void Ascend(sbyte floor)
			{
				Console.WriteLine("Elevator going up");
				for (sbyte i = CurrentFloor; i <= topfloor; i++)
				{
					if (floorReady[i])
						Stop(floor);
					else
						continue;
				}
				Status = ElevatorStatus.STOPPED;
				Console.WriteLine("Waiting..");
			}

			void StayPut()
			{
				Console.WriteLine("That's our current floor");
			}

			public void FloorPress(sbyte floor)
			{
				if (floor > topfloor)
				{
					Console.WriteLine("We only have {0} floors", topfloor);
					return;
				}

				floorReady[floor] = true;

				switch (Status)
				{

					case ElevatorStatus.DOWN:
						Descend(floor);
						break;

					case ElevatorStatus.STOPPED:
						if (CurrentFloor < floor)
							Ascend(floor);
						else if (CurrentFloor == floor)
							StayPut();
						else
							Descend(floor);
						break;

					case ElevatorStatus.UP:
						Ascend(floor);
						break;

					default:
						break;
				}
			}

			public enum ElevatorStatus
			{
				UP,
				STOPPED,
				DOWN
			}
			private const string QUIT = "q";

			static void Main(string[] args)
			{

			Start:
				Console.WriteLine("Welcome to Curt's elevator!!");

				sbyte floor; string floorInput; Elevator elevator;

				floorInput = "10";

				if (sbyte.TryParse(floorInput, out floor))
					elevator = new Elevator(floor);
				else
				{
					Console.WriteLine("That' doesn't make sense...");
					Console.Beep();
					Thread.Sleep(2000);
					Console.Clear();
					goto Start;
				}
				string input = string.Empty;

				while (input != QUIT)
				{
					Console.WriteLine("Please input which floor you would like to go to");

					input = Console.ReadLine();
					if (sbyte.TryParse(input, out floor))
						elevator.FloorPress(floor);
					else if (input == QUIT)
						Console.WriteLine("GoodBye!");
					else
						Console.WriteLine("You have entered an incorrect floor, Please try again");
				}
			}
		}
	}
}

