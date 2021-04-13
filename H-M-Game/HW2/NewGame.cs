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


namespace HW2
{
    public partial class NewGame : Form
    {
        //создаем необходимые нам листы
        static Random generator = new Random();
        List<Units> UnitsList = new List<Units>();
        List<Units> TempUnitsList = new List<Units>();
        List<Units> PlayerUnitsList = new List<Units>();
        List<Units> BotUnitsList = new List<Units>();
        int flag1 = 0, flag2 = 0, flag3 = 0;
        public NewGame()
        {
            //инициализируем лист юнитов
            UnitsList = GetUnits();
            InitializeComponent();
            //добавляем полученные лист в datagridview
            dataGridView1.DataSource = UnitsList;
        }
        /// <summary>
        /// увеличиваем форму на весь экран
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGame_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// для выхода из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// получение юнитов из csv файла
        /// </summary>
        /// <returns></returns>
        static List<Units> GetUnits()
        {
            List<Units> UnitsList = new List<Units>();
            //считываем из файла
            try
            {
                using (StreamReader sr = new StreamReader("HM3.csv"))
                {
                    sr.ReadLine();
                    do
                    {
                        //парсим по ;
                        string[] dataline = sr.ReadLine().Split(';');
                        //инициализируем новый юнит,парсим свойства из файла
                        Units unit = new Units
                        {
                            Unit_name = dataline[0],
                            Attack = uint.Parse(dataline[1]),
                            Defence = uint.Parse(dataline[2]),
                            Maximum_Damage = uint.Parse(dataline[3]),
                            Minimum_Damage = uint.Parse(dataline[4]),
                            Health = double.Parse(dataline[5]),
                            Speed = uint.Parse(dataline[6]),
                            Growth = uint.Parse(dataline[7]),
                            AI_Value = uint.Parse(dataline[8]),
                            Gold = uint.Parse(dataline[9]),
                        };
                        //добавляем юнита в лист юнитов
                        UnitsList.Add(unit);
                    }
                    while (!sr.EndOfStream);
                    return UnitsList;
                }
            }
            //если нет файла юнитов
            catch (FileNotFoundException)
            {
                MessageBox.Show("Нет файла с харктеристиками юнитов!");
                Application.Exit();
            }
            //если файл не в нужнои формате
            catch (IOException)
            {
                MessageBox.Show("Файл поврежден!");
                Application.Exit();
            }
            //если введены некорректные значения свойств
            catch (FormatException)
            {
                MessageBox.Show("В файл введены некорректные данные!");
                Application.Exit();
            }
            //если введены некорректные значения свойств
            catch (OverflowException)
            {
                MessageBox.Show("В файл введены некорректные данные!");
                Application.Exit();
            }
            return null;
        }
        /// <summary>
        /// очистка фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            TempUnitsList.Clear();
            dataGridView1.DataSource = UnitsList;
            flag1 = 0; flag2 = 0; flag3 = 0;
        }
        /// <summary>
        /// фильтр по атаке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //после нажатия на enter
            if (e.KeyCode == Keys.Enter)
            {
                //если этот фильтр еще не применялся
                if (flag1 == 0)
                {
                    //если до этого не было фильтров
                    if (TempUnitsList.Count == 0)
                    {
                        List<Units> tempList = new List<Units>();
                        //находим всех юнитов равных по атаке введенной с помощью linq
                        foreach (var unit in UnitsList.Where(x => Convert.ToString(x.Attack) == textBox1.Text))
                        {
                            //добавляем их во временный лист
                            tempList.Add(unit);
                        }
                        //обновляем значение временного листа
                        TempUnitsList = tempList;
                        //выводим временный лист(отфильтрованные юниты)
                        dataGridView1.DataSource = tempList;
                        //показываем, что этот фильтр уже применен
                        flag1++;
                    }
                    //если до этого уже были фильтры
                    else
                    {
                        List<Units> tempList = new List<Units>();
                        //находим всех юнитов равных по атаке введенной с помощью linq
                        foreach (var unit in TempUnitsList.Where(x => Convert.ToString(x.Attack) == textBox1.Text))
                        {
                            //добавляем во временный лсит
                            tempList.Add(unit);
                        }
                        //обновляем значение временного листа
                        TempUnitsList = tempList;
                        //выводим временный лист(отфильтрованные юниты)
                        dataGridView1.DataSource = tempList;
                        //показываем, что этот фильтр уже применен
                        flag1++;
                    }
                }
                //если этот фильтр уже применен
                else { MessageBox.Show("Сначала очистите предыдущие фильтры!"); }
            }
        }
        /// <summary>
        /// если пользователь ввел некорректное значение свойства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Введите неотрицательное целочисленное значение, (Health может быть дробным > 0)");
        }

        /// <summary>
        /// если пользователь поставил health <=0
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                double.TryParse(e.FormattedValue.ToString(), out double temp);
                if (temp <= 0)
                {
                    MessageBox.Show("Введите коррекктное значение Health (дробь,больше 0), возможно " +
                        "неправильный разделитель между дробной и целой частями");
                    e.Cancel = true;
                }
            }
        }
        /// <summary>
        /// выбор юнитов в команду
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //если выбирается строка где показаны юниты
            if (e.RowIndex != -1)
            {
                //если команда еще неукомплектована
                if (PlayerUnitsList.Count < 5)
                {
                    //если не применены фильтры
                    if (TempUnitsList.Count == 0)
                    {
                        //добавляем выбранного юнита в команду и записываем его в лейбл
                        PlayerUnitsList.Add(new Units(UnitsList[e.RowIndex]));
                        label6.Text += UnitsList[e.RowIndex].Unit_name + "\n";
                    }
                    //если применены фильтры
                    else
                    {
                        //добавляем выбранного юнита в команду и записываем его в лейбл
                        PlayerUnitsList.Add(TempUnitsList[e.RowIndex]);
                        label6.Text += TempUnitsList[e.RowIndex].Unit_name + "\n";
                    }
                }
            }
        }
        /// <summary>
        /// кнопка пересобрать команду
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            PlayerUnitsList.Clear();
            label6.Text = "Ваша команда\n";
        }
        /// <summary>
        /// собираем команду для бота
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (PlayerUnitsList.Count == 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    //генерируем случайного юнита из списка
                    BotUnitsList.Add(new Units(UnitsList[generator.Next(0, UnitsList.Count())]));
                    //проверяем не совпадает ли он с юнитом игрока1,если совпадает удаляем и генерируем заново
                    foreach (var unit in PlayerUnitsList)
                    {
                        if (BotUnitsList[i].Unit_name == unit.Unit_name)
                        {
                            BotUnitsList.Remove(BotUnitsList[i]);
                            i--;
                            break;
                        }
                    }
                }
                //запускаем форму для боя
                this.Hide();
                Fight fight = new Fight(PlayerUnitsList, BotUnitsList);
                fight.Show();
            }
        }
        /// <summary>
        /// фильтр по скорости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //после нажатия на enter
            if (e.KeyCode == Keys.Enter)
            {
                //если этот фильтр еще не применялся
                if (flag2 == 0)
                {
                    //если до этого не было фильтров
                    if (TempUnitsList.Count == 0)
                    {
                        List<Units> tempList = new List<Units>();
                        //находим всех юнитов равных по атаке введенной с помощью linq
                        foreach (var unit in UnitsList.Where(x => Convert.ToString(x.Speed) == textBox2.Text))
                        {
                            //добавляем во временный лист
                            tempList.Add(unit);
                        }
                        //обновляем значение временного листа
                        TempUnitsList = tempList;
                        //выводим отфильтрованный лист
                        dataGridView1.DataSource = tempList;
                        //указываем что фильтр применен
                        flag2++;
                    }
                    //если до этого уже были фильтры
                    else
                    {
                        List<Units> tempList = new List<Units>();
                        //находим всех юнитов равных по атаке введенной с помощью linq
                        foreach (var unit in TempUnitsList.Where(x => Convert.ToString(x.Speed) == textBox2.Text))
                        {
                            //добавялем во временный лист
                            tempList.Add(unit);
                        }
                        //обновляем значение временного листа
                        TempUnitsList = tempList;
                        //выводим отфильтрованный лист
                        dataGridView1.DataSource = tempList;
                        //указываем что фильтр применен
                        flag2++;
                    }
                }
                //если этот фильтр уже применен
                else { MessageBox.Show("Сначала очистите предыдущие фильтры!"); }
            }
        }
        /// <summary>
        /// фильтр по золоту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            //после нажатия на enter
            if (e.KeyCode == Keys.Enter)
            {
                //если этот фильтр уже применялся
                if (flag3 == 0)
                {
                    //если до этого не было фильтров
                    if (TempUnitsList.Count == 0)
                    {
                        List<Units> tempList = new List<Units>();
                        //находим всех юнитов равных по атаке введенной с помощью linq
                        foreach (var unit in UnitsList.Where(x => Convert.ToString(x.Gold) == textBox3.Text))
                        {
                            //добавялем во временный лист
                            tempList.Add(unit);
                        }
                        //обновляем значение временного листа
                        TempUnitsList = tempList;
                        //выводим отфильтрованный лист
                        dataGridView1.DataSource = tempList;
                        //указываем что фильтр применен
                        flag3++;
                    }
                    //если до этого были фильтры
                    else
                    {
                        List<Units> tempList = new List<Units>();
                        //находим всех юнитов равных по атаке введенной с помощью linq
                        foreach (var unit in TempUnitsList.Where(x => Convert.ToString(x.Gold) == textBox3.Text))
                        {
                            //добавялем во временный лист
                            tempList.Add(unit);
                        }
                        //обновляем значение временного листа
                        TempUnitsList = tempList;
                        //выводим отфильтрованный лист
                        dataGridView1.DataSource = tempList;
                        //указываем что фильтр применен
                        flag3++;
                    }
                }
                //если этот фильтр уже применен
                else { MessageBox.Show("Сначала очистите предыдущие фильтры!"); }
            }
        }

    }
}

