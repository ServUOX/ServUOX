using System;

namespace Server.Items
{
    #region Reward Clothing
    public class LibraryFriendSkirt : Kilt
    {
        public override int LabelNumber => 1073352; // Friends of the Library Kilt

        [Constructible]
        public LibraryFriendSkirt()
            : this(0)
        {
        }

        [Constructible]
        public LibraryFriendSkirt(int hue)
            : base(hue)
        {
        }

        public LibraryFriendSkirt(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class LibraryFriendPants : LongPants
    {
        public override int LabelNumber => 1073349; // Friends of the Library Pants

        [Constructible]
        public LibraryFriendPants()
            : this(0)
        {
        }

        [Constructible]
        public LibraryFriendPants(int hue)
            : base(hue)
        {
        }

        public LibraryFriendPants(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class MalabellesDress : Skirt
    {
        public override int LabelNumber => 1073251; // Malabelle's Dress - Museum of Vesper Replica

        [Constructible]
        public MalabellesDress()
            : this(0)
        {
        }

        [Constructible]
        public MalabellesDress(int hue)
            : base(hue)
        {
        }

        public MalabellesDress(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    #endregion
}
