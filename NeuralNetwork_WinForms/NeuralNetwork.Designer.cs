namespace NeuralNetwork_WinForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxResult = new System.Windows.Forms.PictureBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonLearn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxResult
            // 
            this.pictureBoxResult.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.Size = new System.Drawing.Size(250, 250);
            this.pictureBoxResult.TabIndex = 0;
            this.pictureBoxResult.TabStop = false;
            this.pictureBoxResult.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxResult_Paint);
            this.pictureBoxResult.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxResult_MouseClick);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(12, 268);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(167, 23);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Clear dataset";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonLearn
            // 
            this.buttonLearn.Location = new System.Drawing.Point(187, 268);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(75, 23);
            this.buttonLearn.TabIndex = 2;
            this.buttonLearn.Text = "Learn";
            this.buttonLearn.UseVisualStyleBackColor = true;
            this.buttonLearn.Click += new System.EventHandler(this.buttonLearn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Add  data using mouse buttons ( left - class a, right - class b)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 349);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLearn);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.pictureBoxResult);
            this.Name = "Form1";
            this.Text = "Neural network";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxResult;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.Label label1;
    }
}

