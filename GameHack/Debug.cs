using System;
using System.Text;

namespace GameHack
{
    public static class Debug
    {
        internal static string parseText(string text, int len = 40)
        {
            if (text.Length > len)
            {
                text = text.Substring(0, len - 3) + "...";
            }
            else
            {
                text = text.PadRight(len, ' ');
            }
            var tb = Encoding.Default.GetBytes(text);
            var bytes = new byte[len];
            Array.Copy(tb, bytes, len);
            return Encoding.Default.GetString(bytes);

        }
        public static void logError(string text, ConsoleColor co = ConsoleColor.White)
        {
            Console.ForegroundColor = co;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void Log(string text, ConsoleColor co = ConsoleColor.White, string pex = "")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("||");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(pex);
            Console.ResetColor();
            Console.ForegroundColor = co;
            var len = pex != "" ? 37 : 40;
            var p = parseText(text, len);
            Console.Write(p);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("||");
            Console.ResetColor();
        }
    }
}
