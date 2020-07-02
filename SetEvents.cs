using UnityEngine;

namespace PlayersWhoPlayed
{
    public class SetEvents
    {
        internal void OnRoundEnd()
        {
            Global.SendListToServer("Full RP");
        }

        internal void OnRoundStart()
        {
            GameObject.FindWithTag("FemurBreaker").AddComponent<PlayersStuckedComponent>();
        }
    }
}