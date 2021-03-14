namespace Logic
{
    public class ExpressionTree
    {
        
        private static decimal Factorial(decimal n)
        {
            var j = 1;
            for (var i = 1; i <= n; i++)
            {
                j *= i;
            }

            return j;
        }

        /// <summary>
        /// http://stackoverflow.com/a/466434/848344
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static decimal Pow(decimal x, decimal y)
        {
            return DecimalExp(y * LogN(x));
        }

        private static decimal Exponentiation(decimal a, decimal b)
        {
            var total = a;
            for (var i = 1; i < b; i++) total = a * total;
            return total;
        }

        // Adjust this to modify the precision
        private const int Iterations = 27;

        /// <summary>
        /// Power series
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        private static decimal DecimalExp(decimal power)
        {
            var iteration = Iterations;
            decimal result = 1;
            while (iteration > 0)
            {
                var factorial = Factorial(iteration);
                result += Exponentiation(power, iteration) / factorial;
                iteration--;
            }

            return result;
        }

        /// <summary>
        /// Natural logarithm series
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static decimal LogN(decimal number)
        {
            var aux = number - 1;
            decimal result = 0;
            var iteration = Iterations;
            while (iteration > 0)
            {
                result += Exponentiation(aux, iteration) / iteration;
                iteration--;
            }

            return result;
        }
    }
}