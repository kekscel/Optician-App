
namespace proje
{
    partial class Giriş
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Giriş));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.HideButon = new System.Windows.Forms.Button();
            this.çıkışbuton = new System.Windows.Forms.Button();
            this.girişbuton = new System.Windows.Forms.Button();
            this.UnHideButon = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(91)))), ((int)(((byte)(100)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.textBox1.Location = new System.Drawing.Point(850, 376);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(91)))), ((int)(((byte)(100)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.textBox2.Location = new System.Drawing.Point(850, 432);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(200, 21);
            this.textBox2.TabIndex = 2;
            // 
            // HideButon
            // 
            this.HideButon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.HideButon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(91)))), ((int)(((byte)(100)))));
            this.HideButon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HideButon.ForeColor = System.Drawing.Color.Black;
            this.HideButon.Image = global::proje.Properties.Resources.hide1;
            this.HideButon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HideButon.Location = new System.Drawing.Point(1066, 426);
            this.HideButon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HideButon.Name = "HideButon";
            this.HideButon.Size = new System.Drawing.Size(48, 32);
            this.HideButon.TabIndex = 23;
            this.HideButon.UseVisualStyleBackColor = false;
            this.HideButon.Click += new System.EventHandler(this.HideButon_Click);
            // 
            // çıkışbuton
            // 
            this.çıkışbuton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.çıkışbuton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.çıkışbuton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("çıkışbuton.BackgroundImage")));
            this.çıkışbuton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.çıkışbuton.FlatAppearance.BorderSize = 0;
            this.çıkışbuton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.çıkışbuton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.çıkışbuton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.çıkışbuton.Location = new System.Drawing.Point(850, 566);
            this.çıkışbuton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.çıkışbuton.Name = "çıkışbuton";
            this.çıkışbuton.Size = new System.Drawing.Size(200, 50);
            this.çıkışbuton.TabIndex = 5;
            this.çıkışbuton.Text = "Çıkış";
            this.çıkışbuton.UseVisualStyleBackColor = false;
            this.çıkışbuton.Click += new System.EventHandler(this.çıkışbuton_Click);
            // 
            // girişbuton
            // 
            this.girişbuton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.girişbuton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.girişbuton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("girişbuton.BackgroundImage")));
            this.girişbuton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.girişbuton.FlatAppearance.BorderSize = 0;
            this.girişbuton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.girişbuton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.girişbuton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.girişbuton.Location = new System.Drawing.Point(850, 484);
            this.girişbuton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.girişbuton.Name = "girişbuton";
            this.girişbuton.Size = new System.Drawing.Size(200, 50);
            this.girişbuton.TabIndex = 4;
            this.girişbuton.Text = "Giriş";
            this.girişbuton.UseVisualStyleBackColor = false;
            this.girişbuton.Click += new System.EventHandler(this.girişbuton_Click);
            // 
            // UnHideButon
            // 
            this.UnHideButon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UnHideButon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(91)))), ((int)(((byte)(100)))));
            this.UnHideButon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnHideButon.ForeColor = System.Drawing.Color.Black;
            this.UnHideButon.Image = global::proje.Properties.Resources.unhide;
            this.UnHideButon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UnHideButon.Location = new System.Drawing.Point(1066, 426);
            this.UnHideButon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UnHideButon.Name = "UnHideButon";
            this.UnHideButon.Size = new System.Drawing.Size(48, 32);
            this.UnHideButon.TabIndex = 25;
            this.UnHideButon.UseVisualStyleBackColor = false;
            this.UnHideButon.Click += new System.EventHandler(this.UnHideButon_Click_1);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.button1.Location = new System.Drawing.Point(1066, 484);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 50);
            this.button1.TabIndex = 26;
            this.button1.Text = "İç ve Dışa Aktarma";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Giriş
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BackgroundImage = global::proje.Properties.Resources.hoşgeldiniz1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.UnHideButon);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.çıkışbuton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.HideButon);
            this.Controls.Add(this.girişbuton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "Giriş";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giriş";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button HideButon;
        private System.Windows.Forms.Button çıkışbuton;
        private System.Windows.Forms.Button girişbuton;
        private System.Windows.Forms.Button UnHideButon;
        private System.Windows.Forms.Button button1;
    }
}

