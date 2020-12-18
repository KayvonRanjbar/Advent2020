using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent2020
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines("Instructions.txt");

            RunInstructionsPartOne(instructions);

            RunInstructionsPartTwo(instructions);
        }
        
        private static void RunInstructionsPartTwo(string[] instructions)
        {
            for (int i = 0; i < instructions.Length; i++)
            {
                string[] modifiedInstructions = new string[instructions.Length];
                instructions.CopyTo(modifiedInstructions, 0);
                if (instructions[i].Contains("jmp"))
                {
                    modifiedInstructions[i] = modifiedInstructions[i].Replace("jmp", "nop");
                }
                else if (instructions[i].Contains("nop"))
                {
                    modifiedInstructions[i] = modifiedInstructions[i].Replace("nop", "jmp");
                }
                else
                {
                    continue;
                }
                
                int accumulator = 0;
                int instructionNumber = 0;
                int instructionNumberImmediatelyAfterLastInstruction = modifiedInstructions.Length;
                int iteration = 0;
                
                while (instructionNumber != instructionNumberImmediatelyAfterLastInstruction)
                {
                    if (instructionNumber > modifiedInstructions.Length)
                    {
                        throw new Exception($"Invalid instruction number: {instructionNumber}");
                    }

                    if (iteration > modifiedInstructions.Length)
                    {
                        break;
                    }
                
                    string instruction = modifiedInstructions[instructionNumber];

                    RunInstruction(instruction, out int changeInAccumulator, out int changeInInstructionNumber);

                    iteration++;
                    accumulator += changeInAccumulator;
                    instructionNumber += changeInInstructionNumber;
                }

                if (instructionNumber == instructionNumberImmediatelyAfterLastInstruction)
                {
                    Console.WriteLine(accumulator);
                    break;
                }
            }
        }

        private static void RunInstructionsPartOne(string[] instructions)
        {
            int accumulator = 0;
            int instructionNumber = 0;
            
            List<int> runInstructionNumbers = new List<int>();

            while (!runInstructionNumbers.Contains(instructionNumber))
            {
                runInstructionNumbers.Add(instructionNumber);
                
                string instruction = instructions[instructionNumber];

                RunInstruction(instruction, out int changeInAccumulator, out int changeInInstructionNumber);

                accumulator += changeInAccumulator;
                instructionNumber += changeInInstructionNumber;
            }

            Console.WriteLine(accumulator);
        }

        private static void RunInstruction(string instruction, out int changeInAccumulator, out int changeInInstructionNumber)
        {
            string operation = instruction.Split(' ')[0];
            int argument = int.Parse(instruction.Split(' ')[1]);

            switch (operation)
            {
                case "acc":
                {
                    changeInAccumulator = argument;
                    changeInInstructionNumber = 1;
                    break;
                }

                case "jmp":
                {
                    changeInAccumulator = 0;
                    changeInInstructionNumber = argument;
                    break;
                }

                case "nop":
                {
                    changeInAccumulator = 0;
                    changeInInstructionNumber = 1;
                    break;
                }

                default:
                {
                    throw new Exception($"Invalid operation: {operation}");
                }
            }
        }
    }
}