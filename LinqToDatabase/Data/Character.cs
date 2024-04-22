using System.ComponentModel.DataAnnotations.Schema;

namespace LinqToDatabase.Data
{
    public class Character
    {
        public const int maxHp = 100;
        public int CharacterId { get; set; }
        public int CharacterLevel { get; set; }
        public string NickName { get; set; }
        public int LifePoints { get; set; } = maxHp;
        public int PlayerId { get; set; }
        public Player? Player { get; set; }
        public List<Inventory>? Inventory { get; set; }

        public void setLife(int value)
        {
            LifePoints += value;
            if(LifePoints > maxHp) LifePoints = maxHp;
        }
    }
}
