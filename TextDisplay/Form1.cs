using System.Runtime.Serialization.Json;
using System.Text;
//using System.Text.Json;
using Microsoft.VisualBasic;

namespace TextDisplay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �t�H�[���̈ړ�
        /// </summary>
        /// <param name="eX"></param>
        /// <param name="eY"></param>
        private void FormMove(int eX, int eY)
        {
            // �ړ���̃t�H�[���ʒu
            Rectangle newPosition =
                new(Left + eX, Top + eY, Width, Height);

            // ��ʂ̒[�ɋz�������Ĕz�u����
            if (snapAssistToolStripMenuItem.Checked)
            {
                // �z������T�C�Y
                Size gap = new(16, 16);

                // ����p��RECT
                Rectangle newRect;

                // ��Ɨ̈�̎擾�i���̍�Ɨ̈�̓����ɋz������j
                Size area = new(
                    Screen.FromControl(label1).WorkingArea.Width,
                    Screen.FromControl(label1).WorkingArea.Height);

                // ��Ɨ̈�̍�����̍��W�̎擾
                Point wp = Screen.FromControl(label1).WorkingArea.Location;

                // ��ʒ[�̔���p�i��ʂ̒[�̈ʒu�ɁA�z������T�C�Y����RECT���`����j
                Rectangle rectLeft =
                    new(wp.X, wp.Y, gap.Width, area.Height);
                Rectangle rectTop =
                    new(wp.X, wp.Y, area.Width, gap.Height);
                Rectangle rectRight =
                    new(area.Width - gap.Width + wp.X, wp.Y, gap.Width, area.Height);
                Rectangle rectBottom =
                    new(wp.X, area.Height - gap.Height + wp.Y, area.Width, gap.Height);

                // �Փ˔���
                // ����p��RECT�������̃E�B���h�E�̋��ɏd�˂�悤�Ɉړ����A
                // ��ʒ[�̔���p��RECT�ƏՓ˂��Ă��邩�`�F�b�N����B
                // �Փ˂��Ă����ꍇ�́A�z��������悤�Ɉړ�����

                // ���[�Փ˔���
                {
                    newRect = newPosition;
                    newRect.Width = gap.Width;

                    if (newRect.IntersectsWith(rectLeft))
                    {
                        // ���[�ɋz��������
                        newPosition.X = wp.X;
                    }
                }
                // �E�[�Փ˔���
                {
                    newRect = newPosition;
                    newRect.X = newPosition.Right - gap.Width;  // �E�B���h�E�̉E��
                    newRect.Width = gap.Width;

                    if (newRect.IntersectsWith(rectRight))
                    {
                        // �E�[�ɋz��������
                        newPosition.X = area.Width - Width + wp.X;
                    }
                }
                // ��[�Փ˔���
                {
                    newRect = newPosition;
                    newRect.Height = gap.Height;

                    if (newRect.IntersectsWith(rectTop))
                    {
                        // ��[�ɋz��������
                        newPosition.Y = wp.Y;
                    }
                }
                // ���[�Փ˔���
                {
                    newRect = newPosition;
                    newRect.Y = newPosition.Bottom - gap.Height; // �E�B���h�E�̉��[
                    newRect.Height = gap.Height;

                    if (newRect.IntersectsWith(rectBottom))
                    {
                        // ���[�ɋz��������
                        newPosition.Y = area.Height - Height + wp.Y;
                    }
                }
            }

            // ���ۂɈړ�������
            Left = newPosition.Left;
            Top = newPosition.Top;
        }

        /// <summary>
        /// �t�H�[���̃T�C�Y�ύX
        /// </summary>
        private void FormSizeChange()
        {
            //������̑傫�����v�����ăT�C�Y�ύX
            Size size = TextRenderer.MeasureText(label1.Text, label1.Font);
            if (size.Width == 0)
            {
                size.Width = 20;
                size.Height= label1.Height;
            }
            Size = size;
        }

        ///�}�E�X�̃N���b�N�ʒu���L��
        private Point mousePoint;

        ///Form1��MouseDown�C�x���g�n���h��
        ///�}�E�X�̃{�^���������ꂽ�Ƃ�
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //�ʒu���L������
                mousePoint = new Point(e.X, e.Y);
            }
        }

        ///Form1��MouseMove�C�x���g�n���h��
        ///�}�E�X���������Ƃ�
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                FormMove(e.X - mousePoint.X, e.Y - mousePoint.Y);
            }
        }

        private void SnapAssistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            snapAssistToolStripMenuItem.Checked = !snapAssistToolStripMenuItem.Checked;
        }

        private void TopmostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            topmostToolStripMenuItem.Checked = TopMost;
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = label1.Font;
            fontDialog1.Color = label1.ForeColor;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.ForeColor = fontDialog1.Color;
                label1.Font = fontDialog1.Font;
                FormSizeChange();
            }
        }

        private void ForeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = label1.ForeColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.ForeColor = colorDialog1.Color;
            }
        }

        private void BackColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                BackColor = colorDialog1.Color;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var param = Deserialize<ParamLabel?>("TextDisplay.config.json");
            Set(param);
        }

        private void Set(ParamLabel? paramLabel)
        {
            if (paramLabel is not null)
            {
                param = paramLabel;
                label1.Text = param.Text;
                label1.ForeColor = param.ForeColor;
                BackColor = param.BackColor;
                label1.Font = new Font(param.Font.FontFamily, param.Font.Size);
                toolStripTextBox1.Text = param.Text;
            }
            FormSizeChange();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            param = new ParamLabel
            {
                Text = label1.Text,
                ForeColor = label1.ForeColor,
                BackColor = BackColor,
                Font = new ParamTextFont
                {
                    FontFamily = label1.Font.FontFamily.Name,
                    Size = label1.Font.Size,
                    Bold = label1.Font.Bold,
                    Italic = label1.Font.Italic,
                    Strikeout = label1.Font.Strikeout,
                    Underline = label1.Font.Underline,
                },
            };
            Serialize(param, "TextDisplay.config.json");
            Close();
        }

        /// <summary>
        /// �������ݗp
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="jsonfile"></param>
        private static void Serialize<T>(T data, string jsonfile)
        {
            //var options = new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //};
            //using var stream = new FileStream(jsonfile, FileMode.Create, FileAccess.Write);
            //JsonSerializer.SerializeAsync(stream, data, options);
            using var fs = new FileStream(jsonfile, FileMode.Create);
            using var writer = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, true, true, "  ");
            var serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(writer, data);
        }
        /// <summary>
        /// �ǂݍ��ݗp
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonfile"></param>
        /// <returns></returns>
        private static T? Deserialize<T>(string jsonfile)
        {
            try
            {
                //var jsonString = File.ReadAllText(jsonfile);
                //return JsonSerializer.Deserialize<T>(jsonString);
                using var ms = new FileStream(jsonfile, FileMode.Open);
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T?)serializer.ReadObject(ms);
            }
            catch (Exception)
            {
                return default;
            }
        }

        private ParamLabel param = new();

        public class ParamLabel
        {
            public string Text { get; set; } = "Right click to edit this message.";
            public Color ForeColor { get; set; } = Color.GhostWhite;
            public Color BackColor { get; set; } = Color.MediumBlue;
            public ParamTextFont Font { get; set; } = new ParamTextFont();
        }
        public class ParamTextFont
        {
            public string FontFamily { get; set; } = "Yu Gothic UI";
            public float Size { get; set; } = 24;
            public bool Bold { get; set; } = false;
            public bool Italic { get; set; } = false;
            public bool Strikeout { get; set; } = false;
            public bool Underline { get; set; } = false;
        }

        private void TextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var str = Interaction.InputBox("", "Text", label1.Text);
            if (str != "")
            {
                label1.Text = str;
                FormSizeChange();
            }
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Set(new ParamLabel());
        }

        private void ToolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = toolStripTextBox1.Text;
            FormSizeChange();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            (label1.ForeColor, BackColor) = (BackColor, label1.ForeColor);
        }

        private void BlinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = blinkToolStripMenuItem.Checked = !blinkToolStripMenuItem.Checked;
        }
    }
}