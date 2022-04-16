using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a juggernaut corpse")]
    public class Juggernaut : BaseCreature
    {
        [Constructible]
        public Juggernaut()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.3, 0.6)
        {
            Name = "a blackthorn juggernaut";
            Body = 768;

            SetStr(301, 400);
            SetDex(51, 70);
            SetInt(51, 100);

            SetHits(181, 240);

            SetDamage(ResistType.Phys, 50, 0, 12, 19);
            SetDamage(ResistType.Fire, 25);
            SetDamage(ResistType.Engy, 25);

            SetResist(ResistType.Phys, 65, 75);
            SetResist(ResistType.Fire, 35, 45);
            SetResist(ResistType.Cold, 35, 45);
            SetResist(ResistType.Pois, 15, 25);
            SetResist(ResistType.Engy, 10, 20);

            SetSkill(SkillName.Anatomy, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 140.1, 150.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 12000;
            Karma = -12000;

            VirtualArmor = 70;

            if (0.1 > Utility.RandomDouble())
                PackItem(new PowerCrystal());

            if (0.4 > Utility.RandomDouble())
                PackItem(new ClockworkAssembly());

            SetSpecialAbility(SpecialAbility.ColossalBlow);
        }

        public Juggernaut(Serial serial)
            : base(serial)
        {
        }

        public override bool AlwaysMurderer => true;
        public override bool BardImmunity => !Core.AOS;
        public override bool BleedImmunity => true;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override int Meat => 1;
        public override int TreasureMapLevel => 5;

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (0.05 > Utility.RandomDouble())
            {
                if (!IsParagon)
                {
                    if (0.75 > Utility.RandomDouble())
                        c.DropItem(DawnsMusicGear.RandomCommon);
                    else
                        c.DropItem(DawnsMusicGear.RandomUncommon);
                }
                else
                    c.DropItem(DawnsMusicGear.RandomRare);
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Gems, 1);
        }

        public override int GetDeathSound()
        {
            return 0x423;
        }

        public override int GetAttackSound()
        {
            return 0x23B;
        }

        public override int GetHurtSound()
        {
            return 0x140;
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
