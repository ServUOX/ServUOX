using Server.Items;

namespace Server.Mobiles
{
    public class CursedMetallicKnight : BaseCreature
    {
        [Constructible]
        public CursedMetallicKnight()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "cursed metallic knight";
            Body = 147;
            BaseSoundID = 451;

            Hue = Utility.RandomMetalHue();

            SetStr(201, 242);
            SetDex(76, 87);
            SetInt(36, 53);

            SetHits(118, 150);

            SetDamage(8, 18);

            SetDamageType(ResistType.Cold, 100);

            SetResist(ResistType.Physical, 35, 45);
            SetResist(ResistType.Fire, 20, 30);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Poison, 20, 30);
            SetResist(ResistType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 67.4, 77.8);
            SetSkill(SkillName.Tactics, 89.5, 98.5);
            SetSkill(SkillName.Wrestling, 86.8, 93.1);
            PackItem(Loot.PackGold(75, 200));
        }

        public override bool DeleteCorpseOnDeath => true;

        public override bool OnBeforeDeath()
        {
            if (!base.OnBeforeDeath())
                return false;

            new TreasureSand().MoveToWorld(Location, Map);

            return true;
        }

        public CursedMetallicKnight(Serial serial)
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
            _ = reader.ReadInt();
        }
    }
}
