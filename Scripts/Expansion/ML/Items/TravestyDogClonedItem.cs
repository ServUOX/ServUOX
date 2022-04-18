namespace Server.Items
{
    public class TravestyDogClonedItem : Item
    {
        public TravestyDogClonedItem(Item item)
            : base(item.ItemID)
        {
            Name = item.Name;
            Weight = item.Weight;
            Hue = item.Hue;
            Layer = item.Layer;
        }

        public TravestyDogClonedItem(Serial serial)
            : base(serial)
        {
        }

        public override DeathMoveResult OnParentDeath(Mobile parent)
        {
            Delete();

            return DeathMoveResult.RemainEquiped;
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
