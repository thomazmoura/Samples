using System;

namespace ImplicitInstantiationGotcha
{
    class Program
    {
        static void Main(string[] args)
        {
            var workingInitialization = new ParentClass
            {
                Property = new PropertyClass
                {
                    TestValue = "Works Nicely"
                }

            };
            Console.WriteLine(workingInitialization.Property.TestValue);

            ParentClass brokenInitialization = new ParentClass
            {
                Property = {
                    TestValue = "Should Work"
                }
            };

            Console.WriteLine(brokenInitialization.Property.TestValue);
        }
    }

    class ParentClass
    {
        public PropertyClass Property { get; set; } = new PropertyClass();
    }

    class PropertyClass
    {
        public string TestValue { get; set; }
    }
}

