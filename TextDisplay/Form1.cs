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

        #region フォームの移動と表示関係
        /// <summary>
        /// フォームの移動
        /// </summary>
        /// <param name="eX"></param>
        /// <param name="eY"></param>
        private void FormMove(int eX, int eY)
        {
            // 移動先のフォーム位置
            Rectangle newPosition =
                new(Left + eX, Top + eY, Width, Height);

            // 画面の端に吸着させて配置する
            if (snapAssistToolStripMenuItem.Checked)
            {
                // 吸着するサイズ
                Size gap = new(16, 16);

                // 判定用のRECT
                Rectangle newRect;

                // 作業領域の取得（この作業領域の内側に吸着する）
                Size area = new(
                    Screen.FromControl(label1).WorkingArea.Width,
                    Screen.FromControl(label1).WorkingArea.Height);

                // 作業領域の左上隅の座標の取得
                Point wp = Screen.FromControl(label1).WorkingArea.Location;

                // 画面端の判定用（画面の端の位置に、吸着するサイズ分のRECTを定義する）
                Rectangle rectLeft =
                    new(wp.X, wp.Y, gap.Width, area.Height);
                Rectangle rectTop =
                    new(wp.X, wp.Y, area.Width, gap.Height);
                Rectangle rectRight =
                    new(area.Width - gap.Width + wp.X, wp.Y, gap.Width, area.Height);
                Rectangle rectBottom =
                    new(wp.X, area.Height - gap.Height + wp.Y, area.Width, gap.Height);

                // 衝突判定
                // 判定用のRECTを自分のウィンドウの隅に重ねるように移動し、
                // 画面端の判定用のRECTと衝突しているかチェックする。
                // 衝突していた場合は、吸着させるように移動する

                // 左端衝突判定
                {
                    newRect = newPosition;
                    newRect.Width = gap.Width;

                    if (newRect.IntersectsWith(rectLeft))
                    {
                        // 左端に吸着させる
                        newPosition.X = wp.X;
                    }
                }
                // 右端衝突判定
                {
                    newRect = newPosition;
                    newRect.X = newPosition.Right - gap.Width;  // ウィンドウの右隅
                    newRect.Width = gap.Width;

                    if (newRect.IntersectsWith(rectRight))
                    {
                        // 右端に吸着させる
                        newPosition.X = area.Width - Width + wp.X;
                    }
                }
                // 上端衝突判定
                {
                    newRect = newPosition;
                    newRect.Height = gap.Height;

                    if (newRect.IntersectsWith(rectTop))
                    {
                        // 上端に吸着させる
                        newPosition.Y = wp.Y;
                    }
                }
                // 下端衝突判定
                {
                    newRect = newPosition;
                    newRect.Y = newPosition.Bottom - gap.Height; // ウィンドウの下端
                    newRect.Height = gap.Height;

                    if (newRect.IntersectsWith(rectBottom))
                    {
                        // 下端に吸着させる
                        newPosition.Y = area.Height - Height + wp.Y;
                    }
                }
            }

            // 実際に移動させる
            Left = newPosition.Left;
            Top = newPosition.Top;
        }

        /// <summary>
        /// マウスのクリック位置を記憶
        /// </summary>
        private Point mousePoint;

        /// <summary>
        /// Form1のMouseDownイベントハンドラ
        /// マウスのボタンが押されたとき
        /// </summary>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new Point(e.X, e.Y);
            }
        }

        /// <summary>
        /// Form1のMouseMoveイベントハンドラ
        /// マウスが動いたとき
        /// </summary>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                FormMove(e.X - mousePoint.X, e.Y - mousePoint.Y);
            }
        }

        /// <summary>
        /// フォーム移動時に画面端に吸着させるかどうかを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnapAssistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            snapAssistToolStripMenuItem.Checked = !snapAssistToolStripMenuItem.Checked;
        }

        /// <summary>
        /// フォームを最前面に表示させるかどうかを変更する
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

        #region テキストのデザイン関係
        /// <summary>
        /// フォームのサイズをコントロールのサイズに変更
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
        /// 設定を反映させる
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
        /// 設定をリセットする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetConfig(new Configuration());
        }

        /// <summary>
        /// フォントダイアログを表示してフォントを変更する
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
        /// カラーダイアログを表示して文字色を変更する
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
        /// カラーダイアログを表示して背景色を変更する
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
        /// テキストボックスを使って表示するテキストを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = toolStripTextBox1.Text;
            FormSizeChange();
        }

        /// <summary>
        /// VBのテキスト入力フォームコントロールを使ってテキストを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // キャンセルボタンを押したら戻り値が""になる
            var str = Interaction.InputBox("", "Text", label1.Text);
            // ""の場合は変更しない
            if (str != "")
            {
                label1.Text = str;
                FormSizeChange();
            }
        }

        /// <summary>
        /// Paddingを変更する
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
        /// Paddingの設定値
        /// </summary>
        private int padding = 0;

        /// <summary>
        /// Paddingの設定時に↑↓が押されたときは値を +1，-1 にする
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

        #region シリアライズ関係
        /// <summary>
        /// フォームを起動するときに設定ファイルを読み込む
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // 起動時に設定ファイルを読み込む
            var param = DeSerialize<Configuration?>(configName);
            SetConfig(param);
        }

        /// <summary>
        /// フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// フォームを閉じるときに設定ファイルを書き込む
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // AutoSaveにチェックが入っていたら保存する
            if (autoSaveToolStripMenuItem.Checked)
            {
                // 終了時に設定ファイルをjsonで保存する
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
                // 設定がデフォルト値と異なっていたら保存する
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
        /// 設定ファイルをjsonで保存する
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
        /// 自動保存するかどうかを設定する
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
        /// json書き込み用シリアライズ
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
        /// json読み込み用デシリアライズ
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