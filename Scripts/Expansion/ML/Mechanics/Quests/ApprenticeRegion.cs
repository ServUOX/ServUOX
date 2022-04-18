using System.Collections;
using System.Xml;
using Server.Engines.Quests;
using Server.Mobiles;

namespace Server.Regions
{
    public class ApprenticeRegion : BaseRegion
    {
        private readonly Hashtable m_Table = new Hashtable();
        public ApprenticeRegion(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
        {
        }

        public Hashtable Table => m_Table;
        public override void OnEnter(Mobile m)
        {
            base.OnEnter(m);

            if (m is PlayerMobile player)
            {
                for (int i = 0; i < player.Quests.Count; i++)
                {
                    BaseQuest quest = player.Quests[i];

                    for (int j = 0; j < quest.Objectives.Count; j++)
                    {
                        BaseObjective objective = quest.Objectives[j];

                        if (objective is ApprenticeObjective objective1 && !objective.Completed)
                        {
                            ApprenticeObjective apprentice = objective1;

                            if (IsPartOf(apprentice.Region))
                            {
                                if (apprentice.Enter is int @int)
                                    player.SendLocalizedMessage(@int);
                                else if (apprentice.Enter is string @string)
                                    player.SendMessage(@string);

                                BuffInfo info = new BuffInfo(BuffIcon.ArcaneEmpowerment, 1078511, 1078512, apprentice.Skill.ToString()); // Accelerated Skillgain Skill: ~1_val~
                                BuffInfo.AddBuff(m, info);
                                m_Table[m] = info;
                            }
                        }
                    }
                }
            }
        }

        public override void OnExit(Mobile m)
        {
            base.OnExit(m);

            if (m is PlayerMobile player)
            {
                for (int i = 0; i < player.Quests.Count; i++)
                {
                    BaseQuest quest = player.Quests[i];

                    for (int j = 0; j < quest.Objectives.Count; j++)
                    {
                        BaseObjective objective = quest.Objectives[j];

                        if (objective is ApprenticeObjective objective1 && !objective.Completed)
                        {
                            ApprenticeObjective apprentice = objective1;

                            if (IsPartOf(apprentice.Region))
                            {
                                if (apprentice.Leave is int @int)
                                    player.SendLocalizedMessage(@int);
                                else if (apprentice.Leave is string @string)
                                    player.SendMessage(@string);

                                if (m_Table[m] is BuffInfo info)
                                    BuffInfo.RemoveBuff(m, info);
                            }
                        }
                    }
                }
            }
        }
    }
}
