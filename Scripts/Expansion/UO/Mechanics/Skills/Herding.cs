using Server.Engines.CannedEvil;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using System;

namespace Server.SkillHandlers
{
    public class HerdingTarget : Target
    {
        private static readonly Type[] m_ChampTamables = new Type[]
        {
                typeof(StrongMongbat), typeof(Imp), typeof(Scorpion), typeof(GiantSpider),
                typeof(Snake), typeof(LavaLizard), typeof(Drake), typeof(Dragon),
                typeof(Kirin), typeof(Unicorn), typeof(GiantRat), typeof(Slime),
                typeof(DireWolf), typeof(HellHound), typeof(DeathwatchBeetle),
                typeof(LesserHiryu), typeof(Hiryu)
        };

        private readonly ShepherdsCrook m_Crook;

        public HerdingTarget(ShepherdsCrook crook)
            : base(10, false, TargetFlags.None)
        {
            m_Crook = crook;
        }

        protected override void OnTarget(Mobile from, object targ)
        {
            if (targ is BaseCreature bc)
            {
                if (IsHerdable(bc))
                {
                    if (bc.Controlled)
                    {
                        bc.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502467, from.NetState); // That animal looks tame already.
                    }
                    else
                    {
                        from.SendLocalizedMessage(502475); // Click where you wish the animal to go.
                        from.Target = new ShepherdsCrookTarget(bc, m_Crook);
                    }
                }
                else
                {
                    from.SendLocalizedMessage(502468); // That is not a herdable animal.
                }
            }
            else
            {
                from.SendLocalizedMessage(502472); // You don't seem to be able to persuade that to move.
            }
        }

        private bool IsHerdable(BaseCreature bc)
        {
            if (bc.IsParagon)
            {
                return false;
            }

            if (bc.Tamable)
            {
                return true;
            }

            Map map = bc.Map;


            if (Region.Find(bc.Home, map) is ChampionSpawnRegion region)
            {
                ChampionSpawn spawn = region.ChampionSpawn;

                if (spawn != null && spawn.IsChampionSpawn(bc))
                {
                    Type t = bc.GetType();

                    foreach (Type type in m_ChampTamables)
                    {
                        if (type == t)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private class ShepherdsCrookTarget : Target
        {
            private readonly BaseCreature m_Creature;
            private readonly ShepherdsCrook m_Crook;

            public ShepherdsCrookTarget(BaseCreature c, ShepherdsCrook crook)
                : base(10, true, TargetFlags.None)
            {
                m_Creature = c;
                m_Crook = crook;
            }

            protected override void OnTarget(Mobile from, object targ)
            {
                if (targ is IPoint2D)
                {
                    double min = m_Creature.CurrentTameSkill - 30;
                    double max = m_Creature.CurrentTameSkill + 30 + Utility.Random(10);

                    if (max <= from.Skills[SkillName.Herding].Value)
                    {
                        m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502471, from.NetState); // That wasn't even challenging.
                    }

                    if (from.CheckTargetSkill(SkillName.Herding, m_Creature, min, max))
                    {
                        IPoint2D p = (IPoint2D)targ;

                        if (targ != from)
                        {
                            p = new Point2D(p.X, p.Y);
                        }

                        m_Creature.TargetLocation = p;
                        from.SendLocalizedMessage(502479); // The animal walks where it was instructed to.

                        if (Siege.SiegeShard && m_Crook is IUsesRemaining)
                        {
                            Siege.CheckUsesRemaining(from, m_Crook);
                        }
                    }
                    else
                    {
                        from.SendLocalizedMessage(502472); // You don't seem to be able to persuade that to move.
                    }
                }
            }
        }
    }
}
