namespace RecommenderSystem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Test.Execute(Test.Size.SMALL, 20, 2, 0.01, false);
            Test.Execute(Test.Size.SMALL, 20, 2, 0.1, false);
            Test.Execute(Test.Size.SMALL, 20, 2, 0.5, false);
            Test.Execute(Test.Size.SMALL, 20, 2, 1, false);

            Test.Execute(Test.Size.MEDIUM, 15, 2, 0.01, false);
            Test.Execute(Test.Size.MEDIUM, 15, 2, 0.1, false);
            Test.Execute(Test.Size.MEDIUM, 15, 2, 0.5, false);
            Test.Execute(Test.Size.MEDIUM, 15, 2, 1, false);

            Test.Execute(Test.Size.LARGE, 10, 2, 0.01, false);
            Test.Execute(Test.Size.LARGE, 10, 2, 0.1, false);
            Test.Execute(Test.Size.LARGE, 10, 2, 0.5, false);
            Test.Execute(Test.Size.LARGE, 10, 2, 1, false);
        }
    }
}
