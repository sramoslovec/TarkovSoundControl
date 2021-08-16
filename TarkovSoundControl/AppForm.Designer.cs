
namespace TarkovSoundControl
{
    partial class AppForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarBackegroundVolume = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.labelBackgroundVolume = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.labelForegroundVolume = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBarForegroundVolume = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBackegroundVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarForegroundVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "EFT background volume control";
            this.notifyIcon.Visible = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Searching for EFT process";
            // 
            // trackBarBackegroundVolume
            // 
            this.trackBarBackegroundVolume.Location = new System.Drawing.Point(9, 66);
            this.trackBarBackegroundVolume.Margin = new System.Windows.Forms.Padding(1);
            this.trackBarBackegroundVolume.Maximum = 100;
            this.trackBarBackegroundVolume.Name = "trackBarBackegroundVolume";
            this.trackBarBackegroundVolume.Size = new System.Drawing.Size(268, 45);
            this.trackBarBackegroundVolume.TabIndex = 5;
            this.trackBarBackegroundVolume.TickFrequency = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tarkov background volume";
            // 
            // labelBackgroundVolume
            // 
            this.labelBackgroundVolume.AutoSize = true;
            this.labelBackgroundVolume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelBackgroundVolume.Location = new System.Drawing.Point(236, 46);
            this.labelBackgroundVolume.Name = "labelBackgroundVolume";
            this.labelBackgroundVolume.Size = new System.Drawing.Size(15, 17);
            this.labelBackgroundVolume.TabIndex = 6;
            this.labelBackgroundVolume.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(161, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "made by Sramoslovec";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox1.Location = new System.Drawing.Point(9, 170);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(73, 19);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "autostart";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // labelForegroundVolume
            // 
            this.labelForegroundVolume.AutoSize = true;
            this.labelForegroundVolume.Location = new System.Drawing.Point(236, 101);
            this.labelForegroundVolume.Name = "labelForegroundVolume";
            this.labelForegroundVolume.Size = new System.Drawing.Size(15, 17);
            this.labelForegroundVolume.TabIndex = 11;
            this.labelForegroundVolume.Text = "0";
            this.labelForegroundVolume.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Tarkov foreground volume";
            this.label6.Visible = false;
            // 
            // trackBarForegroundVolume
            // 
            this.trackBarForegroundVolume.Location = new System.Drawing.Point(9, 121);
            this.trackBarForegroundVolume.Margin = new System.Windows.Forms.Padding(1);
            this.trackBarForegroundVolume.Maximum = 100;
            this.trackBarForegroundVolume.Name = "trackBarForegroundVolume";
            this.trackBarForegroundVolume.Size = new System.Drawing.Size(268, 45);
            this.trackBarForegroundVolume.TabIndex = 10;
            this.trackBarForegroundVolume.TickFrequency = 5;
            this.trackBarForegroundVolume.Visible = false;
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 191);
            this.Controls.Add(this.labelForegroundVolume);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trackBarForegroundVolume);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelBackgroundVolume);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBarBackegroundVolume);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 230);
            this.MinimumSize = new System.Drawing.Size(300, 230);
            this.Name = "AppForm";
            this.Padding = new System.Windows.Forms.Padding(30, 34, 30, 34);
            this.Text = "Tarkov Sound Control";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBackegroundVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarForegroundVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarBackegroundVolume;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelBackgroundVolume;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label labelForegroundVolume;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBarForegroundVolume;
    }
}

