using Server.Gumps;

namespace Server.Items
{
    public class SmallWorldTreeRugAddonDeed : BaseAddonDeed, IRewardOption
    {
        public override BaseAddon Addon => new SmallWorldTreeRugAddon(South);
        public override int LabelNumber => 1157206;  // Small World Tree

        [Constructible]
        public SmallWorldTreeRugAddonDeed()
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool South { get; private set; }

        public void GetOptions(RewardOptionList list)
        {
            list.Add(0, 1116332); // South 
            list.Add(1, 1116333); // East
        }

        public void OnOptionSelected(Mobile from, int choice)
        {
            South = choice == 0;

            if (!Deleted)
            {
                base.OnDoubleClick(from);
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.CloseGump(typeof(RewardOptionGump));
                from.SendGump(new RewardOptionGump(this));
            }
            else
            {
                from.SendLocalizedMessage(1062334); // This item must be in your backpack to be used.       	
            }
        }

        public SmallWorldTreeRugAddonDeed(Serial serial)
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
