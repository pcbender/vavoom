using System.Linq;

namespace Super.Secret.Library
{
    internal static class InternalPrivateJumble
    {
        private static class MeToo
        {
            private static string Reversed(string txt)
            {
                return new string(txt.ToCharArray().Reverse().ToArray());
            } 
        }

        internal static string Name => nameof(InternalPrivateJumble);

    }
}