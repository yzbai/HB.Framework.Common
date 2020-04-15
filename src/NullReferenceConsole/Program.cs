using System;

namespace NullReferenceConsole
{
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
        static void Main(string[] args)
        {
            

            Console.WriteLine("Hello World!");
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
}
