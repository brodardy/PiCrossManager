namespace PiCrossManager.Generator
{
    partial class GeneratorView
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.ofdOpenImage = new System.Windows.Forms.OpenFileDialog();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numThreshold = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.pbxFinal = new PiCrossManager.Generator.MyPictureBox();
            this.pbxGrayscale = new PiCrossManager.Generator.MyPictureBox();
            this.pbxOriginal = new PiCrossManager.Generator.MyPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFinal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGrayscale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOriginal)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Original image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Grayscale image";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Final image";
            // 
            // btnSaveXML
            // 
            this.btnSaveXML.Location = new System.Drawing.Point(248, 446);
            this.btnSaveXML.Name = "btnSaveXML";
            this.btnSaveXML.Size = new System.Drawing.Size(200, 23);
            this.btnSaveXML.TabIndex = 6;
            this.btnSaveXML.Text = "Save";
            this.btnSaveXML.UseVisualStyleBackColor = true;
            // 
            // ofdOpenImage
            // 
            this.ofdOpenImage.FileName = "openFileDialog1";
            // 
            // numWidth
            // 
            this.numWidth.Location = new System.Drawing.Point(324, 269);
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(42, 20);
            this.numWidth.TabIndex = 7;
            // 
            // numHeight
            // 
            this.numHeight.Location = new System.Drawing.Point(324, 295);
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(42, 20);
            this.numHeight.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(261, 297);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Height";
            // 
            // numThreshold
            // 
            this.numThreshold.Location = new System.Drawing.Point(309, 321);
            this.numThreshold.Name = "numThreshold";
            this.numThreshold.Size = new System.Drawing.Size(57, 20);
            this.numThreshold.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(245, 323);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Threshold";
            // 
            // pbxFinal
            // 
            this.pbxFinal.Location = new System.Drawing.Point(16, 269);
            this.pbxFinal.Name = "pbxFinal";
            this.pbxFinal.Size = new System.Drawing.Size(200, 200);
            this.pbxFinal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxFinal.TabIndex = 5;
            this.pbxFinal.TabStop = false;
            // 
            // pbxGrayscale
            // 
            this.pbxGrayscale.Location = new System.Drawing.Point(248, 30);
            this.pbxGrayscale.Name = "pbxGrayscale";
            this.pbxGrayscale.Size = new System.Drawing.Size(200, 200);
            this.pbxGrayscale.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxGrayscale.TabIndex = 3;
            this.pbxGrayscale.TabStop = false;
            // 
            // pbxOriginal
            // 
            this.pbxOriginal.Location = new System.Drawing.Point(16, 30);
            this.pbxOriginal.Name = "pbxOriginal";
            this.pbxOriginal.Size = new System.Drawing.Size(200, 200);
            this.pbxOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxOriginal.TabIndex = 1;
            this.pbxOriginal.TabStop = false;
            // 
            // GeneratorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 481);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numThreshold);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numHeight);
            this.Controls.Add(this.numWidth);
            this.Controls.Add(this.btnSaveXML);
            this.Controls.Add(this.pbxFinal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbxGrayscale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbxOriginal);
            this.Controls.Add(this.label1);
            this.Name = "GeneratorView";
            this.Text = "PiCross Generator";
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFinal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGrayscale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOriginal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MyPictureBox pbxOriginal;
        private MyPictureBox pbxGrayscale;
        private System.Windows.Forms.Label label2;
        private MyPictureBox pbxFinal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveXML;
        private System.Windows.Forms.OpenFileDialog ofdOpenImage;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numThreshold;
        private System.Windows.Forms.Label label6;

    }
}