using System;
using System.Collections.Generic;
using Super.Secret.Library;

namespace Vavoom.Tester
{
    class Program
    {
        static void Main(string[] args)
        {

            var foo = ExposedObject.From(new Foo());

            string stuff = foo.Bar.Baz._stuff;

            Console.WriteLine(stuff);

            foo.Name = "Bilbo";

            Console.WriteLine(foo.Name);

            Console.WriteLine(foo.DoubleIt());

            Console.WriteLine(foo.Reverse("Bilbo"));

            Console.WriteLine(foo._field2);

            foo._field2 = "Hello, Bilbo!";

            Console.WriteLine(foo._field2);

            var realList = new List<int>();

            var exposedList = ExposedObject.From(realList);

            // Read a private field - prints 0
            Console.WriteLine(exposedList._size);

            // Modify a private field
            exposedList._items = new int[] { 5, 4, 3, 2, 1 };

            // Modify another private field
            exposedList._size = 5;

            // Call a private method
            exposedList.EnsureCapacity(20);


            // Add a value to the list
            exposedList.Add(0);

            // Enumerate the list. Prints "5 4 3 2 1 0"
            foreach (var x in exposedList) 
                Console.WriteLine(x);

            ThisIsAnUnrelatedInterface newInstance = ExposedObject.New<ThisIsAnUnrelatedInterface>( typeof(ImVisible), "MyName" );

            Console.WriteLine(newInstance.Name);

            // Call a static method
            var staticJumble = ExposedClass.From(typeof(StaticJumble));
            string reversed = staticJumble.Reversed("Prow scuttle parrel provost Sail ho shrouds spirits boom mizzenmast yardarm. Pinnace holystone mizzenmast quarter crow's nest nipperkin grog yardarm hempen halter furl. Swab barque interloper chantey doubloon starboard grog black jack gangway rutters.");

            Console.WriteLine(reversed);

            var internalJumble = ExposedClass.From("Super.Secret.Library.InternalJumble", typeof(ImVisible).Assembly);
            reversed = internalJumble.Reversed(reversed);

            Console.WriteLine(reversed);

            var internalPrivateJumble = ExposedClass.From("Super.Secret.Library.InternalPrivateJumble", typeof(ImVisible).Assembly);
            reversed = internalPrivateJumble.MeToo.Reversed(reversed);

            Console.WriteLine(reversed);

            // Call a generic method
            var enumerableType = ExposedClass.From(typeof(System.Linq.Enumerable));
            Console.WriteLine(
                enumerableType.Max(new[] { 1, 3, 5, 3, 1 }));
        }
    } 

    public interface ThisIsAnUnrelatedInterface
    {
        string Name { get; }
    }
}