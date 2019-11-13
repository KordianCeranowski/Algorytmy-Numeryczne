using System;
using System.Numerics;

namespace Gauss
{
    [Serializable]
    class MyType
    {
        public BigInteger Numerator;
        public BigInteger Denominator;

        public MyType() 
        {
            this.Numerator = new BigInteger(0);
            this.Denominator = new BigInteger(1);
        }

        public MyType(BigInteger numerator, BigInteger denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
            Simplify();
        }

        public MyType(int numerator, int denominator)
        {
            this.Numerator = new BigInteger(numerator);
            this.Denominator = new BigInteger(denominator);
            Simplify();
        }

        // Pilnuje poprawności znaku
        // Upraszcza wartości dzieląc przez GCD aka NWD
        private void Simplify()
        {
            if (Denominator.Sign < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }

            BigInteger gcd = BigInteger.GreatestCommonDivisor(Numerator, Denominator);
           
            Numerator /= gcd;
            Denominator /= gcd;
        }

        private void FastSimplify() 
        {
            if (Denominator.Sign < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }
        }


        // Mnożenie
        public static MyType operator *(MyType left, MyType right)
        {
            return new MyType(
                left.Numerator * right.Numerator,
                left.Denominator * right.Denominator
                );
        }

        // Dzielenie
        public static MyType operator /(MyType left, MyType right)
        {
            return new MyType(
                left.Numerator * right.Denominator,
                left.Denominator * right.Numerator
                );
        }

        // Dodawanie
        public static MyType operator +(MyType left, MyType right)
        {
            BigInteger gcd = BigInteger.GreatestCommonDivisor(left.Denominator, right.Denominator);

            BigInteger numerator =
                left.Numerator * (right.Denominator / gcd) +
                right.Numerator * (left.Denominator / gcd);
            BigInteger denominator =
                left.Denominator * (right.Denominator / gcd);
            return new MyType(numerator, denominator);
        }

        // Odejmowanie
        public static MyType operator -(MyType left, MyType right) 
        {
            MyType temp = new MyType(-(right.Numerator), right.Denominator);
            return (left + temp);
        }

        public static bool operator >(MyType left, MyType right)
        {
            BigInteger gcd = BigInteger.GreatestCommonDivisor(left.Denominator, right.Denominator);

            return (left.Numerator * (right.Denominator / gcd)) > (right.Numerator * (left.Denominator / gcd));
        }
        public static bool operator <(MyType left, MyType right)
        {
            BigInteger gcd = BigInteger.GreatestCommonDivisor(left.Denominator, right.Denominator);

            return (left.Numerator * (right.Denominator / gcd)) < (right.Numerator * (left.Denominator / gcd));
        }

        public static bool operator ==(MyType left, MyType right)
        {
            BigInteger gcd = BigInteger.GreatestCommonDivisor(left.Denominator, right.Denominator);
            return (left.Numerator * (right.Denominator / gcd)) == (right.Numerator * (left.Denominator / gcd));
        }
        public static bool operator !=(MyType left, MyType right)
        {
            return !(left == right);
        }




        // castowanie na decimal do liczenia Normy
        public static explicit operator double(MyType x)
        {
            return ((double)x.Numerator / (double)x.Denominator);
        }



        // Equals i HashCode wygenerowane
        public override bool Equals(object obj)
        {
            return obj is MyType type &&
                   Numerator.Equals(type.Numerator) &&
                   Denominator.Equals(type.Denominator);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }


        public override string ToString()
        {
            return this.Numerator.ToString() + "/" + this.Denominator.ToString();
        }

    }
}
