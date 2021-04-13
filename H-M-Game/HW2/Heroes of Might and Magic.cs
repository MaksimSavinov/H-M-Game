using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLib;
using System.Xml;
using System.Globalization;
using System.IO;


namespace HW2
{
    public partial class Heroes_of_Might_and_Magic : Form
    {
        //создаем листы юнитов
        List<Units> PlayerUnitsList = new List<Units>();
        List<Units> BotUnitsList = new List<Units>();
        public Heroes_of_Might_and_Magic()
        {
            InitializeComponent();
        }

        /// <summary>
        /// растягиваем форму на полный экран
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Heroes_of_Might_and_Magic_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }


        /// <summary>
        /// запускаем игру, при выборе НОВАЯ ИГРА
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewGame newgame = new NewGame();
            newgame.Show();
        }

        /// <summary>
        /// для выхода из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// загружаем xml файл если он есть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            GetTeams();
        }
        /// <summary>
        /// загржуем xml файл и парсим его
        /// </summary>
        private void GetTeams()
        {
            //создание нового объекта
            XmlDocument xml = new XmlDocument();
            try
            {
                //загрузка файла
                xml.Load("save.xml");
                //проходим по всем элементам игрока1
                foreach (XmlElement element in xml.GetElementsByTagName("MyUnit"))
                {
                    Units unit = new Units();
                    //проходим по всем атрибутам юнита и добавляем свойства объекту
                    foreach (XmlElement e in element)
                    {
                        if (e.Name == "Unit_name") unit.Unit_name = e.InnerText;
                        else if (e.Name == "Attack") unit.Attack = uint.Parse(e.InnerText);
                        else if (e.Name == "Defence") unit.Maximum_Damage = uint.Parse(e.InnerText);
                        else if (e.Name == "Maximum_Damage") unit.Maximum_Damage = uint.Parse(e.InnerText);
                        else if (e.Name == "Minimum_Damage") unit.Minimum_Damage = uint.Parse(e.InnerText);
                        else if (e.Name == "Health") unit.Health = double.Parse(e.InnerText, CultureInfo.InvariantCulture);
                        else if (e.Name == "Speed") unit.Speed = uint.Parse(e.InnerText);
                        else if (e.Name == "Growth") unit.Growth = uint.Parse(e.InnerText);
                        else if (e.Name == "AI_Value") unit.AI_Value = uint.Parse(e.InnerText);
                        else if (e.Name == "Gold") unit.Gold = uint.Parse(e.InnerText);
                        //если пользователь изменил название свойства
                        else throw (new XmlException());
                    }
                    //добавляем в массив игрока 1
                    PlayerUnitsList.Add(unit);
                }
                ////проходим по всем элементам игрока2
                foreach (XmlElement element in xml.GetElementsByTagName("BotUnit"))
                {
                    Units unit = new Units();
                    // проходим по всем атрибутам юнита и добавляем свойства объекту
                    foreach (XmlElement e in element)
                    {
                        if (e.Name == "Unit_name") unit.Unit_name = e.InnerText;
                        else if (e.Name == "Attack") unit.Attack = uint.Parse(e.InnerText);
                        else if (e.Name == "Defence") unit.Maximum_Damage = uint.Parse(e.InnerText);
                        else if (e.Name == "Maximum_Damage") unit.Maximum_Damage = uint.Parse(e.InnerText);
                        else if (e.Name == "Minimum_Damage") unit.Minimum_Damage = uint.Parse(e.InnerText);
                        else if (e.Name == "Health") unit.Health = double.Parse(e.InnerText, CultureInfo.InvariantCulture);
                        else if (e.Name == "Speed") unit.Speed = uint.Parse(e.InnerText);
                        else if (e.Name == "Growth") unit.Growth = uint.Parse(e.InnerText);
                        else if (e.Name == "AI_Value") unit.AI_Value = uint.Parse(e.InnerText);
                        else if (e.Name == "Gold") unit.Gold = uint.Parse(e.InnerText);
                        //если пользователь изменил название свойства
                        else throw (new XmlException());
                    }
                    ////добавляем в массив игрока 1
                    BotUnitsList.Add(unit);
                }
                //запускам сохраненную игру
                this.Hide();
                Fight fight = new Fight(PlayerUnitsList, BotUnitsList);
                fight.Show();
            }
            //если нет файла сохранения
            catch (FileNotFoundException)
            {
                MessageBox.Show("У вас нет доступного сохранения!");
            }
            //если пользователь изменил файл, что он перестал быть формата XML
            catch (XmlException)
            {
                MessageBox.Show("Файл поврежден!");
            }
            //если введены некорректные значения свойств
            catch (FormatException)
            {
                MessageBox.Show("В файл введены некорректные данные!");
            }
            //если введены некорректные значения свойств
            catch (OverflowException)
            {
                MessageBox.Show("В файл введены некорректные данные!");
            }
        }
    }
}
