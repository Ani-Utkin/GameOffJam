using ScriptableObjects;

namespace RTSI.Services
{
    public class PlayerStatsService
    {
        public int MaxHealth { get; private set; }
        public int Attack { get; private set; }
        public int Dodge { get; private set; }
        public int Speed { get; private set; }
        
        public int CurrentHealth { get; set; }
        
        public void Initialize(CombatStats playerCombatStats)
        {
            MaxHealth = playerCombatStats.MaxHealth;
            Attack = playerCombatStats.Attack;
            Dodge = playerCombatStats.Dodge;
            Speed = playerCombatStats.Speed;
            CurrentHealth = MaxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
        }
    }
}