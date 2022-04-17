using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a fetid essence corpse")]
    public class FetidEssence : BaseCreature
    {
        [Constructible]
        public FetidEssence()
            : base(AIType.AI_Spellweaving, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a fetid essence";
            Body = 273;

            SetStr(101, 150);
            SetDex(210, 250);
            SetInt(451, 550);

            SetHits(551, 650);

            SetDamage(ResistType.Phys, 30, 0, 21, 25);
            SetDamage(ResistType.Pois, 70);

            SetResist(ResistType.Phys, 40, 50);
            SetResist(ResistType.Fire, 40, 50);
            SetResist(ResistType.Cold, 40, 50);
            SetResist(ResistType.Pois, 70, 90);
            SetResist(ResistType.Engy, 75, 80);

            SetSkill(SkillName.Meditation, 91.4, 99.4);
            SetSkill(SkillName.EvalInt, 88.5, 92.3);
            SetSkill(SkillName.Magery, 97.9, 101.7);
            SetSkill(SkillName.Poisoning, 100);
            SetSkill(SkillName.Anatomy, 0, 4.5);
            SetSkill(SkillName.MagicResist, 103.5, 108.8);
            SetSkill(SkillName.Tactics, 81.0, 84.6);
            SetSkill(SkillName.Wrestling, 81.3, 83.9);

            Fame = 3700;
            Karma = -3700;

            SetAreaEffect(AreaEffect.EssenceOfDisease);
        }

        public FetidEssence(Serial serial)
            : base(serial)
        {
        }

        public override Poison HitPoison => Poison.Deadly;
        public override Poison PoisonImmunity => Poison.Deadly;

        public override void OnDeath(Container CorpseLoot)
        {
            for (int i = 0; i < Utility.RandomMinMax(0, 1); i++)
            {
                CorpseLoot.DropItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot() 
        {
            AddLoot(LootPack.FilthyRich);
        }

        public override int GetHurtSound() => 0x56C;

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
