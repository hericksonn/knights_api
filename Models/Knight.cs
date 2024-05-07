using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace KnightsApi.Models
{
    public class Knight
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("nickname")]
        public string Nickname { get; set; } = null!;

        [BsonElement("birthday")]
        public DateTime Birthday { get; set; }

        [BsonElement("weapons")]
        public List<Weapon> Weapons { get; set; } = new();

        [BsonElement("attributes")]
        public Attributes Attributes { get; set; } = new();

        [BsonElement("keyAttribute")]
        public string KeyAttribute { get; set; } = null!;


        [BsonIgnore]
        public int Attack => CalculateAttack();

        [BsonIgnore]
        public int Experience => CalculateExperience();

        private int CalculateAttack()
        {
            int keyAttrMod = GetModifier(Attributes.GetType().GetProperty(KeyAttribute)?.GetValue(Attributes) as int? ?? 0);
            int equippedWeaponMod = Weapons.FirstOrDefault(w => w.Equipped)?.Mod ?? 0;
            return 10 + keyAttrMod + equippedWeaponMod;
        }

        private int CalculateExperience()
        {
            int age = DateTime.Now.Year - Birthday.Year;
            if (age < 7) return 0;
            return (int)Math.Floor((age - 7) * Math.Pow(22, 1.45));
        }

        private int GetModifier(int attributeValue)
        {
            return attributeValue switch
            {
                <= 8 => -2,
                9 or 10 => -1,
                11 or 12 => 0,
                >= 13 and <= 15 => 1,
                >= 16 and <= 18 => 2,
                >= 19 and <= 20 => 3,
                _ => 0
            };
        }
    }

    public class Weapon
    {
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("mod")]
        public int Mod { get; set; }

        [BsonElement("attr")]
        public string Attr { get; set; } = null!;

        [BsonElement("equipped")]
        public bool Equipped { get; set; }
    }

    public class Attributes
    {
        [BsonElement("strength")]
        public int Strength { get; set; }

        [BsonElement("dexterity")]
        public int Dexterity { get; set; }

        [BsonElement("constitution")]
        public int Constitution { get; set; }

        [BsonElement("intelligence")]
        public int Intelligence { get; set; }

        [BsonElement("wisdom")]
        public int Wisdom { get; set; }

        [BsonElement("charisma")]
        public int Charisma { get; set; }
    }
}
