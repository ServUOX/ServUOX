using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;

namespace Server.Mobiles
{
    public abstract class BaseHealer : BaseVendor
    {
        private static readonly TimeSpan ResurrectDelay = TimeSpan.FromSeconds(2.0);
        private DateTime m_NextResurrect;

        private List<PlayerMobile> m_ResQue = new List<PlayerMobile>();

        private readonly List<SBInfo> m_SBInfos = new List<SBInfo>();

        public BaseHealer()
            : base(null)
        {
            if (!IsInvulnerable)
            {
                AI = AIType.AI_Mage;
                ActiveSpeed = 0.2;
                PassiveSpeed = 0.8;
                RangePerception = DefaultRangePerception;
                FightMode = FightMode.Aggressor;
            }

            SpeechHue = 0;

            SetStr(304, 400);
            SetDex(102, 150);
            SetInt(204, 300);

            SetDamage(10, 23);

            SetDamageType(ResistType.Physical, 100);

            SetResist(ResistType.Physical, 40, 50);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Poison, 40, 50);
            SetResist(ResistType.Energy, 40, 50);

            SetSkill(SkillName.Anatomy, 75.0, 97.5);
            SetSkill(SkillName.EvalInt, 82.0, 100.0);
            SetSkill(SkillName.Healing, 75.0, 97.5);
            SetSkill(SkillName.Magery, 82.0, 100.0);
            SetSkill(SkillName.MagicResist, 82.0, 100.0);
            SetSkill(SkillName.Tactics, 82.0, 100.0);

            Fame = 1000;
            Karma = 10000;
        }

        public BaseHealer(Serial serial)
            : base(serial)
        {
        }

        public override bool IsActiveVendor => false;
        public override bool IsInvulnerable => false;
        public override VendorShoeType ShoeType => VendorShoeType.Sandals;
        public virtual bool HealsYoungPlayers => true;
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override void InitSBInfo()
        {
        }

        public virtual int GetRobeColor()
        {
            return Utility.RandomYellowHue();
        }

        public override void InitOutfit()
        {
            base.InitOutfit();
            AddItem(new Robe(GetRobeColor()));
        }

        public virtual bool CheckResurrect(Mobile m)
        {
            return true;
        }

        public virtual void OfferResurrection(Mobile m)
        {
            PlayerMobile pm = m as PlayerMobile;
            if (pm == null) return;

            if (!m_ResQue.Contains(pm))
            {
                m_ResQue.Add(pm);
            }
            else return;

            Direction = GetDirectionTo(m);
            m.PlaySound(0x1F2);
            // m.FixedEffect(0x376A, 10, 16);

            m.CloseGump(typeof(ResurrectGump));
            m.SendGump(new ResurrectGump(m, ResurrectMessage.Healer));
        }

        public virtual void OfferHeal(PlayerMobile m)
        {
            Direction = GetDirectionTo(m);

            if (m.CheckYoungHealTime())
            {
                Say(501229); // You look like you need some healing my child.

                m.PlaySound(0x1F2);
                m.FixedEffect(0x376A, 9, 32);

                m.Hits = m.HitsMax;
            }
            else
            {
                Say(501228); // I can do no more for you at this time.
            }
        }

        public override bool CheckMovement(Direction d, out int newZ)
        {
            if (!base.CheckMovement(d, out newZ) || m_ResQue.Count > 0)
                return false;
            return true;
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            List<PlayerMobile> m_ResQueDel = new List<PlayerMobile>();

            if (DateTime.UtcNow >= m_NextResurrect)
            {
                foreach (PlayerMobile pm in m_ResQue)
                {
                    if (pm == null || pm.HasGump(typeof(ResurrectGump))) continue;
                    else
                    {
                        m_ResQueDel.Add(pm);
                    }
                }
                foreach (PlayerMobile pmd in m_ResQueDel)
                {
                    if (m_ResQue.Contains(pmd)) m_ResQue.Remove(pmd);
                }
            }

            if (!m.Alive)
            {
                m_NextResurrect = DateTime.UtcNow + ResurrectDelay;

                if (!m.Frozen && InRange(m, 5) && InLOS(m))
                {
                    var range = (int)GetDistanceToSqrt(m);
                    if (range > 4)
                    {
                        if (m.Map == null || !m.Map.CanFit(m.Location, 16, false, false))
                        {
                            //    m.SendLocalizedMessage(502391); // Thou can not be resurrected there!
                        }
                        else if (CheckResurrect(m))
                        {
                            OfferResurrection(m);
                        }
                    }
                }
            }
            else
            {
                if (HealsYoungPlayers && m.Hits < m.HitsMax && m is PlayerMobile && ((PlayerMobile)m).Young)
                {
                    OfferHeal((PlayerMobile)m);
                }
            }

            /*
            if (!m.Frozen && DateTime.UtcNow >= m_NextResurrect && InRange(m, 5) && !InRange(oldLocation, 2) && InLOS(m))
            {
                if (!m.Alive)
                {
                    m_NextResurrect = DateTime.UtcNow + ResurrectDelay;

                    if (m.Map == null || !m.Map.CanFit(m.Location, 16, false, false))
                    {
                        m.SendLocalizedMessage(502391); // Thou can not be resurrected there!
                    }
                    else if (CheckResurrect(m))
                    {
                        OfferResurrection(m);
                    }
                }
                else if (HealsYoungPlayers && m.Hits < m.HitsMax && m is PlayerMobile && ((PlayerMobile)m).Young)
                {
                    OfferHeal((PlayerMobile)m);
                }
            }
            */
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            if (!IsInvulnerable)
            {
                AI = AIType.AI_Mage;
                ActiveSpeed = 0.2;
                PassiveSpeed = 0.8;
                RangePerception = BaseCreature.DefaultRangePerception;
                FightMode = FightMode.Aggressor;
            }
        }
    }
}
