using EXILED;
using System;
using System.Collections.Generic;
using System.IO;

namespace PlayersWhoPlayed
{
    public static class Global
    {
        public static readonly string FileName = "PlayersWhoPlayed.txt";
        public static readonly string FilePath = "/etc/scpsl/Plugin";
        public static readonly string VoidData = "";
        public static readonly float MinimumTimeToAllowPlayer = 180.0f;

        public static void SendListToServer(string serverName)
        {
            string allowedMessage = "Игроки, поигравшие **достаточно** на сервере " + serverName + ":\n";
            string refusedMessage = "Игроки, поигравшие **недостаточно** на сервере " + serverName + ":\n";
            Dictionary<string, PlayerData> tempPlayersTimeOnServer = PlayersTimeOnServer;
            PlayersTimeOnServer = new Dictionary<string, PlayerData>();
            foreach (KeyValuePair<string, PlayerData> playerTimeOnServer in tempPlayersTimeOnServer)
            {
                if (playerTimeOnServer.Value.Time > MinimumTimeToAllowPlayer)
                    allowedMessage = allowedMessage + playerTimeOnServer.Value.Nickname + "@" + playerTimeOnServer.Key.Replace("@steam", string.Empty) + " был на сервере " + GetMinesAndSecond((int)playerTimeOnServer.Value.Time)[0] + " минут и " + GetMinesAndSecond((int)playerTimeOnServer.Value.Time)[1] + " секунд. Игровые роли: " + playerTimeOnServer.Value.Roles + "\n";
                else
                    refusedMessage = refusedMessage + playerTimeOnServer.Value.Nickname + "@" + playerTimeOnServer.Key.Replace("@steam", string.Empty) + " был на сервере " + GetMinesAndSecond((int)playerTimeOnServer.Value.Time)[0] + " минут и " + GetMinesAndSecond((int)playerTimeOnServer.Value.Time)[1] + " секунд. Игровые роли: " + playerTimeOnServer.Value.Roles + "\n";
            }
            SaveMessageOnDisk("-------------------------Round: " + DateTime.Now.ToString() + "-------------------------\n" + allowedMessage + "\n" + refusedMessage);
        }

        private static string[] GetMinesAndSecond(int seconds)
        {
            int mines = 0;
            if (seconds > 60)
            {
                mines = seconds / 60;
                seconds %= 60;
            }
            return new string[] { mines.ToString(), seconds.ToString() };
        }

        public static void SaveMessageOnDisk(string message)
        {
            try
            {
                File.WriteAllText(Path.Combine(FilePath, FileName), message, System.Text.Encoding.UTF8);
                Log.Info("Success saved message on disk");
            }
            catch (Exception ex)
            {
                Log.Warn("Catch an exception while save message on disk: " + ex.Message);
            }
        }

        public static Dictionary<string, PlayerData> PlayersTimeOnServer = new Dictionary<string, PlayerData>();
    }

    public class PlayerData
    {
        public float Time { get; set; }
        public string Nickname { get; private set; }
        public string Roles { get; set; }
        public PlayerData(float Time, string Nickname, string Roles)
        {
            this.Time = Time;
            this.Nickname = Nickname;
            this.Roles = Roles;
        }
    }
}