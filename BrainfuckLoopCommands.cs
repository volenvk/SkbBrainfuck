using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
		    var brackets = GetBrackets(vm.Instructions);

            vm.RegisterCommand('[', b =>
			{
			    if (b.Memory[b.MemoryPointer] == 0) b.InstructionPointer = brackets[b.InstructionPointer];
            });
			vm.RegisterCommand(']', b =>
			{
			    if (b.Memory[b.MemoryPointer] != 0) b.InstructionPointer = brackets[b.InstructionPointer];
            });
		}

	    private static Dictionary<int, int> GetBrackets(string instructions)
	    {
	        var brackets = new Dictionary<int, int>();
	        var openBracket = new Stack<int>();
	        for (int i = 0; i < instructions.Length; i++)
	        {
	            switch (instructions[i])
	            {
	                case '[':
	                    openBracket.Push(i);
	                    brackets[i] = -1;
	                    continue;
	                case ']':
	                    var open = openBracket.Pop();
	                    brackets[i] = open;
	                    brackets[open] = i;
	                    continue;
	            }
	        }

	        return brackets;
	    }
    }
}