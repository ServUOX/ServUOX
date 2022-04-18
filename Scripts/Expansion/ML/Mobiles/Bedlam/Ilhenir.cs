using System;
using Server.Engines.CannedEvil;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a corpse of Ilhenir")]
    public class Ilhenir : BaseChampion
    {
        [Constructible]
        public Ilhenir()
            : base(AIType.AI_Mage)
        {
            Name = "Ilhenir";
            Title = "the Stained";
            Body = 0x103;
            Hue = 1164;

            SetStr(1105, 1350);
            SetDex(82, 160);
            SetInt(505, 750);

            SetHits(9000);

            SetDamage(21, 28);

            SetDamageType(ResistType.Phys, 60);
            SetDamageType(ResistType.Fire, 20);
            SetDamageType(ResistType.Pois, 20);

            SetResist(ResistType.Phys, 55, 65);
            SetResist(ResistType.Fire, 50, 60);
            SetResist(ResistType.Cold, 55, 65);
            SetResist(ResistType.Pois, 70, 90);
            SetResist(ResistType.Engy, 65, 75);

            SetSkill(SkillName.EvalInt, 100);
            SetSkill(SkillName.Magery, 100);
            SetSkill(SkillName.Meditation, 0);
            SetSkill(SkillName.Poisoning, 5.4);
            SetSkill(SkillName.Anatomy, 117.5);
            SetSkill(SkillName.MagicResist, 120.0);
            SetSkill(SkillName.Tactics, 119.9);
            SetSkill(SkillName.Wrestling, 119.9);

            Fame = 24000;
            Karma = -24000;

            VirtualArmor = 44;

            for (int i = 0; i < Utility.RandomMinMax(1, 3); i++)
            {
                PackItem(Loot.RandomScroll(0, Loot.ArcanistScrollTypes.Length, SpellbookType.Arcanist));
            }

            PackItem(Loot.PackGold(2000, 2500));

            SetSpecialAbility(SpecialAbility.DragonBreath);
        }

        public Ilhenir(Serial serial)
            : base(serial)
        {
        }

        public override ChampionSkullType SkullType => ChampionSkullType.Pain;
        public override Type[] UniqueList => new Type[] { };
        public override Type[] SharedList => new Type[]
                {
                    typeof(ANecromancerShroud),
                    typeof(LieutenantOfTheBritannianRoyalGuard),
                    typeof(OblivionsNeedle),
                    typeof(TheRobeOfBritanniaAri)
                };
        public override Type[] DecorativeList => new Type[] { typeof(MonsterStatuette) };
        public override MonsterStatuetteType[] StatueTypes => new MonsterStatuetteType[] { };
        public override bool Unprovokable => true;
        public override bool Uncalmable => true;
        public override Poison PoisonImmunity => Poison.Lethal;
        public override int TreasureMapLevel => 5;

        public virtual void PackTalismans(Container CorpseLoot, int amount)
        {
            int count = Utility.Random(amount);

            for (int i = 0; i < count; i++)
                PackItem(new RandomTalisman());
        }

        public override void OnDeath(Container CorpseLoot) 
        {
            if (Core.ML)
            {
                PackTalismans(CorpseLoot, 5);
            }

            if (Utility.RandomDouble() < 0.5)
                CorpseLoot.DropItem(new AnimalPheromone());

            base.OnDeath(CorpseLoot);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 4);
            AddLoot(LootPack.FilthyRich);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (Utility.RandomDouble() < 0.1)
                DropOoze();

            base.OnDamage(amount, from, willKill);
        }

        public override int GetDeathSound() => 0x57F;
        public override int GetAttackSound() => 0x580;
        public override int GetIdleSound() => 0x581;
        public override int GetAngerSound() => 0x582;
        public override int GetHurtSound() => 0x583;

        public virtual void DropOoze()
        {
            int amount = Utility.RandomMinMax(1, 3);
            bool corrosive = Utility.RandomBool();

            for (int i = 0; i < amount; i++)
            {
                Item ooze = new StainedOoze(corrosive);
                Point3D p = new Point3D(Location);

                for (int j = 0; j < 5; j++)
                {
                    p = GetSpawnPosition(2);
                    bool found = false;

                    foreach (Item item in Map.GetItemsInRange(p, 0))
                        if (item is StainedOoze)
                        {
                            found = true;
                            break;
                        }

                    if (!found)
                        break;
                }

                ooze.MoveToWorld(p, Map);
            }

            if (Combatant is PlayerMobile mobile)
            {
                if (corrosive)
                    mobile.SendLocalizedMessage(1072071); // A corrosive gas seeps out of your enemy's skin!
                else
                    mobile.SendLocalizedMessage(1072072); // A poisonous gas seeps out of your enemy's skin!
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
            _ = reader.ReadInt();
        }
    }
}
