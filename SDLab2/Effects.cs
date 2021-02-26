using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab2_Paint
{
    public class Effects
    {
        private ChildForm form;

        public Effects(ChildForm childForm)
        {
            form = childForm;
        }

        public void Circuit() //Контуры
        {
            if (form.TempDraw == null)
                return;

            var tempBmp = new Bitmap(form.TempDraw);

            int i, j;
            int DispX = 1, DispY = 1;
            int red, green, blue;
            var oldText = form.Text;

            for (i = 0; i < tempBmp.Height - 2; i++)
            {
                for (j = 0; j < tempBmp.Width - 2; j++)
                {
                    Color pixel1 = tempBmp.GetPixel(j, i),
                          pixel2 = tempBmp.GetPixel(j + DispX, i + DispY);

                    red = Math.Min(Math.Abs(pixel1.R - pixel2.R) + 128, 255);
                    green = Math.Min(Math.Abs(pixel1.G - pixel2.G) + 128, 255);
                    blue = Math.Min(Math.Abs(pixel1.B - pixel2.B) + 128, 255);

                    form.TempDraw.SetPixel(j, i, Color.FromArgb(red, green, blue));
                }

                if (i % 10 == 0)
                {
                    form.Text = "Контур - " + Math.Truncate(100 * i / (tempBmp.Height - 2.0)).ToString() + "%";
                    form.drawPanel.Invalidate();
                    form.drawPanel.Refresh();
                }
            }

            form.Text = oldText;
            form.Snapshot = form.TempDraw;
            form.drawPanel.Invalidate();
            form.drawPanel.Refresh();
        }

        public void Sharpness() //Резкость
        {
            if (form.TempDraw == null)
                return;

            var tempBmp = new Bitmap(form.TempDraw);

            int DY = 1, DX = 1;
            int i, j;
            int red, green, blue;
            var oldText = form.Text;

            for (i = DX; i < tempBmp.Height - DX - 1; i++)
            {
                for (j = DY; j < tempBmp.Width - DY - 1; j++)
                {
                    red = (int)(tempBmp.GetPixel(j, i).R + 0.5 * tempBmp.GetPixel(j, i).R
                        - tempBmp.GetPixel(j - DX, i - DY).R);
                    green = (int)(tempBmp.GetPixel(j, i).G + 0.7 * tempBmp.GetPixel(j, i).G
                        - tempBmp.GetPixel(j - DX, i - DY).G);
                    blue = (int)(tempBmp.GetPixel(j, i).B + 0.5 * tempBmp.GetPixel(j, i).B
                        - tempBmp.GetPixel(j - DX, i - DY).B);

                    red = Math.Min(Math.Max(red, 0), 255);
                    green = Math.Min(Math.Max(green, 0), 255);
                    blue = Math.Min(Math.Max(blue, 0), 255);

                    form.TempDraw.SetPixel(j, i, Color.FromArgb(red, green, blue));
                }

                if (i % 10 == 0)
                {
                    form.Text = "Резкость - " + Math.Truncate(100 * i / (tempBmp.Height - 2.0)).ToString() + "%";
                    form.drawPanel.Invalidate();
                    form.drawPanel.Refresh();
                }
            }

            form.Text = oldText;
            form.Snapshot = form.TempDraw;
            form.drawPanel.Invalidate();
            form.drawPanel.Refresh();
        }

        public void Blur() //Размытие
        {
            if (form.TempDraw == null)
                return;

            var tempBmp = new Bitmap(form.TempDraw);
            int DY = 1, DX = 1;
            int i, j;
            int red, green, blue;
            var oldText = form.Text;

            for (i = DX; i < tempBmp.Height - DX - 1; i++)
            {
                for (j = DY; j < tempBmp.Width - DY - 1; j++)
                {
                    red = (tempBmp.GetPixel(j - 1, i - 1).R + tempBmp.GetPixel(j - 1, i).R
                        + tempBmp.GetPixel(j - 1, i + 1).R + tempBmp.GetPixel(j, i - 1).R
                        + tempBmp.GetPixel(j, i).R + tempBmp.GetPixel(j, i + 1).R
                        + tempBmp.GetPixel(j + 1, i - 1).R + tempBmp.GetPixel(j + 1, i).R
                        + tempBmp.GetPixel(j + 1, i + 1).R) / 9;

                    green = (tempBmp.GetPixel(j - 1, i - 1).G + tempBmp.GetPixel(j - 1, i).G
                        + tempBmp.GetPixel(j - 1, i + 1).G + tempBmp.GetPixel(j, i - 1).G
                        + tempBmp.GetPixel(j, i).G + tempBmp.GetPixel(j, i + 1).G
                        + tempBmp.GetPixel(j + 1, i - 1).G + tempBmp.GetPixel(j + 1, i).G
                        + tempBmp.GetPixel(j + 1, i + 1).G) / 9;

                    blue = (tempBmp.GetPixel(j - 1, i - 1).B + tempBmp.GetPixel(j - 1, i).B
                        + tempBmp.GetPixel(j - 1, i + 1).B + tempBmp.GetPixel(j, i - 1).B
                        + tempBmp.GetPixel(j, i).B + tempBmp.GetPixel(j, i + 1).B
                        + tempBmp.GetPixel(j + 1, i - 1).B + tempBmp.GetPixel(j + 1, i).B
                        + tempBmp.GetPixel(j + 1, i + 1).B) / 9;

                    red = Math.Min(Math.Max(red, 0), 255);
                    green = Math.Min(Math.Max(green, 0), 255);
                    blue = Math.Min(Math.Max(blue, 0), 255);

                    form.TempDraw.SetPixel(j, i, Color.FromArgb(red, green, blue));
                }
                if (i % 10 == 0)
                {
                    form.Text = "Размытие - " + Math.Truncate(100 * i / (tempBmp.Height - 2.0)).ToString() + "%";
                    form.drawPanel.Invalidate();
                    form.drawPanel.Refresh();
                }
            }

            form.Text = oldText;
            form.Snapshot = form.TempDraw;
            form.drawPanel.Invalidate();
            form.drawPanel.Refresh();
        }

        public void SharpEdges() //Резкие границы
        {
            var rnd = new Random();

            var tempBmp = new Bitmap(form.TempDraw);

            int DX = 1, DY = 1;
            int red, green, blue;
            var oldText = form.Text;

            for (int i = 3; i < tempBmp.Height - 3; i++)
            {
                for (int j = 3; j < tempBmp.Width - 3; j++)
                {

                    DX = (int)(rnd.NextDouble() * 4 - 2);
                    DY = (int)(rnd.NextDouble() * 4 - 2);

                    red = tempBmp.GetPixel(j + DX, i + DY).R;
                    green = tempBmp.GetPixel(j + DX, i + DY).G;
                    blue = tempBmp.GetPixel(j + DX, i + DY).B;
                    form.TempDraw.SetPixel(j, i, Color.FromArgb(red, green, blue));
                }

                if (i % 10 == 0)
                {
                    form.Text = "Резкие края - " + Math.Truncate(100 * i / (tempBmp.Height - 2.0)).ToString() + "%";
                    form.drawPanel.Invalidate();
                    form.drawPanel.Refresh();
                }
            }

            form.Text = oldText;
            form.Snapshot = form.TempDraw;
            form.drawPanel.Invalidate();
            form.drawPanel.Refresh();
        }

        public void RotateLeft() //Поворот влево
        {
            form.IsRotated = !form.IsRotated;
            form.TempDraw.RotateFlip(RotateFlipType.Rotate270FlipNone);
            form.Snapshot = form.TempDraw;

            var temp = form.drawPanel.Width;
            form.drawPanel.Width = form.drawPanel.Height;
            form.drawPanel.Height = temp;
        }

        public void RotateRight() //Пооворот вправо
        {
            form.IsRotated = !form.IsRotated;
            form.TempDraw.RotateFlip(RotateFlipType.Rotate90FlipNone);
            form.Snapshot = form.TempDraw;

            var temp = form.drawPanel.Width;
            form.drawPanel.Width = form.drawPanel.Height;
            form.drawPanel.Height = temp;
        }

        public void MirrorX() //Отразить по горизонтали
        {
            form.TempDraw.RotateFlip(RotateFlipType.RotateNoneFlipX);
            form.Snapshot = form.TempDraw;

            form.drawPanel.Invalidate();
            form.drawPanel.Refresh();
        }

        public void MirrorY() //Отразить по вертикали
        {
            form.TempDraw.RotateFlip(RotateFlipType.RotateNoneFlipY);
            form.Snapshot = form.TempDraw;

            form.drawPanel.Invalidate();
            form.drawPanel.Refresh();
        }
    }
}
