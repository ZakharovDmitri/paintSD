using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_Paint
{
    public partial class ChildForm : Form
    {
        public enum Tool { Pencil, Ellipse, Line, Star, Eraser }

        private Point p1;
        private Point p2;

        public bool isChanged = false;

        private bool isMouseDown;

        private Tool curTool = Tool.Pencil;
        private Pen myPen = new Pen(Color.Black);
        private Point[] starPoints = new Point[10];

        public Bitmap Snapshot { get; set; }

        public Bitmap TempDraw { get; set; }

        public Effects SetEffects { get; set; }

        public bool IsRotated { get; set; }

        public string Path { get; set; }


        public ChildForm()
        {
            InitializeComponent();

            Snapshot = new Bitmap(drawPanel.ClientRectangle.Width, drawPanel.ClientRectangle.Height);
            SetEffects = new Effects(this);

            p1 = new Point();
            p2 = new Point();

            myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            myPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            var g = Graphics.FromImage(Snapshot);
            g.Clear(Color.White);
            g.Save();
        }

        public void SetTool(Tool tool) => curTool = tool; //Выбор инструмента

        public void SetColor(Color color) => myPen.Color = color; //Выбор цвета

        public void SetWidth(int width) => myPen.Width = width; //Выбор толщины

        public void SaveImage(string path) //Сохранение файла
        {
            try
            {
                Snapshot.Save(path);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void drawPanel_Paint(object sender, PaintEventArgs e) //Рисунок
        {
            if (TempDraw == null)
                return;

            var tempBmp = new Bitmap(TempDraw);
            var g = Graphics.FromImage(TempDraw);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (!isMouseDown)
            {
                e.Graphics.DrawImage(TempDraw, 0, 0, drawPanel.Width, drawPanel.Height);
                return;
            }

            switch (curTool)
            {
                case Tool.Pencil:
                    g.DrawLine(myPen, p1, p2);

                    p1.X = p2.X;
                    p1.Y = p2.Y;

                    e.Graphics.DrawImage(TempDraw, 0, 0, drawPanel.Width, drawPanel.Height);

                    break;
                case Tool.Ellipse:
                    g = Graphics.FromImage(tempBmp);

                    g.DrawEllipse(myPen, new RectangleF(
                        Math.Min(p1.X, p2.X),
                        Math.Min(p1.Y, p2.Y),
                        Math.Abs(p2.X - p1.X),
                        Math.Abs(p2.Y - p1.Y)));

                    e.Graphics.DrawImage(tempBmp, 0, 0, drawPanel.Width, drawPanel.Height);

                    return;
                case Tool.Line:
                    g = Graphics.FromImage(tempBmp);

                    g.DrawLine(myPen, p1, p2);
                    e.Graphics.DrawImage(tempBmp, 0, 0, drawPanel.Width, drawPanel.Height);

                    return;
                case Tool.Star:
                    Point middle = new Point(Math.Abs(p2.X / 2 + p1.X / 2), Math.Abs(p2.Y / 2 + p1.Y / 2));     //Задать центр звезды
                    int r = Math.Abs(middle.X - p2.X); //Радиус круга
                    double aoutR = -Math.PI / 2; //Начальный угол для внешних точек
                    double ainR = -Math.PI / 2 + 2 * Math.PI / 10; //Начальный угол для внутренних точек

                    for (int i = 0; i < 9; ++i)
                    {
                        starPoints[i++] = new Point(middle.X + (int)(r * Math.Cos(aoutR)), middle.Y + (int)(r * Math.Sin(aoutR)));
                        starPoints[i] = new Point(middle.X + (int)(r / 3 * Math.Cos(ainR)), middle.Y + (int)(r / 3 * Math.Sin(ainR)));
                        aoutR += 2 * Math.PI / 5; ainR += 2 * Math.PI / 5;
                    }

                    g = Graphics.FromImage(tempBmp);

                    for (int i = 0; i < 9; ++i)
                        g.DrawLine(myPen, starPoints[i], starPoints[i + 1]);

                    g.DrawLine(myPen, starPoints[9], starPoints[0]);
                    e.Graphics.DrawImage(tempBmp, 0, 0, drawPanel.Width, drawPanel.Height);

                    return;
                case Tool.Eraser:
                    var tempColor = myPen.Color;
                    myPen.Width *= 3;

                    myPen.Color = Color.White;
                    g.DrawLine(myPen, p1, p2);

                    myPen.Color = tempColor;
                    myPen.Width /= 3;

                    p1.X = p2.X;
                    p1.Y = p2.Y;

                    e.Graphics.DrawImage(TempDraw, 0, 0, drawPanel.Width, drawPanel.Height);

                    break;
            }
        }

        private void drawPanel_MouseUp(object sender, MouseEventArgs e) //Отклик на рисунок
        {
            if (e.Button != MouseButtons.Left || TempDraw == null)
                return;

            isMouseDown = false;

            var g = Graphics.FromImage(TempDraw);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            switch (curTool)
            {
                case Tool.Ellipse:
                    g.DrawEllipse(myPen, new RectangleF(
                        Math.Min(p1.X, p2.X),
                        Math.Min(p1.Y, p2.Y),
                        Math.Abs(p2.X - p1.X),
                        Math.Abs(p2.Y - p1.Y)));

                    g.Save();

                    break;
                case Tool.Star:
                    for (int i = 0; i < 9; i++)
                        g.DrawLine(myPen, starPoints[i], starPoints[i + 1]);

                    g.DrawLine(myPen, starPoints[9], starPoints[0]);
                    g.Save();

                    break;
                case Tool.Line:
                    g.DrawLine(myPen, p1, p2);
                    g.Save();

                    break;
            }

            if (TempDraw != null)
                Snapshot = (Bitmap)TempDraw.Clone();
            isChanged = true;
        }

        private void drawPanel_MouseMove(object sender, MouseEventArgs e) //Мышь на рисунке
        {
            if (isMouseDown)
            {
                var g = Graphics.FromImage(TempDraw);

                p2.X = (int)(e.X / (drawPanel.Width / (double)Snapshot.Width));
                p2.Y = (int)(e.Y / (drawPanel.Height / (double)Snapshot.Height));

                drawPanel.Invalidate();
                drawPanel.Refresh();
            }
        }

        private void drawPanel_MouseDown(object sender, MouseEventArgs e) //Клик на рисунок
        {
            if (e.Button != MouseButtons.Left)
                return;

            isMouseDown = true;

            p1.X = (int)(e.X / (drawPanel.Width / (double)Snapshot.Width));
            p1.Y = (int)(e.Y / (drawPanel.Height / (double)Snapshot.Height));

            TempDraw = (Bitmap)Snapshot.Clone();
        }

        private void ChildForm_Resize(object sender, EventArgs e) => drawPanel_Resize(sender, e); //Изменение размеров формы

        private void drawPanel_Resize(object sender, EventArgs e) //Изменение размеров рисунка
        {
            drawPanel.Invalidate();
            drawPanel.Refresh();
        }

        public void drawPanel_MouseWheel(object sender, MouseEventArgs e) //Колёсико
        {
            var coefX = Snapshot.Width * 0.1;
            var coefY = Snapshot.Height * 0.1;

            if (e.Delta > 0)
            {
                if (drawPanel.ClientRectangle.Width + coefX > 4096
                    || drawPanel.ClientRectangle.Height + coefY > 4096)
                    return;
            }
            else
            {
                if (drawPanel.MinimumSize.Width > drawPanel.ClientRectangle.Width - coefX
                    || drawPanel.MinimumSize.Height > drawPanel.ClientRectangle.Height - coefY)
                    return;

                coefX = -coefX;
                coefY = -coefY;
            }

            var offsetX = e.X - HorizontalScroll.Value;
            var offsetY = e.Y - VerticalScroll.Value;

            drawPanel.Width += (int)coefX;
            drawPanel.Height += (int)coefY;
        }

        private void ChildForm_FormClosing(object sender, FormClosingEventArgs e) //Закрытие формы
        {
            if (isChanged)
            {
                var d = MessageBox.Show("Сохранить изображение перед закрытием?", Text, MessageBoxButtons.YesNoCancel);

                if (d == DialogResult.Yes)
                {
                    var sfd = new SaveFileDialog
                    {
                        Title = "Сохранение изображения",
                        Filter = "Bitmap (*.bmp)|*bmp",
                        AddExtension = true,
                        DefaultExt = ".bmp"
                    };

                    if (sfd.ShowDialog() == DialogResult.OK)
                        SaveImage(sfd.FileName);
                }
                else if (d == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void ChildForm_Activated(object sender, EventArgs e)
        {
            ((MainForm)MdiParent).btnColor.BackColor = myPen.Color;
            ((MainForm)MdiParent).trackBar1.Value = (int)myPen.Width;

            switch (curTool)
            {
                case Tool.Ellipse:
                    ((MainForm)MdiParent).btnEllipse.Select();
                    break;
                case Tool.Star:
                    ((MainForm)MdiParent).btnStar.Select();
                    break;
                case Tool.Line:
                    ((MainForm)MdiParent).btnLine.Select();
                    break;
                case Tool.Eraser:
                    ((MainForm)MdiParent).btnEraser.Select();
                    break;
                case Tool.Pencil:
                    ((MainForm)MdiParent).btnPencil.Select();
                    break;
            }
        }
    }
}
