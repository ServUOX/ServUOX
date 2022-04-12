using System;
using Server.Items;
using Server.Network;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public interface IBloodCreature
    {
    }

    [CorpseName("a bloodworm corpse")]
    public class BloodWorm : BaseCreature, IBloodCreature
    {
        [Constructible]
        public BloodWorm()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a bloodworm";
            Body = 287;

            SetStr(401, 473);
            SetDex(80);
            SetInt(18, 19);

            SetHits(374, 422);

            SetDamage(11, 17);

            SetDamageType(ResistType.Phys, 60);
            SetDamageType(ResistType.Pois, 40);

            SetResist(ResistType.Phys, 52, 55);
            SetResist(ResistType.Fire, 42, 50);
            SetResist(ResistType.Cold, 29, 31);
            SetResist(ResistType.Pois, 69, 75);
            SetResist(ResistType.Engy, 26, 27);

            SetSkill(SkillName.MagicResist, 35.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 100.0);

            SetSpecialAbility(SpecialAbility.Anemia);
        }

        public BloodWorm(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.02)
                c.DropItem(new LuckyCoin());
        }

        public override int GetIdleSound()
        {
            return 1503;
        }

        public override int GetAngerSound()
        {
            return 1500;
        }

        public override int GetHurtSound()
        {
            return 1502;
        }

        public override int GetDeathSound()
        {
            return 1501;
        }

        public override void OnAfterMove(Point3D oldLocation)
        {
            base.OnAfterMove(oldLocation);

            if (Hits < HitsMax && 0.25 > Utility.RandomDouble())
            {
                Corpse toAbsorb = null;

                foreach (Item item in Map.GetItemsInRange(Location, 1))
                {
                    if (item is Corpse)
                    {
                        Corpse c = (Corpse)item;

                        if (c.ItemID == 0x2006)
                        {
                            toAbsorb = c;
                            break;
                        }
                    }
                }

                if (toAbsorb != null)
                {
                    toAbsorb.ProcessDelta();
                    toAbsorb.SendRemovePacket();
                    toAbsorb.ItemID = Utility.Random(0xECA, 9); // bone graphic
                    toAbsorb.Hue = 0;
                    toAbsorb.Direction = Direction.North;
                    toAbsorb.ProcessDelta();

                    Hits = HitsMax;

                    // * The creature drains blood from a nearby corpse to heal itself. *
                    PublicOverheadMessage(MessageType.Regular, 0x3B2, 1111699);
                }
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
            int version = reader.ReadInt();
        }
    }
}
