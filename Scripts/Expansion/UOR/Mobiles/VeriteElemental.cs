using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an ore elemental corpse")]
    public class VeriteElemental : BaseCreature
    {
        [Constructible]
        public VeriteElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a verite elemental";
            Body = 113;
            BaseSoundID = 268;

            SetStr(226, 255);
            SetDex(126, 145);
            SetInt(71, 92);

            SetHits(136, 153);

            SetDamage(9, 16);

            SetDamageType(ResistType.Physical, 50);
            SetDamageType(ResistType.Energy, 50);

            SetResist(ResistType.Physical, 30, 40);
            SetResist(ResistType.Fire, 10, 20);
            SetResist(ResistType.Cold, 50, 60);
            SetResist(ResistType.Poison, 50, 60);
            SetResist(ResistType.Energy, 50, 60);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.1, 100.0);
            SetSkill(SkillName.Wrestling, 60.1, 100.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 35;
        }

        public VeriteElemental(Serial serial)
            : base(serial)
        {
        }

        public override bool AutoDispel => true;
        public override bool BleedImmunity => true;
        public override int TreasureMapLevel => 1;

        public override void OnDeath(Container CorpseLoot)
        {
            CorpseLoot.DropItem(new VeriteOre(25));
            //ore.ItemID = 0x19B9;
            base.OnDeath(CorpseLoot);
        }

        public static void OnHit(Mobile defender, Item item, int damage)
        {
            IWearableDurability dur = item as IWearableDurability;

            if (dur == null || dur.MaxHitPoints == 0 || item.LootType == LootType.Blessed || item.Insured)
            {
                return;
            }

            if (damage < 10)
                damage = 10;

            if (dur.HitPoints > 0)
            {
                dur.HitPoints = Math.Max(0, dur.HitPoints - damage);
            }
            else
            {
                defender.LocalOverheadMessage(Server.Network.MessageType.Regular, 0x3B2, 1061121); // Your equipment is severely damaged.
                dur.MaxHitPoints = Math.Max(0, dur.MaxHitPoints - damage);

                if (!item.Deleted && dur.MaxHitPoints == 0)
                {
                    item.Delete();
                }
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Gems, 2);
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
