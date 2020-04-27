using System;

namespace NullReferenceConsole
{
    struct TestStruct
    {
        public int age;
        public string name;

        public TestStruct(int a, string n)
        {
            age = a;
            name = n;
        }
    }

    enum TestEnum
    {
        A,
        B,
        C
    }
    class Program
    {

        private readonly string _name;


        /// <summary>
        /// ctor
        /// </summary>
        /// <exception cref="System.IO.IOException"></exception>
        public Program()
        {
            _name = "sss";
            //_name只能在这里初始化，

            testFun(_name);
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <exception cref="System.IO.IOException"></exception>
        public void Test()
        {
            testFun(_name);
        }

        //public Program()
        //{
        //    Initialize();
        //}

        //private void Initialize()
        //{
        //    _name = "xx";
        //}

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="MethodAccessException"></exception>
        /// <exception cref="MemberAccessException"></exception>
        /// <exception cref="System.Runtime.InteropServices.InvalidComObjectException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="System.Runtime.InteropServices.COMException"></exception>
        /// <exception cref="TypeLoadException"></exception>
        static void Main(string[] args)
        {
            string dd = "safasf";

            byte[] bytes = new byte[0];

            string[] vs = new string[0];



            dd.IsIn(null);

            "xx".IsIn(null);
            
        }

        /// <summary>
        /// testFun
        /// </summary>
        /// <param name="str"></param>
        /// <exception cref="System.IO.IOException"></exception>
        static void testFun(string str)
        {
            Console.WriteLine(str.Length);
        }

        
    }

    public static class Extensions
    {
        public static bool IsIn(this string? str, params string?[]? words)
        {

 

            return false;
        }
    }
}
