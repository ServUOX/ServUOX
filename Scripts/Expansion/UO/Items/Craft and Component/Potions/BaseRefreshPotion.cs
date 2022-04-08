namespace Server.Items
{
    public abstract class BaseRefreshPotion : BasePotion
    {
        public BaseRefreshPotion(PotionEffect effect)
            : base(0xF0B, effect)
        {
        }

        public BaseRefreshPotion(Serial serial)
            : base(serial)
        {
        }

        public abstract double Refresh { get; }
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

        public override void Drink(Mobile from)
        {
            if (from.Stam < from.StamMax)
            {
                from.Stam += Scale(from, (int)(Refresh * from.StamMax));

                PlayDrinkEffect(from);
                Consume();
            }
            else
            {
                from.SendMessage("You decide against drinking this potion, as you are already at full stamina.");
            }
        }
    }
}
