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
    public partial class MainForm : Form
    {
        int i = 1;

        public MainForm()
        {
            InitializeComponent();

            btnColor.BackColor = Color.Black;
        }

        #region Работа с файлами

        private void ShowNewForm(object sender, EventArgs e) //Создание нового файла
        {
            var childForm = new ChildForm
            {
                MdiParent = this,
                Text = "Безымянный" + i,
                MaximizeBox = true
            };

            childForm.Show();
            childForm.SetColor(Color.Black);
            childForm.SetWidth(trackBar1.Value);
            i++;
        }

        private void OpenFile(object sender, EventArgs e) //Отркрытие файла
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                Title = "Открытие файл",
                Filter = "Bitmap (*.bmp)|*bmp| JPEG (*.jpeg)|*jpeg| Все файлы (*.*)|(*.*)",
                AddExtension = true
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                var childForm = new ChildForm
                {
                    MdiParent = this,
                    Path = ofd.FileName,
                    Text = ofd.FileName,
                    MaximizeBox = true
                };

                childForm.Show();
                childForm.TempDraw = new Bitmap(Image.FromFile(ofd.FileName));
                childForm.Snapshot = childForm.TempDraw;
                childForm.SetColor(btnColor.BackColor);
                childForm.SetWidth(trackBar1.Value);

                childForm.drawPanel.Invalidate();
                childForm.drawPanel.Refresh();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) //Кнопка сохранить
        {
            if (!(ActiveMdiChild is ChildForm child))
                return;

            if (child.Path != null)
                child.SaveImage(child.Path);
            else
                SaveAsToolStripMenuItem_Click(sender, e);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e) //Кнопка сохранить как
        {
            if (!(ActiveMdiChild is ChildForm child))
                return;

            var sfd = new SaveFileDialog
            {
                Title = "Сохранение изображения",
                Filter = "Bitmap (*.bmp)|*bmp",
                AddExtension = true,
                DefaultExt = ".bmp"
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                child.Path = sfd.FileName;
                child.SaveImage(child.Path);
                child.Text = sfd.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e) //Кнопка выхода
        {
            this.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e) //Каскад
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e) //По вертикали
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e) //По горизонтали
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e) //Упорядочить
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e) //Закрытие формы
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        #endregion

        

        

        private void усилениеРезкостиToolStripMenuItem_Click(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.SetEffects.Sharpness();

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.SetEffects.Blur();



        private void повернутьВлевоToolStripMenuItem_Click(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.SetEffects.RotateLeft();

        private void повернутьВправоToolStripMenuItem_Click(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.SetEffects.RotateRight();

        

        

        

        private void btnPencil_Click(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.SetTool(ChildForm.Tool.Pencil);

        private void btnEllipse_Click(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.SetTool(ChildForm.Tool.Ellipse);

        private void btnLine_Click(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.SetTool(ChildForm.Tool.Line);

        private void btnStar_Click(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.SetTool(ChildForm.Tool.Star);

        private void btnEraser_Click(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.SetTool(ChildForm.Tool.Eraser);

        private void btnColor_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                (ActiveMdiChild as ChildForm)?.SetColor(colorDialog.Color);
                btnColor.BackColor = colorDialog.Color;
            }
        }

        private void btnZoomIn_Click(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.drawPanel_MouseWheel(this, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 1));

        private void btnZoomOut_Click(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.drawPanel_MouseWheel(this, new MouseEventArgs(MouseButtons.None, 0, 0, 0, -1));

        private void trackBar1_Scroll(object sender, EventArgs e) =>
            (ActiveMdiChild as ChildForm)?.SetWidth(trackBar1.Value);

       
    }
}
