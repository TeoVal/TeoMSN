using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MessageClient.Utils
{
    public partial class QuestionBox : Form
    {
        private string Answer { get; set; } = null;

        private string Question { get; set; }

        private string FormTitle { get; set; }

        public QuestionBox()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Answer = answerBox.Text;
            this.Close();
        }

        private void QuestionBox_Load(object sender, EventArgs e)
        {
            questionLabel.Text = Question;
            this.Text = FormTitle;
        }

        public static string Ask(string question, string title)
        {
            string answer = null;

            QuestionBox questionBox = new QuestionBox();
            questionBox.Question = question;
            questionBox.FormTitle = title;
            questionBox.ShowDialog();
            answer = questionBox.Answer;

            return answer;
        }
    }
}
