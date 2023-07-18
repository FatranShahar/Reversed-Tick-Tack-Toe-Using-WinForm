using System;
using System.Windows.Forms;

namespace WinFormsUI
{
    public class SettingForm : Form
    {
        private int m_BoardSize;
        private string m_Player1Name;
        private string m_Player2Name;
        private bool m_VsComputer = true;

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }

        }

        public string Player1Name
        {
            get
            {
                return m_Player1Name;
            }

            set
            {
                m_Player1Name = value;
            }

        }

        public string Player2Name
        {
            get
            {
                return m_Player2Name;
            }

            set
            {
                m_Player2Name = value;
            }
        
        }

        public bool IsVsComputer
        {
            get
            {
                return m_VsComputer;
            }
        }

        public SettingForm()
        {
            // Set the title of the form
            Text = "Game Settings";

            // Set the size of the form
            Size = new System.Drawing.Size(250, 250);

            // Center the form on the screen
            StartPosition = FormStartPosition.CenterScreen;

            // Set the form's MaximumSize and MinimumSize to the same fixed size
            MaximumSize = Size;
            MinimumSize = Size;

            // Disable the minimize and maximize buttons
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            Label m_PlayersLable = new Label();
            m_PlayersLable.Text = "Players:";
            m_PlayersLable.Location = new System.Drawing.Point(10, 10);
            m_PlayersLable.AutoSize = true;

            // Create the label
            Label labelPlayer1 = new Label();
            labelPlayer1.Text = "Player 1:";
            labelPlayer1.Location = new System.Drawing.Point(20, 30);
            labelPlayer1.AutoSize = true;

            // Create the Player 1 text box
            TextBox textBoxPlayer1 = new TextBox();
            textBoxPlayer1.Location = new System.Drawing.Point(100, 30);
            textBoxPlayer1.Size = new System.Drawing.Size(100, 30);
            textBoxPlayer1.Text = "";

            // Add the label and text box to the form's controls collection for Player 1
            Controls.Add(m_PlayersLable);
            Controls.Add(labelPlayer1);
            Controls.Add(textBoxPlayer1);           

            // Create the checkbox for Player 2
            CheckBox checkBoxPlayer2 = new CheckBox();
            checkBoxPlayer2.Text = "Player 2:";
            checkBoxPlayer2.Location = new System.Drawing.Point(20, 60);
            checkBoxPlayer2.AutoSize = true;

            // Create the Player 2 text box
            TextBox textBoxPlayer2 = new TextBox();
            textBoxPlayer2.Location = new System.Drawing.Point(100, 60);
            textBoxPlayer2.Size = new System.Drawing.Size(100, 20);
            textBoxPlayer2.Enabled = false;
            textBoxPlayer2.Text = "[Computer]";

            checkBoxPlayer2.CheckedChanged += (sender, e) =>
            {
                textBoxPlayer2.Enabled = checkBoxPlayer2.Checked;
                if (textBoxPlayer2.Enabled)
                {
                    textBoxPlayer2.Text = "";
                }
                else
                {
                    textBoxPlayer2.Text = "[Computer]";
                }

                m_VsComputer = !textBoxPlayer2.Enabled;

            };

            // Add the label and text box to the form's controls collection for Player 2
            //Controls.Add(labelPlayer2);
            Controls.Add(checkBoxPlayer2);
            Controls.Add(textBoxPlayer2);

            // Create the label
            Label m_BoardSizeLabel = new Label();
            m_BoardSizeLabel.Text = "Board Size:";
            m_BoardSizeLabel.Location = new System.Drawing.Point(10, 100);
            m_BoardSizeLabel.AutoSize = true;

            //Create updownNumeric
            NumericUpDown m_Row = new NumericUpDown();
            m_Row.Location = new System.Drawing.Point(60, 120);
            m_Row.Size = new System.Drawing.Size(30, 30);
            m_Row.AutoSize = true;
            m_Row.Maximum = 10;
            m_Row.Minimum = 4;

            Label m_RowLabel = new Label();
            m_RowLabel.Text = "Rows:";
            m_RowLabel.Location = new System.Drawing.Point(20, 120);
            m_RowLabel.AutoSize = true;

            Label m_ColLabel = new Label();
            m_ColLabel.Text = "Cols:";
            m_ColLabel.Location = new System.Drawing.Point(110, 120);
            m_ColLabel.AutoSize = true;

            NumericUpDown m_Col = new NumericUpDown();
            m_Col.Location = new System.Drawing.Point(140, 120);
            m_Col.AutoSize = true;
            m_Col.Size = new System.Drawing.Size(30, 30);
            m_Col.Maximum = 10;
            m_Col.Minimum = 4;

            // Link the NumericUpDown controls
            m_Row.ValueChanged += (sender, e) =>
            {
                m_Col.Value = m_Row.Value;
            };

            m_Col.ValueChanged += (sender, e) =>
            {
                m_Row.Value = m_Col.Value;
            };

            Controls.Add(m_RowLabel);
            Controls.Add(m_ColLabel);
            Controls.Add(m_BoardSizeLabel);
            Controls.Add(m_Col);
            Controls.Add(m_Row);

            //start button
            Button m_StartButton = new Button();
            m_StartButton.Text = "Play!";
            m_StartButton.Enabled = false;
            m_StartButton.AutoSize = true;
            m_StartButton.Width = 150;
            int rowCenter = (Width - m_StartButton.Width) / 2 - 10;
            m_StartButton.Location = new System.Drawing.Point(rowCenter, 160);

            textBoxPlayer1.TextChanged += (sender, e) =>
            {
                if(checkBoxPlayer2.Enabled)
                {
                    m_StartButton.Enabled = (textBoxPlayer1.Text != "" && textBoxPlayer2.Text != "");
                }
                else
                {
                    m_StartButton.Enabled = (textBoxPlayer1.Text != "");
                }
                
            };

            textBoxPlayer2.TextChanged += (sender, e) =>
            {
                if (checkBoxPlayer2.Enabled)
                {
                    m_StartButton.Enabled = (textBoxPlayer1.Text != "" && textBoxPlayer2.Text != "");
                }
                else
                {
                    m_StartButton.Enabled = (textBoxPlayer1.Text != "");
                }
            };

            Controls.Add(m_StartButton);

            m_StartButton.Click += (sender, e) =>
            {
                // Retrieve the data from the form controls
                BoardSize = (int)m_Row.Value;
                Player1Name = textBoxPlayer1.Text;

                if (textBoxPlayer2.Text == "[Computer]")
                {
                    Player2Name = "Computer";
                }
                else
                {
                    Player2Name = textBoxPlayer2.Text;
                }

                // Close the settings form
                Close();
            };

        }
    }
}
