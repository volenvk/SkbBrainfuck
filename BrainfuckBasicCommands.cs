using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            for (int i = 48; i < 123; i++)
            {
                var symbol = (char)i;
                vm.RegisterCommand(symbol, b => b.Memory[b.MemoryPointer] = (byte)symbol);
                if (i == 57) i = 64;
                if (i == 90) i = 96;
            }
            vm.RegisterCommand('.', b => write((char)b.Memory[b.MemoryPointer]));
            vm.RegisterCommand(',', b =>
            {
                var symbol = read();
                b.Memory[b.MemoryPointer] = symbol > 0 ? (byte)symbol : (byte)0;
            });
            vm.RegisterCommand('>', b =>
            {
                if (b.MemoryPointer + 1 < b.Memory.Length) b.MemoryPointer++;
                else b.MemoryPointer = 0;
            });
            vm.RegisterCommand('<', b =>
            {
                if (b.MemoryPointer - 1 >= 0) b.MemoryPointer--;
                else b.MemoryPointer = b.Memory.Length - 1;
            });
            vm.RegisterCommand('+', b =>
            {
                if (b.Memory[b.MemoryPointer] < byte.MaxValue) b.Memory[b.MemoryPointer]++;
                else b.Memory[b.MemoryPointer] = byte.MinValue;
            });
            vm.RegisterCommand('-', b =>
            {
                if (b.Memory[b.MemoryPointer] > byte.MinValue) b.Memory[b.MemoryPointer]--;
                else b.Memory[b.MemoryPointer] = byte.MaxValue;
            });
        }
    }
}