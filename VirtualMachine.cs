using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        private Dictionary<char, Action<IVirtualMachine>> register;

        public VirtualMachine(string program, int memorySize)
        {
            register = new Dictionary<char, Action<IVirtualMachine>>();
            Instructions = program;
            Memory = new byte[memorySize];
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            register[symbol] = execute;
        }

        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }
        public void Run()
        {
            while (InstructionPointer < Instructions.Length)
            {
                var instruction = Instructions[InstructionPointer];
                if (register.ContainsKey(instruction))
                    register[instruction].Invoke(this);

                InstructionPointer++;
            }
        }
    }
}