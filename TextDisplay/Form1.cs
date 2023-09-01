using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.VisualBasic;

namespace TextDisplay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region �t�H�[���̈ړ��ƕ\���֌W
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
        /// �}�E�X�̃N���b�N�ʒu���L��
        /// </summary>
        private Point mousePoint;

        /// <summary>
        /// Form1��MouseDown�C�x���g�n���h��
        /// �}�E�X�̃{�^���������ꂽ�Ƃ�
        /// </summary>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //�ʒu���L������
                mousePoint = new Point(e.X, e.Y);
            }
        }

        /// <summary>
        /// Form1��MouseMove�C�x���g�n���h��
        /// �}�E�X���������Ƃ�
        /// </summary>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                FormMove(e.X - mousePoint.X, e.Y - mousePoint.Y);
            }
        }

        /// <summary>
        /// �t�H�[���ړ����ɉ�ʒ[�ɋz�������邩�ǂ�����ύX����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnapAssistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            snapAssistToolStripMenuItem.Checked = !snapAssistToolStripMenuItem.Checked;
        }

        /// <summary>
        /// �t�H�[�����őO�ʂɕ\�������邩�ǂ�����ύX����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopmostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            topmostToolStripMenuItem.Checked = TopMost = !TopMost;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            (label1.ForeColor, BackColor) = (BackColor, label1.ForeColor);
        }

        private void BlinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = blinkToolStripMenuItem.Checked = !blinkToolStripMenuItem.Checked;
        }
        #endregion

        #region �e�L�X�g�̃f�U�C���֌W
        /// <summary>
        /// �t�H�[���̃T�C�Y���R���g���[���̃T�C�Y�ɕύX
        /// </summary>
        private void FormSizeChange()
        {
            Size size = label1.Size;
            if (size.Width == 0)
            {
                size.Width = 20;
                size.Height = label1.Height;
            }
            Size = size;
        }

        /// <summary>
        /// �ݒ�𔽉f������
        /// </summary>
        /// <param name="config"></param>
        private void SetConfig(Configuration? config)
        {
            if (config is not null)
            {
                label1.Text = config.Text;
                label1.ForeColor = config.ForeColor;
                base.BackColor = config.BackColor;
                label1.Font = new Font(config.Font.FontFamily, config.Font.Size);
                label1.Padding = new Padding(config.Padding);
                toolStripTextBoxPadding.Text = config.Padding.ToString();
                snapAssistToolStripMenuItem.Checked = config.SnapAssist;
                topmostToolStripMenuItem.Checked = TopMost = config.TopMost;
                blinkToolStripMenuItem.Checked = timer1.Enabled = config.Blink;
                autoSaveToolStripMenuItem.Checked = config.AutoSave;
            }
            toolStripTextBox1.Text = label1.Text;
            FormSizeChange();
        }

        /// <summary>
        /// �ݒ�����Z�b�g����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetConfig(new Configuration());
        }

        /// <summary>
        /// �t�H���g�_�C�A���O��\�����ăt�H���g��ύX����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// �J���[�_�C�A���O��\�����ĕ����F��ύX����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = label1.ForeColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.ForeColor = colorDialog1.Color;
            }
        }

        /// <summary>
        /// �J���[�_�C�A���O��\�����Ĕw�i�F��ύX����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                BackColor = colorDialog1.Color;
            }
        }

        /// <summary>
        /// �e�L�X�g�{�b�N�X���g���ĕ\������e�L�X�g��ύX����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = toolStripTextBox1.Text;
            FormSizeChange();
        }

        /// <summary>
        /// VB�̃e�L�X�g���̓t�H�[���R���g���[�����g���ăe�L�X�g��ύX����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // �L�����Z���{�^������������߂�l��""�ɂȂ�
            var str = Interaction.InputBox("", "Text", label1.Text);
            // ""�̏ꍇ�͕ύX���Ȃ�
            if (str != "")
            {
                label1.Text = str;
                FormSizeChange();
            }
        }

        /// <summary>
        /// Padding��ύX����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripTextBoxPadding_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(toolStripTextBoxPadding.Text, out int val))
            {
                label1.Padding = new Padding(val);
                FormSizeChange();
                padding = val;
            }
            else if (toolStripTextBoxPadding.Text == "")
            {
                toolStripTextBoxPadding.Text = "0";
            }
            else
            {
                toolStripTextBoxPadding.Text = padding.ToString();
            }
        }

        /// <summary>
        /// Padding�̐ݒ�l
        /// </summary>
        private int padding = 0;

        /// <summary>
        /// Padding�̐ݒ莞�Ɂ����������ꂽ�Ƃ��͒l�� +1�C-1 �ɂ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripTextBoxPadding_KeyDown(object sender, KeyEventArgs e)
        {
            if (int.TryParse(toolStripTextBoxPadding.Text, out int val))
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (val < 100)
                    {
                        toolStripTextBoxPadding.Text = (val + 1).ToString();
                    }
                }
                else if (e.KeyCode == Keys.Down && val > 0)
                {
                    toolStripTextBoxPadding.Text = (val - 1).ToString();
                }
            }
        }
        #endregion

        #region �V���A���C�Y�֌W
        /// <summary>
        /// �t�H�[�����N������Ƃ��ɐݒ�t�@�C����ǂݍ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // �N�����ɐݒ�t�@�C����ǂݍ���
            var param = DeSerialize<Configuration?>(configName);
            SetConfig(param);
        }

        /// <summary>
        /// �t�H�[�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// �t�H�[�������Ƃ��ɐݒ�t�@�C������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // AutoSave�Ƀ`�F�b�N�������Ă�����ۑ�����
            if (autoSaveToolStripMenuItem.Checked)
            {
                // �I�����ɐݒ�t�@�C����json�ŕۑ�����
                var config = new Configuration
                {
                    Text = label1.Text,
                    ForeColor = label1.ForeColor,
                    BackColor = BackColor,
                    Font = new ParamFont
                    {
                        FontFamily = label1.Font.FontFamily.Name,
                        Size = label1.Font.Size,
                        Bold = label1.Font.Bold,
                        Italic = label1.Font.Italic,
                        Strikeout = label1.Font.Strikeout,
                        Underline = label1.Font.Underline,
                    },
                    Padding = label1.Padding.All,
                    SnapAssist = snapAssistToolStripMenuItem.Checked,
                    TopMost = topmostToolStripMenuItem.Checked,
                    Blink = blinkToolStripMenuItem.Checked,
                    AutoSave = autoSaveToolStripMenuItem.Checked,
                };
                // �ݒ肪�f�t�H���g�l�ƈقȂ��Ă�����ۑ�����
                Configuration defConfig = new();
                if (config.Text != defConfig.Text ||
                    config.ForeColor != defConfig.ForeColor ||
                    config.BackColor != defConfig.BackColor ||
                    config.Font.FontFamily != defConfig.Font.FontFamily ||
                    config.Font.Size != defConfig.Font.Size ||
                    config.Font.Bold != defConfig.Font.Bold ||
                    config.Font.Italic != defConfig.Font.Italic ||
                    config.Font.Strikeout != defConfig.Font.Strikeout ||
                    config.Font.Underline != defConfig.Font.Underline ||
                    config.Padding != defConfig.Padding ||
                    config.SnapAssist != defConfig.SnapAssist ||
                    config.TopMost != defConfig.TopMost ||
                    config.Blink != defConfig.Blink ||
                    config.AutoSave != defConfig.AutoSave)
                {
                    Serialize(config, configName);
                }
            }
        }

        /// <summary>
        /// �ݒ�t�@�C����json�ŕۑ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var config = new Configuration
            {
                Text = label1.Text,
                ForeColor = label1.ForeColor,
                BackColor = BackColor,
                Font = new ParamFont
                {
                    FontFamily = label1.Font.FontFamily.Name,
                    Size = label1.Font.Size,
                    Bold = label1.Font.Bold,
                    Italic = label1.Font.Italic,
                    Strikeout = label1.Font.Strikeout,
                    Underline = label1.Font.Underline,
                },
                Padding = label1.Padding.All,
                SnapAssist = snapAssistToolStripMenuItem.Checked,
                TopMost = topmostToolStripMenuItem.Checked,
                Blink = blinkToolStripMenuItem.Checked,
                AutoSave = autoSaveToolStripMenuItem.Checked,
            };
            Serialize(config, configName);
        }

        /// <summary>
        /// �����ۑ����邩�ǂ�����ݒ肷��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoSaveToolStripMenuItem.Checked = !autoSaveToolStripMenuItem.Checked;
        }

        private const string configName = "TextDisplay.config.json";
        public class Configuration
        {
            public string Text { get; set; } = "Right click to edit this message.";
            public Color ForeColor { get; set; } = Color.GhostWhite;
            public Color BackColor { get; set; } = Color.MediumBlue;
            public ParamFont Font { get; set; } = new ParamFont();
            public int Padding { get; set; } = 0;
            public bool SnapAssist { get; set; } = true;
            public bool TopMost { get; set; } = true;
            public bool Blink { get; set; } = false;
            public bool AutoSave { get; set; } = false;
        }
        public class ParamFont
        {
            public string FontFamily { get; set; } = "Yu Gothic UI";
            public float Size { get; set; } = 24;
            public bool Bold { get; set; } = false;
            public bool Italic { get; set; } = false;
            public bool Strikeout { get; set; } = false;
            public bool Underline { get; set; } = false;
        }

        /// <summary>
        /// json�������ݗp�V���A���C�Y
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="jsonfile"></param>
        private static void Serialize<T>(T data, string jsonfile)
        {
            using var fs = new FileStream(jsonfile, FileMode.Create);
            using var writer = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, true, true, "  ");
            var serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(writer, data);
        }
        /// <summary>
        /// json�ǂݍ��ݗp�f�V���A���C�Y
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonfile"></param>
        /// <returns></returns>
        private static T? DeSerialize<T>(string jsonfile)
        {
            try
            {
                using var ms = new FileStream(jsonfile, FileMode.Open);
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T?)serializer.ReadObject(ms);
            }
            catch
            {
                return default;
            }
        }
        #endregion
    }
}