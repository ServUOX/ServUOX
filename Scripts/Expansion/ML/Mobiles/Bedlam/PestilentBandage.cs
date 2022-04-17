using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a pestilent bandage corpse")]
    public class PestilentBandage : BaseCreature
    {
        public override double HealChance => 1.0;

        [Constructible]
        public PestilentBandage()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a pestilent bandage";
            Body = 154;
            Hue = 0x515;
            BaseSoundID = 471;

            SetStr(691, 740);
            SetDex(141, 180);
            SetInt(51, 80);

            SetHits(415, 445);

            SetDamage(ResistType.Phys, 40, 0, 13, 23);
            SetDamage(ResistType.Cold, 20);
            SetDamage(ResistType.Pois, 40);

            SetResist(ResistType.Phys, 45, 55);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 20, 30);
            SetResist(ResistType.Engy, 20, 30);

            SetSkill(SkillName.Poisoning, 0.0, 10.0);
            SetSkill(SkillName.Anatomy, 0);
            SetSkill(SkillName.MagicResist, 75.0, 80.0);
            SetSkill(SkillName.Tactics, 80.0, 85.0);
            SetSkill(SkillName.Wrestling, 70.0, 75.0);

            Fame = 20000;
            Karma = -20000;

            VirtualArmor = 28;

            PackItem(new Bandage(5));

            SetAreaEffect(AreaEffect.PoisonBreath);
        }

        public PestilentBandage(Serial serial)
            : base(serial)
        {
        }

        public override Poison HitPoison => Poison.Lethal;

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}
