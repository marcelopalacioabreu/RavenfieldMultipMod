using System;

namespace RFMultipMod
{
    public class Hook
    {
        public static void StartMultiplayerMod()
        {
            Console.WriteLine("Ravenfield Multiplayer Mod is GO for launch."); //This will write to output.log.
            //TODO: Get some runtime level hooks injected here, see https://www.codeproject.com/articles/463508/net-clr-injection-modify-il-code-on-run-time
        }
    }
}
