using System.Collections.Generic;
using Server.ContextMenus;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a llama corpse")]
    public class Llama : BaseCreature
    {
        [Constructible]
        public Llama()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a llama";
            Body = 0xDC;
            BaseSoundID = 0x3F3;

            SetStr(21, 49);
            SetDex(36, 55);
            SetInt(16, 30);

            SetHits(15, 27);
            SetMana(0);

            SetDamage(3, 5);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 15, 20);

            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 19.2, 29.0);
            SetSkill(SkillName.Wrestling, 19.2, 29.0);

            Fame = 300;
            Karma = 0;

            VirtualArmor = 16;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 35.1;
        }

        public Llama(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int Hides => 12;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;

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

    [CorpseName("a llama corpse")]
    public class RidableLlama : BaseMount
    {
        [Constructible]
        public RidableLlama()
            : this("a ridable llama")
        {
        }

        [Constructible]
        public RidableLlama(string name)
            : base(name, 0xDC, 0x3EA6, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0x3F3;

            SetStr(21, 49);
            SetDex(56, 75);
            SetInt(16, 30);

            SetHits(15, 27);
            SetMana(0);

            SetDamage(3, 5);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 10, 15);
            SetResist(ResistType.Fire, 5, 10);
            SetResist(ResistType.Cold, 5, 10);
            SetResist(ResistType.Pois, 5, 10);
            SetResist(ResistType.Engy, 5, 10);

            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 19.2, 29.0);
            SetSkill(SkillName.Wrestling, 19.2, 29.0);

            Fame = 300;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 29.1;
        }

        public RidableLlama(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override int Hides => 12;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }

    [CorpseName("a llama corpse")]
    public class PackLlama : BaseCreature
    {
        [Constructible]
        public PackLlama()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a pack llama";
            Body = 292;
            BaseSoundID = 0x3F3;

            SetStr(52, 80);
            SetDex(36, 55);
            SetInt(16, 30);

            SetHits(50);
            SetStam(86, 105);
            SetMana(0);

            SetDamage(2, 6);

            SetDamageType(ResistType.Phys, 100);

            SetResist(ResistType.Phys, 25, 35);
            SetResist(ResistType.Fire, 10, 15);
            SetResist(ResistType.Cold, 10, 15);
            SetResist(ResistType.Pois, 10, 15);
            SetResist(ResistType.Engy, 10, 15);

            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 19.2, 29.0);
            SetSkill(SkillName.Wrestling, 19.2, 29.0);

            Fame = 0;
            Karma = 200;

            VirtualArmor = 16;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 29.1;

            Container pack = Backpack;

            if (pack != null)
                pack.Delete();

            pack = new StrongBackpack();
            pack.Movable = false;

            AddItem(pack);
        }

        public PackLlama(Serial serial)
            : base(serial)
        {
        }

        public override int Meat => 1;
        public override FoodType FavoriteFood => FoodType.FruitsAndVegies | FoodType.GrainsAndHay;
        public override bool CanAutoStable => (Backpack == null || Backpack.Items.Count == 0) && base.CanAutoStable;

        #region Pack Animal Methods
        public override bool OnBeforeDeath()
        {
            if (!base.OnBeforeDeath())
                return false;

            PackAnimal.CombineBackpacks(this);

            return true;
        }

        public override DeathMoveResult GetInventoryMoveResultFor(Item item)
        {
            return DeathMoveResult.MoveToCorpse;
        }

        public override bool IsSnoop(Mobile from)
        {
            if (PackAnimal.CheckAccess(this, from))
                return false;

            return base.IsSnoop(from);
        }

        public override bool OnDragDrop(Mobile from, Item item)
        {
            if (CheckFeed(from, item))
                return true;

            if (PackAnimal.CheckAccess(this, from))
            {
                AddToBackpack(item);
                return true;
            }

            return base.OnDragDrop(from, item);
        }

        public override bool CheckNonlocalDrop(Mobile from, Item item, Item target)
        {
            return PackAnimal.CheckAccess(this, from);
        }

        public override bool CheckNonlocalLift(Mobile from, Item item)
        {
            return PackAnimal.CheckAccess(this, from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            PackAnimal.TryPackOpen(this, from);
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            PackAnimal.GetContextMenuEntries(this, from, list);
        }
        #endregion

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
