using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a Malefic corpse")]
    public class Malefic : DreadSpider
    {
        [Constructible]
        public Malefic()
        {
            Name = "Malefic";
            Hue = 0x455;

            SetStr(210, 284);
            SetDex(153, 197);
            SetInt(349, 390);

            SetHits(600, 747);
            SetStam(153, 197);
            SetMana(349, 390);

            SetDamage(ResistType.Phys, 20, 0, 15, 22);
            SetDamage(ResistType.Pois, 80);

            SetResist(ResistType.Phys, 60, 70);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 40, 49);
            SetResist(ResistType.Pois, 100);
            SetResist(ResistType.Engy, 41, 48);

            SetSkill(SkillName.Wrestling, 96.9, 112.4);
            SetSkill(SkillName.Tactics, 91.3, 105.4);
            SetSkill(SkillName.MagicResist, 79.8, 95.1);
            SetSkill(SkillName.Magery, 103.0, 118.6);
            SetSkill(SkillName.EvalInt, 105.7, 119.6);
            SetSkill(SkillName.Meditation, 0);

            Fame = 21000;
            Karma = -21000;

            Tamable = false;

            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            SetWeaponAbility(WeaponAbility.Dismount);

            if ( Utility.RandomDouble() < 0.1 )
            PackItem( new ParrotItem() );
            
        }

        public Malefic(Serial serial)
            : base(serial)
        {
        }

        public override bool CanBeParagon => false;
        public override bool GivesMLMinorArtifact => true;

        public override void GenerateLoot() => AddLoot(LootPack.UltraRich, 3);

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
