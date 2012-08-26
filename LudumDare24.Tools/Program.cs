using System;

namespace LudumDare24.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            EvaluateBoard boardEvaluator = new EvaluateBoard();
            for (int i = 0; i < args.Length; i++)
            {
                Console.Write("{0}; ", args[i]);
                boardEvaluator.ProcessRecord(args[i]);
            }
        }
    }
}
