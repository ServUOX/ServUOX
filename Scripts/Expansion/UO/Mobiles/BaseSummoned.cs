using System;

namespace Server.Mobiles
{
    [CorpseName("a corpse")]
    public class BaseSummoned : BaseCreature
    {
        private DateTime m_DecayTime;
        public BaseSummoned(AIType aitype, FightMode fightmode, int spot, int meleerange, double passivespeed, double activespeed)
            : base(aitype, fightmode, spot, meleerange, passivespeed, activespeed)
        {
            m_DecayTime = DateTime.UtcNow + m_Delay;
        }

        public BaseSummoned(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysAttackable => true;
        public override bool DeleteCorpseOnDeath => true;
        public override double DispelDifficulty => 117.5;
        public override double DispelFocus => 45.0;
        public override bool IsDispellable => true;

        public virtual TimeSpan m_Delay => TimeSpan.FromMinutes(2.0);

        public override void OnThink()
        {
            if (DateTime.UtcNow > m_DecayTime)
            {
                FixedParticles(14120, 10, 15, 5012, EffectLayer.Waist);
                PlaySound(510);
                Delete();
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_DecayTime = DateTime.UtcNow + TimeSpan.FromMinutes(1.0);
        }
    }
}
