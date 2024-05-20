using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the path to the input file:");
        string filePath = Console.ReadLine();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        List<(string, int)> people = new List<(string, int)>();

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length >= 2)
                    {
                        string name = parts[0];
                        int age;
                        if (int.TryParse(parts[1], out age))
                        {
                            people.Add((name, age));
                        }
                        else
                        {
                            Console.WriteLine($"Invalid age format in line: {line}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid format in line: {line}");
                    }
                }
            }

            if (people.Count == 0)
            {
                Console.WriteLine("No valid data found in the file.");
                return;
            }

            Random random = new Random();
            var selectedPerson = people[random.Next(people.Count)];
            string selectedName = selectedPerson.Item1;
            int selectedAge = selectedPerson.Item2;

            Console.WriteLine($"Welcome to the Guess the Age Game! Guess the age of {selectedName}.");

            int attempts = 0;
            int guess;

            do
            {
                Console.Write("Enter your guess: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out guess))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                attempts++;

                if (guess < selectedAge)
                {
                    Console.WriteLine("Too low! Try again.");
                }
                else if (guess > selectedAge)
                {
                    Console.WriteLine("Too high! Try again.");
                }
                else
                {
                    Console.WriteLine($"Congratulations! You guessed it right. {selectedName} is {selectedAge} years old.");
                    Console.WriteLine($"Number of attempts: {attempts}");
                }

            } while (guess != selectedAge);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
  
