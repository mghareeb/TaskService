using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TaskService.ServiceReference1;

namespace TaskService
{
    [ServiceBehavior(Name = "RedPill", Namespace = "http://KnockKnock.readify.net")]
    public class RedPill : IRedPill
    {
        #region Constants

        //My Token
        private readonly Guid READIFY_TOKEN = new Guid("f4d6f64e-7dd5-4139-98a6-5f774955842c");

        //Fibonacci Constants
        private const int MAX_FIB_INDEX = 92;
        private const int MIN_FIB_INDEX = -92;

        #endregion

        #region Public Methods

        public Guid WhatIsYourToken()
        {
            return  this.READIFY_TOKEN;
        }

        public long FibonacciNumber(long n)
        {
            if (n < MIN_FIB_INDEX || n > MAX_FIB_INDEX)
            {
                throw new ArgumentOutOfRangeException("n", "param (n) is out of range");
            }
            if (n == 0) return 0;
            long a = 0;
            long b = 1;
            var index = Math.Abs(n);
            for (var i = 0; i < index; i++)
            {
                var temp = a;
                a = b;
                b += temp;
            }
            return n < 0 && index % 2 == 0 ? -a : a;
        }


        public TriangleType WhatShapeIsThis(int a, int b, int c)
        {
            if (!IsValidTriangle(a, b, c))
            {
                return TriangleType.Error;
            }

            if (a == b && a == c && b == c)
            {
                return TriangleType.Equilateral;
            }

            if (a == b || a == c || b == c)
            {
                return TriangleType.Isosceles;
            }

            return TriangleType.Scalene;
        }

        public string ReverseWords(string s)
        {
            if (s == null)
                throw new ArgumentNullException("s", "param (s) cannot be null");
            var input = s;
            char[] output = new char[input.Length];
            int written = 0;
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (char.IsWhiteSpace(c) || i == input.Length - 1)
                {
                    endIndex = char.IsWhiteSpace(c) ? i - 1 : i;
                    for (int j = endIndex; j >= startIndex; j--)
                    {
                        output[written++] = input[j];
                    }
                    startIndex = i + 1;
                    if (char.IsWhiteSpace(c))
                        output[written++] = c;
                }
            }
            return new string(output);
        }

        #endregion

        #region Private Methods

        private bool IsValidTriangle(int a, int b, int c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                return false;
            if (a < b + c && b < a + c && c < a + b)
                return true;
            return false;
        }

        #endregion
    }
}
