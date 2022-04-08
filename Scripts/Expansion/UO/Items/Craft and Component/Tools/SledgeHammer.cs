using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable(0xFB5, 0xFB4)]
    public class SledgeHammer : BaseTool
    {
        [Constructible]
        public SledgeHammer()
            : base(0xFB5)
        {
            Layer = Layer.OneHanded;
        }

        [Constructible]
        public SledgeHammer(int uses)
            : base(uses, 0xFB5)
        {
            Layer = Layer.OneHanded;
        }

        public SledgeHammer(Serial serial)
            : base(serial)
        {
        }

        public override CraftSystem CraftSystem => DefBlacksmithy.CraftSystem;
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

    public class SledgeHammerWeapon : BaseBashing, ITool
    {
        [Constructible]
        public SledgeHammerWeapon()
            : base(0xFB5)
        {
            Layer = Layer.OneHanded;
            ShowUsesRemaining = true;
        }

        #region ITool Members
        public CraftSystem CraftSystem => DefBlacksmithy.CraftSystem;
        public bool BreakOnDepletion => true;

        public bool CheckAccessible(Mobile m, ref int num)
        {
            if (!IsChildOf(m) && Parent != m)
            {
                num = 1044263;
                return false;
            }

            return true;
        }
        #endregion

        public SledgeHammerWeapon(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.CrushingBlow;
        public override WeaponAbility SecondaryAbility => WeaponAbility.ParalyzingBlow;
        public override int AosStrengthReq => 40;
        public override int AosMinDamage => 13;
        public override int AosMaxDamage => 17;
        public override int AosSpeed => 40;
        public override float MlSpeed => 3.25f;
        public override int OldStrengthReq => 40;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 70;

        public override void OnDoubleClick(Mobile from)
        {
            if (CraftSystem != null && (IsChildOf(from.Backpack) || Parent == from))
            {
                int num = CraftSystem.CanCraft(from, this, null);

                if (num > 0 && (num != 1044267 || !Core.SE)) // Blacksmithing shows the gump regardless of proximity of an anvil and forge after SE
                {
                    from.SendLocalizedMessage(num);
                }
                else
                {
                    _ = CraftSystem.GetContext(from);

                    from.SendGump(new CraftGump(from, CraftSystem, this, null));
                }
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
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
