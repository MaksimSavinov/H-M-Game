using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using GameLib;
using System.Xml.Linq;

namespace HW2
{
    public partial class Fight : Form
    {
        static Random generator = new Random();
        //лист для юнитов 1 игрока
        List<Units> Player1UnitsList = new List<Units>();
        //лист юнитов 2 игрока
        List<Units> Player2UnitsList = new List<Units>();
        //лист юнитов 2 игрока с сохранением всех юнитов
        List<Units> FullBotUnitsList = new List<Units>();
        int selectedButton;
        public Fight(List<Units> PlayerUnitsList, List<Units> BotUnitsList)
        {
            //инициализируем лист юнитов игроков
            Player1UnitsList = PlayerUnitsList;
            Player2UnitsList = BotUnitsList;
            foreach (var unit in BotUnitsList)
            {
                FullBotUnitsList.Add(unit);
            }
            InitializeComponent();
        }
        /// <summary>
        /// присваиваем начальные значения лейблов и растягиваем форму на весь экран(сорри за эпилепсию)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fight_Load(object sender, EventArgs e)
        {
            label1.Text = $"{Player1UnitsList[0].Unit_name} \n Health = {(Player1UnitsList[0].Health > 0 ? Player1UnitsList[0].Health : 0)}";
            label2.Text = $"{Player1UnitsList[1].Unit_name} \n Health = {(Player1UnitsList[1].Health > 0 ? Player1UnitsList[1].Health : 0)}";
            label3.Text = $"{Player1UnitsList[2].Unit_name} \n Health = {(Player1UnitsList[2].Health > 0 ? Player1UnitsList[2].Health : 0)}";
            label4.Text = $"{Player1UnitsList[3].Unit_name} \n Health = {(Player1UnitsList[3].Health > 0 ? Player1UnitsList[3].Health : 0)}";
            label5.Text = $"{Player1UnitsList[4].Unit_name} \n Health = {(Player1UnitsList[4].Health > 0 ? Player1UnitsList[4].Health : 0) }";
            label6.Text = $"{Player2UnitsList[0].Unit_name} \n Health = {(Player2UnitsList[0].Health > 0 ? Player2UnitsList[0].Health : 0)}";
            label7.Text = $"{Player2UnitsList[1].Unit_name} \n Health = {(Player2UnitsList[1].Health > 0 ? Player2UnitsList[1].Health : 0)}";
            label8.Text = $"{Player2UnitsList[2].Unit_name} \n Health = {(Player2UnitsList[2].Health > 0 ? Player2UnitsList[2].Health : 0)}";
            label9.Text = $"{Player2UnitsList[3].Unit_name} \n Health = {(Player2UnitsList[3].Health > 0 ? Player2UnitsList[3].Health : 0)}";
            label10.Text = $"{Player2UnitsList[4].Unit_name} \n Health = {(Player2UnitsList[4].Health > 0 ? Player2UnitsList[4].Health : 0)}";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// для выхода из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// начало боя,делаем кнопки активными
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label14_Click(object sender, EventArgs e)
        {
            if (label14.Text == "Нажмите для НАЧАЛА БОЯ")
            {
                label14.Text = "Выберите юнита, которым будете атаковать!";
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                //если игра из сохранения и какие то из юнитов уже убиты
                if (Player1UnitsList[0].Health == 0) { button1.Enabled = false; }
                if (Player1UnitsList[1].Health == 0) { button2.Enabled = false; }
                if (Player1UnitsList[2].Health == 0) { button3.Enabled = false; }
                if (Player1UnitsList[3].Health == 0) { button4.Enabled = false; }
                if (Player1UnitsList[4].Health == 0) { button5.Enabled = false; }
                //если все юниты игрока1 убиты
                if ((Player1UnitsList[0].Health + Player1UnitsList[1].Health + Player1UnitsList[2].Health + Player1UnitsList[3].Health + Player1UnitsList[4].Health) == 0)
                {
                    this.Hide();
                    Lose lose = new Lose();
                    lose.Show();
                }
                //если все юниты игрока2 убиты
                if ((Player2UnitsList[0].Health + Player2UnitsList[1].Health + Player2UnitsList[2].Health + Player2UnitsList[3].Health + Player2UnitsList[4].Health) == 0)
                {
                    this.Hide();
                    Win win = new Win();
                    win.Show();
                }
                List<Units> TempBotList = new List<Units>();
                foreach (var unit in Player2UnitsList)
                {
                    TempBotList.Add(unit);
                }
                for (int i = 0; i < Player2UnitsList.Count(); i++)
                {
                    if (Player2UnitsList[i].Health <= 0)
                    {
                        TempBotList.Remove(Player2UnitsList[i]);
                    }
                }
                Player2UnitsList.Clear();
                foreach (var unit in TempBotList)
                {
                    Player2UnitsList.Add(unit);
                }
            }
        }
        /// <summary>
        /// Запускаем раунд при выборе пользователям юнита1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            selectedButton = 0;
            if (label14.Text == "Выберите юнита, которым будете атаковать!") Attack();
            else Defence();
        }
        /// <summary>
        /// Запускаем раунд при выборе пользователям юнита2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            selectedButton = 1;
            if (label14.Text == "Выберите юнита, которым будете атаковать!") Attack();
            else Defence();
        }
        /// <summary>
        /// Запускаем раунд при выборе пользователям юнита3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            selectedButton = 2;
            if (label14.Text == "Выберите юнита, которым будете атаковать!") Attack();
            else Defence();
        }
        /// <summary>
        /// Запускаем раунд при выборе пользователям юнита4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            selectedButton = 3;
            if (label14.Text == "Выберите юнита, которым будете атаковать!") Attack();
            else Defence();
        }
        /// <summary>
        /// Запускаем раунд при выборе пользователям юнита5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            selectedButton = 4;
            if (label14.Text == "Выберите юнита, которым будете атаковать!") Attack();
            else Defence();
        }
        /// <summary>
        /// Метод для описывания атаки
        /// </summary>
        private void Attack()
        {
            //выбор атакующего юнита ботом
            int botNumber = generator.Next(0, Player2UnitsList.Count());
            //боец бота
            Units BotFighter = Player2UnitsList[botNumber];
            //боец игрока
            Units PlayerFighter = Player1UnitsList[selectedButton];
            //расчитываем очки бота
            double BotPoints = BotFighter.Defence + BotFighter.Speed * 0.7 + 0.22 * BotFighter.Growth + 0.2 * BotFighter.AI_Value;
            //расчитываем очки игрока
            double PlayerPoints = PlayerFighter.Attack + PlayerFighter.Speed * 0.8 + 0.1 * PlayerFighter.Growth + 0.2 * PlayerFighter.AI_Value;
            //если атака не прошла
            if (((PlayerPoints - BotPoints) / (PlayerFighter.Health - BotFighter.Health) < 0) || (PlayerFighter.Health == BotFighter.Health))
            {
                MessageBox.Show($"{PlayerFighter.Unit_name} атакует {BotFighter.Unit_name} , однако броня слишком прочная, атака была отражена!");
            }
            //если разность меньше 0.4 и юнит бота погибает,изменяем здоровье на лейбле, убираем юнита из листа юнитов бота
            //меняем здоровье юнита на 0 в FullBotUnitsLIst
            else if ((PlayerPoints - BotPoints) / (PlayerFighter.Health - BotFighter.Health) < 0.4)
            {
                BotFighter.Health = 0;
                MessageBox.Show($"{PlayerFighter.Unit_name} атакует {BotFighter.Unit_name} , удар оказался настолько мощным, что противник погиб на месте!");
                if (BotFighter == FullBotUnitsList[0])
                {
                    label6.Text = $"{FullBotUnitsList[0].Unit_name} \n Health = {0}";
                    FullBotUnitsList[0].Health = 0;
                }
                if (BotFighter == FullBotUnitsList[1])
                {
                    label7.Text = $"{FullBotUnitsList[1].Unit_name} \n Health = {0}";
                    FullBotUnitsList[1].Health = 0;
                }
                if (BotFighter == FullBotUnitsList[2])
                {
                    label8.Text = $"{FullBotUnitsList[2].Unit_name} \n Health = {0}";
                    FullBotUnitsList[2].Health = 0;
                }
                if (BotFighter == FullBotUnitsList[3])
                {
                    label9.Text = $"{FullBotUnitsList[3].Unit_name} \n Health = {0}";
                    FullBotUnitsList[3].Health = 0;
                }
                if (BotFighter == FullBotUnitsList[4])
                {
                    label10.Text = $"{FullBotUnitsList[4].Unit_name} \n Health = {0}";
                    FullBotUnitsList[4].Health = 0;
                }
                Player2UnitsList.Remove(Player2UnitsList[botNumber]);
            }
            // Если атака нанесена,и здоровье защищающегося юнита бота<=0, юнит бота погибает,изменяем здоровье на лейбле, убираем юнита из листа юнитов бота
            //меняем здоровье юнита на 0 в FullBotUnitsLIst
            else
            {
                BotFighter.Health = BotFighter.Health - (PlayerFighter.Maximum_Damage + PlayerFighter.Minimum_Damage) / 2.0;
                if (BotFighter.Health <= 0)
                {
                    BotFighter.Health = 0;
                    MessageBox.Show($"{PlayerFighter.Unit_name} атакует {BotFighter.Unit_name} , здоровье противника упало до критической отметки, он повержен!");
                    if (BotFighter == FullBotUnitsList[0])
                    {
                        label6.Text = $"{FullBotUnitsList[0].Unit_name} \n Health = {0}";
                        FullBotUnitsList[0].Health = 0;
                    }
                    if (BotFighter == FullBotUnitsList[1])
                    {
                        label7.Text = $"{FullBotUnitsList[1].Unit_name} \n Health = {0}";
                        FullBotUnitsList[1].Health = 0;
                    }
                    if (BotFighter == FullBotUnitsList[2])
                    {
                        label8.Text = $"{FullBotUnitsList[2].Unit_name} \n Health = {0}";
                        FullBotUnitsList[2].Health = 0;
                    }
                    if (BotFighter == FullBotUnitsList[3])
                    {
                        label9.Text = $"{FullBotUnitsList[3].Unit_name} \n Health = {0}";
                        FullBotUnitsList[3].Health = 0;
                    }
                    if (BotFighter == FullBotUnitsList[4])
                    {
                        label10.Text = $"{FullBotUnitsList[4].Unit_name} \n Health = {0}";
                        FullBotUnitsList[4].Health = 0;
                    }
                    Player2UnitsList.Remove(Player2UnitsList[botNumber]);
                }
                //Если атака нанесена,и здоровье защищающегося юнита бота>0,изменяем здоровье на лейбле, изменяем здоровье в листе юнитов бота
                //меняем здоровье юнита на нанесенную атаку в FullBotUnitsLIst
                else
                {
                    MessageBox.Show($"{PlayerFighter.Unit_name} атакует {BotFighter.Unit_name} и наносит {(PlayerFighter.Maximum_Damage + PlayerFighter.Minimum_Damage) / 2.0} урона");
                    if (BotFighter == FullBotUnitsList[0])
                    {
                        label6.Text = $"{FullBotUnitsList[0].Unit_name} \n Health = {BotFighter.Health}";
                        FullBotUnitsList[0].Health = BotFighter.Health;
                    }
                    if (BotFighter == FullBotUnitsList[1])
                    {
                        label7.Text = $"{FullBotUnitsList[1].Unit_name} \n Health = {BotFighter.Health}";
                        FullBotUnitsList[1].Health = BotFighter.Health;
                    }
                    if (BotFighter == FullBotUnitsList[2])
                    {
                        label8.Text = $"{FullBotUnitsList[2].Unit_name} \n Health = {BotFighter.Health}";
                        FullBotUnitsList[2].Health = BotFighter.Health;
                    }
                    if (BotFighter == FullBotUnitsList[3])
                    {
                        label9.Text = $"{FullBotUnitsList[3].Unit_name} \n Health = {BotFighter.Health}";
                        FullBotUnitsList[3].Health = BotFighter.Health;
                    }
                    if (BotFighter == FullBotUnitsList[4])
                    {
                        label10.Text = $"{FullBotUnitsList[4].Unit_name} \n Health = {BotFighter.Health}";
                        FullBotUnitsList[4].Health = BotFighter.Health;
                    }
                    Player2UnitsList[botNumber].Health = BotFighter.Health;
                }
            }
            //если все войска бота убиты-вы победили
            if (Player2UnitsList.Count() == 0)
            {
                this.Hide();
                Win win = new Win();
                win.Show();
            }
            //переход к защите
            else
                label14.Text = "Выберите юнита, которым будете защищаться!";
        }
        /// <summary>
        /// метод описывающий защиту игрока1
        /// </summary>
        private void Defence()
        {
            //выбор атакующего юнита бота
            int botNumber = generator.Next(0, Player2UnitsList.Count());
            //создание бойца бота
            Units BotFighter = Player2UnitsList[botNumber];
            //создание бойца игрока1
            Units PlayerFighter = Player1UnitsList[selectedButton];
            //расчитываем очки игрока1
            double PlayerPoints = PlayerFighter.Defence + PlayerFighter.Speed * 0.7 + 0.22 * PlayerFighter.Growth + 0.2 * PlayerFighter.AI_Value;
            //расчитываем очки бота
            double BotPoints = BotFighter.Attack + BotFighter.Speed * 0.8 + 0.1 * BotFighter.Growth + 0.2 * BotFighter.AI_Value;
            //если атака не прошла
            if (((BotPoints - PlayerPoints) / (BotFighter.Health - PlayerFighter.Health) < 0) || (PlayerFighter.Health == BotFighter.Health))
            {
                MessageBox.Show($"{BotFighter.Unit_name} атакует {PlayerFighter.Unit_name} , однако броня слишком прочная, атака была отражена!");
            }
            //если разность меньше 0.4 и юнит игрока1 погибает,изменяем здоровье на лейбле, изменяем здоровье юнита на 0
            else if ((BotPoints - PlayerPoints) / (BotFighter.Health - PlayerFighter.Health) < 0.4)
            {
                PlayerFighter.Health = 0;
                MessageBox.Show($"{BotFighter.Unit_name} атакует {PlayerFighter.Unit_name} , удар оказался настолько мощным, что ваш защитник погиб на месте!");
                if (selectedButton == 0) { label1.Text = $"{Player1UnitsList[0].Unit_name} \n Health = {0}"; button1.Enabled = false; }
                if (selectedButton == 1) { label2.Text = $"{Player1UnitsList[1].Unit_name} \n Health = {0}"; button2.Enabled = false; }
                if (selectedButton == 2) { label3.Text = $"{Player1UnitsList[2].Unit_name} \n Health = {0}"; button3.Enabled = false; }
                if (selectedButton == 3) { label4.Text = $"{Player1UnitsList[3].Unit_name} \n Health = {0}"; button4.Enabled = false; }
                if (selectedButton == 4) { label5.Text = $"{Player1UnitsList[4].Unit_name} \n Health = {0}"; button5.Enabled = false; }
                Player1UnitsList[selectedButton].Health = 0;
            }
            //если после атаки здоровье юнита игрока1<=0, он погибает,изменяем здоровье на лейбле, изменяем здоровье юнита на 0
            else
            {
                PlayerFighter.Health = PlayerFighter.Health - (BotFighter.Maximum_Damage + BotFighter.Minimum_Damage) / 2.0;
                if (PlayerFighter.Health <= 0)
                {
                    MessageBox.Show($"{BotFighter.Unit_name} атакует {PlayerFighter.Unit_name} , здоровье выбранного защитника упало до критической отметки, он повержен!");
                    if (selectedButton == 0) { label1.Text = $"{Player1UnitsList[0].Unit_name} \n Health = {0}"; button1.Enabled = false; }
                    if (selectedButton == 1) { label2.Text = $"{Player1UnitsList[1].Unit_name} \n Health = {0}"; button2.Enabled = false; }
                    if (selectedButton == 2) { label3.Text = $"{Player1UnitsList[2].Unit_name} \n Health = {0}"; button3.Enabled = false; }
                    if (selectedButton == 3) { label4.Text = $"{Player1UnitsList[3].Unit_name} \n Health = {0}"; button4.Enabled = false; }
                    if (selectedButton == 4) { label5.Text = $"{Player1UnitsList[4].Unit_name} \n Health = {0}"; button5.Enabled = false; }
                    Player1UnitsList[selectedButton].Health = 0;
                }
                //если после атаки здоровье юнита игрока1>0,изменяем здоровье на лейбле, изменяем здоровье юнита с вычетом произвденной атаки
                else
                {
                    MessageBox.Show($"{BotFighter.Unit_name} атакует {PlayerFighter.Unit_name} и наносит {(BotFighter.Maximum_Damage + BotFighter.Minimum_Damage) / 2.0} урона");
                    if (selectedButton == 0) { label1.Text = $"{Player1UnitsList[0].Unit_name} \n Health = {PlayerFighter.Health}"; }
                    if (selectedButton == 1) { label2.Text = $"{Player1UnitsList[1].Unit_name} \n Health = {PlayerFighter.Health}"; }
                    if (selectedButton == 2) { label3.Text = $"{Player1UnitsList[2].Unit_name} \n Health = {PlayerFighter.Health}"; }
                    if (selectedButton == 3) { label4.Text = $"{Player1UnitsList[3].Unit_name} \n Health = {PlayerFighter.Health}"; }
                    if (selectedButton == 4) { label5.Text = $"{Player1UnitsList[4].Unit_name} \n Health = {PlayerFighter.Health}"; }
                    Player1UnitsList[selectedButton].Health = PlayerFighter.Health;
                }
            }
            //если после хода здоровье всех юнитов игрока1=0 -поражение
            if ((Player1UnitsList[0].Health + Player1UnitsList[1].Health + Player1UnitsList[2].Health + Player1UnitsList[3].Health + Player1UnitsList[4].Health) == 0)
            {
                this.Hide();
                Lose lose = new Lose();
                lose.Show();
            }
            //иначе записываем автосохранение и переходим к атаке
            else
            {
                Save();
                label14.Text = "Выберите юнита, которым будете атаковать!";
            }
        }
        /// <summary>
        /// сохранение объекта в xml формате
        /// </summary>
        private void Save()
        {
            //создаем документ
            XDocument xdoc = new XDocument();
            //создаем корневой элемент
            XElement units = new XElement("units");
            //для каждого юнита игрока 1 записываем текущее значение каждого параметра
            foreach (var unit in Player1UnitsList)
            {
                XElement MyUnit = new XElement("MyUnit");
                XElement Unit_name = new XElement("Unit_name", unit.Unit_name);
                XElement Attack = new XElement("Attack", unit.Attack);
                XElement Defence = new XElement("Defence", unit.Defence);
                XElement Maximum_Damage = new XElement("Maximum_Damage", unit.Maximum_Damage);
                XElement Minimum_Damage = new XElement("Minimum_Damage", unit.Minimum_Damage);
                XElement Health = new XElement("Health", unit.Health);
                XElement Speed = new XElement("Speed", unit.Speed);
                XElement Growth = new XElement("Growth", unit.Growth);
                XElement AI_Value = new XElement("AI_Value", unit.AI_Value);
                XElement Gold = new XElement("Gold", unit.Gold);
                //добавляем элементы в главный элемент
                MyUnit.Add(Unit_name);
                MyUnit.Add(Attack);
                MyUnit.Add(Defence);
                MyUnit.Add(Maximum_Damage);
                MyUnit.Add(Minimum_Damage);
                MyUnit.Add(Health);
                MyUnit.Add(Speed);
                MyUnit.Add(Growth);
                MyUnit.Add(AI_Value);
                MyUnit.Add(Gold);
                //добавляем главный элемент в корневой элемент
                units.Add(MyUnit);
            }
            foreach (var unit in FullBotUnitsList)
            {
                ////для каждого юнита бота записываем текущее значение каждого параметра
                XElement BotUnit = new XElement("BotUnit");
                XElement Unit_name = new XElement("Unit_name", unit.Unit_name);
                XElement Attack = new XElement("Attack", unit.Attack);
                XElement Defence = new XElement("Defence", unit.Defence);
                XElement Maximum_Damage = new XElement("Maximum_Damage", unit.Maximum_Damage);
                XElement Minimum_Damage = new XElement("Minimum_Damage", unit.Minimum_Damage);
                XElement Health = new XElement("Health", unit.Health);
                XElement Speed = new XElement("Speed", unit.Speed);
                XElement Growth = new XElement("Growth", unit.Growth);
                XElement AI_Value = new XElement("AI_Value", unit.AI_Value);
                XElement Gold = new XElement("Gold", unit.Gold);
                //добавляем элементы в главный элемент
                BotUnit.Add(Unit_name);
                BotUnit.Add(Attack);
                BotUnit.Add(Defence);
                BotUnit.Add(Maximum_Damage);
                BotUnit.Add(Minimum_Damage);
                BotUnit.Add(Health);
                BotUnit.Add(Speed);
                BotUnit.Add(Growth);
                BotUnit.Add(AI_Value);
                BotUnit.Add(Gold);
                //добавляем главный элемент в корневой элемент
                units.Add(BotUnit);
            }
            //добавляем корневой элемент в документ
            xdoc.Add(units);
            //сохраняем документ
            xdoc.Save("save.xml");
        }
    }
}
