using Server.Engines.XmlSpawner2;
using Server.Gumps;
using Server.Items;
using Server.Targeting;

namespace Server.Commands
{
    public class IxpCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("itemexp", AccessLevel.Counselor, new CommandEventHandler(Ixp_OnCommand));
            CommandSystem.Register("ixp", AccessLevel.Counselor, new CommandEventHandler(Ixp_OnCommand));
        }

        [Usage("itemexp")]
        [Description("Item Experience and Points.")]

        public static void Ixp_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Select an item to view experience");
            e.Mobile.Target = new InternalTarget(e.Mobile);
        }

        private class InternalTarget : Target
        {
            private Mobile m_From;

            public InternalTarget(Mobile from) : base(2, false, TargetFlags.None)
            {
                m_From = from;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Item item)
                {
                    if (item.Parent != from && item.Parent != from.Backpack)
                    {
                        from.SendMessage("The item must be in your pack or equipped!");
                        return;
                    }

                    //if ( item is ILevelable)
                    if (XmlAttach.FindAttachment(item, typeof(XmlLevelItem)) is XmlLevelItem)
                    {
                        if (item is BaseWeapon || item is BaseArmor || item is BaseJewel || item is BaseClothing)
                        {
                            from.SendGump(new ItemExperienceGump(from, item, AttributeCategory.Melee));
                        }
                        else
                        {
                            from.SendMessage("That is not a valid levelable item");
                        }
                    }
                    else
                    {
                        from.SendMessage("That item is not levelable!");
                    }
                }
                else
                {
                    from.SendMessage("That is not a valid item!");
                }

            }
        }
    }
}
