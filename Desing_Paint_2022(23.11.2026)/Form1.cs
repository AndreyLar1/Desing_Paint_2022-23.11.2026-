using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desing_Paint_2022_23._11._2026_
{
    public partial class Form1 : Form
    {
        Bitmap bmp; // вирутальный лист в оперативной памяти по пикселям 
        Graphics g; // инструмент который умеет рисовать линии и фигуры 
        Pen pen = new Pen(Color.Black, 3); // ручка черный цвет толщина 3 пикселя
        bool isDrawing = false; // зажата ли кнопка мыши сейчас 
        Point previousPoint; // координаты точки где курсор находился мгновение назад 

        Color currentColor = Color.Black; // запоминаем выбранный цвет кисти 

        string currentTool = "Pen";
        Point currentPoint; // текущие координаты мыши
        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(picCanvas.Width, picCanvas.Height); // создаем пустой лист по высоте и ширине нашего холста
            g = Graphics.FromImage(bmp); // привязываем художника к листу 
            g.Clear(Color.White); // заливаем весь лист сплошным белым(по умолчанию прозрачный) 
            picCanvas.Image = bmp; // выводим вирутальный лист на холст 

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round; // делаем начало каждого штриха круглым 
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round; // делаем конец каждого штриха круглым 
        }


        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // проверяем что нажата левая кнопка мыши 
            {
                isDrawing = true; // включаем режим рисования 
                previousPoint = e.Location; // запоминаем стартовые координаты 
            }
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing) // если кнопка мыши сейчас зажата 
            {
                g.DrawLine(pen, previousPoint, e.Location); // проводим короткий прямой отрезок от прошлой точки до текущей позиции мыши 
                previousPoint = e.Location; // перезаписываем прошлую точку на текущуюю 
                picCanvas.Invalidate(); // заставляем холст обновлять картинку что бы линия была сразу видна 
            }
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false; // отпустили кнопку мыши выключаем режим рисования 
        }

        private void BtnKarandash_Click(object sender, EventArgs e) // 
        {
            pen.Color = currentColor; // возвращаем ручке сохраненный цвет (после ластика) 

            currentTool = "Pen";
            pen.Color = currentColor;
        }

        private void BtnLastik_Click(object sender, EventArgs e)
        {
            pen.Color = Color.White; // та же кисть только белого цвета 

            currentTool = "Pen";
            pen.Color = Color.White;
        }

        private void BtnClear_Click(object sender, EventArgs e) // кнопка очистить 
        {
            g.Clear(Color.White); // заново заливаем весь лист белым 
            picCanvas.Invalidate(); // обновляем картинку на экране что бы очистка сразу отобразилась
        }

        private void trackBarSize_Scroll(object sender, EventArgs e) // ползунок толщины 
        {
            pen.Width = trackBarSize.Value; // устанавливаем толщину ручки для текущего значения ползунка 
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK) // открываем окно выбора цвета и ждем нажатия кнопки ок
            {
                currentColor = colorDialog1.Color; // запоминаем новый выбранный цвет
                pen.Color = currentColor; // сразу красим ручку в этот цвет

            }
        }
        private void PaletteButton_Click(object sender, EventArgs e) // метод для квадратиков цвета 
        {
            Button clickedButton = (Button)sender;   // узнаём, на какой именно цветной квадратик нажали
            currentColor = clickedButton.BackColor;  // берём цвет фона этой кнопки
            pen.Color = currentColor;                // красим ручку в этот цвет
            btnColor.BackColor = currentColor;       // обновляем индикатор на кнопке Цвет
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
           saveFileDialog1.Filter = "PNG Image|*.png|JPEG Image|*.jpg"; // форматы файлов для сохранения
           if (saveFileDialog1.ShowDialog() == DialogResult.OK) // если пользователь выбрал путь и нажал «Сохранить»
           {
               bmp.Save(saveFileDialog1.FileName); // сохраняем наш виртуальный лист bmp на диск
           }
        }
    }
}
