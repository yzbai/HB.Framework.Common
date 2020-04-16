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

            object? structObj = Activator.CreateInstance(typeof(TestStruct));

            TestStruct testStruct = (TestStruct)structObj!;

            Console.WriteLine(testStruct.age);
            Console.WriteLine(testStruct.name);

            int number = 21;

            Type type = number.GetType();

            if (type.IsValueType)
            {
                object? obj = Activator.CreateInstance(type);

                Type type1 = obj!.GetType();

                int? count = 10;

                Type type2 = count.GetType();

            }

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
