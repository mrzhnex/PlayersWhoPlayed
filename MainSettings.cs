using EXILED;

namespace PlayersWhoPlayed
{
    public class MainSettings : Plugin
    {
        public override string getName => "PlayersWhoPlayed";
        private SetEvents SetEvents;

        public override void OnEnable()
        {
            SetEvents = new SetEvents();
            Events.RoundStartEvent += SetEvents.OnRoundStart;
            Events.RoundEndEvent += SetEvents.OnRoundEnd;
        }

        public override void OnDisable()
        {
            Events.RoundStartEvent -= SetEvents.OnRoundStart;
            Events.RoundEndEvent -= SetEvents.OnRoundEnd;
        }

        public override void OnReload() { }
    }
}