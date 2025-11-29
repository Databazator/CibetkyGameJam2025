using Random = System.Random;

namespace _Scripts.Utils
{
    public static class RNG
    {
        private static readonly Random Rnd = new Random();

        public static int GetRandomInt(int min, int max)
        {
            return Rnd.Next(min, max);
        }
        
        public static float GetRandomFloat()
        {
            return (float)Rnd.NextDouble();
        }
    }
}