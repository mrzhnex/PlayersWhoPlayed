using EXILED.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace PlayersWhoPlayed
{
    public class PlayersStuckedComponent : MonoBehaviour
    {
        private float Timer = 0.0f;
        private readonly float TimeIsUp = 5.0f;

        public void Update()
        {
            Timer += Time.deltaTime;
            if (Timer > TimeIsUp)
            {
                Timer = 0.0f;

                foreach (ReferenceHub referenceHub in Player.GetHubs())
                {
                    if (referenceHub != null)
                        ContinueProgress(referenceHub);
                }
            }
        }

        private void ContinueProgress(ReferenceHub referenceHub)
        {
            if (!Global.PlayersTimeOnServer.ContainsKey(referenceHub.GetUserId()))
                Global.PlayersTimeOnServer.Add(referenceHub.GetUserId(), new PlayerData(0.0f, referenceHub.nicknameSync.Network_myNickSync, RolesTranslate[referenceHub.GetRole()]));
            Global.PlayersTimeOnServer[referenceHub.GetUserId()].Time = Global.PlayersTimeOnServer[referenceHub.GetUserId()].Time + TimeIsUp;
            if (!Global.PlayersTimeOnServer[referenceHub.GetUserId()].Roles.Contains(RolesTranslate[referenceHub.GetRole()]))
                Global.PlayersTimeOnServer[referenceHub.GetUserId()].Roles = Global.PlayersTimeOnServer[referenceHub.GetUserId()].Roles + ", " + RolesTranslate[referenceHub.GetRole()];
        }

        private readonly Dictionary<RoleType, string> RolesTranslate = new Dictionary<RoleType, string>()
        {
            { RoleType.Spectator, "Наблюдатель" },
            { RoleType.Scp049, "SCP-049" },
            { RoleType.Scp0492, "SCP-049-2" },
            { RoleType.Scp079, "SCP-079" },
            { RoleType.Scp096, "SCP-096" },
            { RoleType.Scp106, "SCP-106" },
            { RoleType.Scp173, "SCP-173" },
            { RoleType.Scp93953, "SCP-939-53" },
            { RoleType.Scp93989, "SCP-939-89" },
            { RoleType.ChaosInsurgency, "Повстанец Хаоса" },
            { RoleType.ClassD, "Персонал класса D" },
            { RoleType.Scientist, "Научный сотрудник" },
            { RoleType.NtfScientist, "Лейтенант-ученый" },
            { RoleType.NtfCadet, "Кадет" },
            { RoleType.NtfLieutenant, "Лейтенант" },
            { RoleType.NtfCommander, "Командир" },
            { RoleType.FacilityGuard, "Охранник комплекса" },
            { RoleType.Tutorial, "Обучение" },
            { RoleType.None, "[ДАННЫЕ УДАЛЕНЫ]" },
        };
    }
}