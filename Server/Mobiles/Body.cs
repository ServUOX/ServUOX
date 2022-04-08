using System;
using System.IO;

namespace Server
{
    public enum BodyType : byte
    {
        Empty,
        Monster,
        Sea,
        Animal,
        Human,
        Equipment
    }

    public struct Body
    {
        private static readonly BodyType[] m_Types;

        static Body()
        {
            if (File.Exists("Data/bodyTable.cfg"))
            {
                using (StreamReader ip = new StreamReader("Data/bodyTable.cfg"))
                {
                    m_Types = new BodyType[0x1000];

                    string line;

                    while ((line = ip.ReadLine()) != null)
                    {
                        if (line.Length == 0 || line.StartsWith("#"))
                        {
                            continue;
                        }

                        var split = line.Split('\t');

                        BodyType type;

                        if (int.TryParse(split[0], out int bodyID) && Enum.TryParse(split[1], true, out type) && bodyID >= 0 &&
                            bodyID < m_Types.Length)
                        {
                            m_Types[bodyID] = type;
                        }
                        else
                        {
                            Console.WriteLine("Warning: Invalid bodyTable entry:");
                            Console.WriteLine(line);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Warning: Data/bodyTable.cfg does not exist");

                m_Types = new BodyType[0];
            }
        }

        public Body(int bodyID)
        {
            BodyID = bodyID;
        }

        public BodyType Type
        {
            get
            {
                if (BodyID >= 0 && BodyID < m_Types.Length)
                {
                    return m_Types[BodyID];
                }
                else
                {
                    return BodyType.Empty;
                }
            }
        }

        public bool IsHuman => (BodyID >= 0 && BodyID < m_Types.Length && m_Types[BodyID] == BodyType.Human && BodyID != 402 &&
                        BodyID != 403 && BodyID != 607 && BodyID != 608 && BodyID != 970)

        #region Stygian Abyss
                       || BodyID == 694 || BodyID == 695
        #endregion

                    ;

        public bool IsMale => BodyID == 183 || BodyID == 185 || BodyID == 400 || BodyID == 402 || BodyID == 605 ||
                       BodyID == 607 || BodyID == 750

        #region Stygian Abyss
                       || BodyID == 666 || BodyID == 694
        #endregion

                    ;

        public bool IsFemale => BodyID == 184 || BodyID == 186 || BodyID == 401 || BodyID == 403 || BodyID == 606 ||
                       BodyID == 608 || BodyID == 751

        #region Stygian Abyss
                       || BodyID == 667 || BodyID == 695
        #endregion

        #region High Seas
                       || BodyID == 1253
        #endregion

                    ;

        public bool IsGhost => BodyID == 402 || BodyID == 403 || BodyID == 607 || BodyID == 608 || BodyID == 970

        #region Stygian Abyss
                       || BodyID == 694 || BodyID == 695
        #endregion

                    ;

        public bool IsMonster => BodyID >= 0 && BodyID < m_Types.Length && m_Types[BodyID] == BodyType.Monster;

        public bool IsAnimal => BodyID >= 0 && BodyID < m_Types.Length && m_Types[BodyID] == BodyType.Animal;

        public bool IsEmpty => BodyID >= 0 && BodyID < m_Types.Length && m_Types[BodyID] == BodyType.Empty;

        public bool IsSea => BodyID >= 0 && BodyID < m_Types.Length && m_Types[BodyID] == BodyType.Sea;

        public bool IsEquipment => BodyID >= 0 && BodyID < m_Types.Length && m_Types[BodyID] == BodyType.Equipment;

        public bool IsGargoyle => BodyID == 666 || BodyID == 667 || BodyID == 694 || BodyID == 695;

        public int BodyID { get; }

        public static implicit operator int(Body a)
        {
            return a.BodyID;
        }

        public static implicit operator Body(int a)
        {
            return new Body(a);
        }

        public override string ToString()
        {
            return string.Format("0x{0:X}", BodyID);
        }

        public override int GetHashCode()
        {
            return BodyID;
        }

        public override bool Equals(object o)
        {
            if (o == null || !(o is Body))
            {
                return false;
            }

            return ((Body)o).BodyID == BodyID;
        }

        public static bool operator ==(Body l, Body r)
        {
            return l.BodyID == r.BodyID;
        }

        public static bool operator !=(Body l, Body r)
        {
            return l.BodyID != r.BodyID;
        }

        public static bool operator >(Body l, Body r)
        {
            return l.BodyID > r.BodyID;
        }

        public static bool operator >=(Body l, Body r)
        {
            return l.BodyID >= r.BodyID;
        }

        public static bool operator <(Body l, Body r)
        {
            return l.BodyID < r.BodyID;
        }

        public static bool operator <=(Body l, Body r)
        {
            return l.BodyID <= r.BodyID;
        }
    }
}
