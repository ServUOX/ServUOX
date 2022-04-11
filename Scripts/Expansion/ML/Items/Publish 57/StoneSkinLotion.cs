using System;
using Server.Mobiles;
using Server;

namespace Server.Items
{
    [TypeAlias("drNO.ThieveItems.StoneSkinLotion")]
    public class StoneSkinLotion : BaseBalmOrLotion
    {
        protected override void ApplyEffect(PlayerMobile pm)
        {
            pm.AddResistanceMod(new ResistanceMod(ResistType.Cold, -5));
            pm.AddResistanceMod(new ResistanceMod(ResistType.Fire, -5));

            pm.AddResistanceMod(new ResistanceMod(ResistType.Physical, 30));

            base.ApplyEffect(pm);
        }

        public override int LabelNumber => 1094944;  // Stone Skin Lotion

        [Constructible]
        public StoneSkinLotion()
            : base(0xEFD)
        {
            m_EffectType = ThieveConsumableEffect.StoneSkinLotionEffect;
        }

        public StoneSkinLotion(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
