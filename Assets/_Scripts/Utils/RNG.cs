using Random = System.Random;

namespace _Scripts.Utils
{
    public static class RNG
    {
        private static readonly Random Rnd = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return Rnd.Next(min, max);
        }
    }
}