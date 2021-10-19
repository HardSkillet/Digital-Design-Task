using ClassHomework;

namespace WarAndPiece
{
    class Program
    {
        public static void Main()
        {
            for (int i = 1; i < 5; ++i)
            {
                Homework temp = new Homework("Tom" + i.ToString() + ".txt");
            }
            Homework.GetAnswer();
        }
        
    }
}
