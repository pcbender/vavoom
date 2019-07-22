using System.Linq;

namespace Vavoom.Tester
{
    public static class StaticJumble
    {
        private static string Reversed(string txt)
        {
           return new string(txt.ToCharArray().Reverse().ToArray());
        }
    }

    public abstract class BaseBase
    {
        private string _field2 = "Hello David!";

        protected BaseBase()
        {
            Name = nameof(BaseBase);
        }

        protected string Reverse(string val)
        {
            return new string(val.ToCharArray().Reverse().ToArray());
        }

        internal string Name { get; set; } 
    }

    public class FooBase : BaseBase
    {
        public FooBase()
        {
            Number = 42;
        }

        internal int Number { get; }

        private int DoubleIt()
        {
            return Number * 2;
        }
    }

    public class Foo : FooBase
    {
        public Foo()
        {
            Bar = new Bar();
        }

        internal Bar Bar { get; }
    }

    internal class Bar
    {
        internal Bar()
        {
            Baz = new Baz();
        }

        internal Baz Baz { get; }
    }

    internal class Baz
    {
        private string _stuff;

        internal Baz()
        {
            _stuff = "Gwendolyn";
        }
    }
}
