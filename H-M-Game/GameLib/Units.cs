using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class Units
    {
        //Создаем конструктор, чтобы при добавлении одинаковых юнитов создавалась его копия
        public Units(Units other)
        {
            Health = other.Health;
            Unit_name = other.Unit_name;
            Attack=other.Attack;
            Defence = other.Defence;
            Maximum_Damage = other.Maximum_Damage;
            Minimum_Damage = other.Minimum_Damage;
            Speed = other.Speed;
            Growth = other.Growth;
            Gold = other.Gold;
            AI_Value = other.AI_Value;
        }
        //свойства юнитов
        public Units() { }
        public string Unit_name { get; set; }
        public uint Attack { get; set; }
        public uint Defence { get; set; }
        public uint Maximum_Damage { get; set; }
        public uint Minimum_Damage { get; set; }
        public double Health { get; set; }
        public uint Speed { get; set; }
        public uint Growth { get; set; }
        public uint AI_Value { get; set; }
        public uint Gold { get; set; }
    }
}
