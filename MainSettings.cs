using EXILED;

namespace PlayersWhoPlayed
{
    public class MainSettings : Plugin
    {
        public override string getName => nameof(PlayersWhoPlayed);
        public SetEvents SetEvents { get; set; }

        public override void OnEnable()
        {
            SetEvents = new SetEvents();
            Events.RoundStartEvent += SetEvents.OnRoundStart;
            Events.RoundEndEvent += SetEvents.OnRoundEnd;
            Log.Info(getName + " on");
        }

        public override void OnDisable()
        {
            Events.RoundStartEvent -= SetEvents.OnRoundStart;
            Events.RoundEndEvent -= SetEvents.OnRoundEnd;
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}