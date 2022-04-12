using System;

namespace Server.Mobiles
{
    [CorpseName("a vampire bat corpse")]
    public class VampireBatFamiliar : BaseFamiliar
    {
        public VampireBatFamiliar()
        {
            Name = "a vampire bat";
            Body = 317;
            BaseSoundID = 0x270;

            SetStr(120);
            SetDex(120);
            SetInt(100);

            SetHits(90);
            SetStam(120);
            SetMana(0);

            SetDamage(5, 10);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 10, 15);
            SetResist(ResistType.Fire, 10, 15);
            SetResist(ResistType.Cold, 10, 15);
            SetResist(ResistType.Pois, 10, 15);
            SetResist(ResistType.Engy, 10, 15);

            SetSkill(SkillName.Wrestling, 95.1, 100.0);
            SetSkill(SkillName.Tactics, 50.0);

            ControlSlots = 1;
        }

        public VampireBatFamiliar(Serial serial)
            : base(serial)
        {
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
        }
    }
}