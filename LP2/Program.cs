namespace LP2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[,] a = new double[,] { { 4,1,0}, {-4,-3,-6 },{ 1,2,4} };
            double[] answer = SimplexMethod.GetAnswer(a, Sign.Min,new int[] {1,2}, new int[] {3,4} );

            for(int i = 0; i < answer.Length; i++) { Console.WriteLine(answer[i]); }
        }
    }
}
