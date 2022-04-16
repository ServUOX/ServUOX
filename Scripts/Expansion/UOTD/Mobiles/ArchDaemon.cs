using Server.Factions;

namespace Server.Mobiles
{
    [CorpseName("a arch daemon corpse")]
    public class ArchDaemon : BaseCreature
    {
        [Constructible]
        public ArchDaemon()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an Arch Deamon";
            Body = 40;
            Hue = 1755;
            BaseSoundID = 357;

            SetStr(986, 1185);
            SetDex(177, 255);
            SetInt(151, 250);

            SetHits(592, 711);

            SetDamage(ResistType.Phys, 50, 0, 22, 29);
            SetDamage(ResistType.Fire, 25);
            SetDamage(ResistType.Engy, 25);

            SetResist(ResistType.Phys, 65, 80);
            SetResist(ResistType.Fire, 60, 80);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 40, 50);

            SetSkill(SkillName.Anatomy, 25.1, 50.0);
            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 95.5, 100.0);
            SetSkill(SkillName.Meditation, 25.1, 50.0);
            SetSkill(SkillName.MagicResist, 100.5, 150.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 24000;
            Karma = -24000;

            VirtualArmor = 90;
        }

        public ArchDaemon(Serial serial)
            : base(serial)
        {
        }

        public override double DispelDifficulty => 125.0;
        public override double DispelFocus => 45.0;
        public override Faction FactionAllegiance => Shadowlords.Instance;
        public override Ethics.Ethic EthicAllegiance => Ethics.Ethic.Evil;
        public override bool CanRummageCorpses => true;
        public override Poison PoisonImmunity => Poison.Regular;
        public override int TreasureMapLevel => 4;
        public override int Meat => 1;
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.MedScrolls, 2);
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
