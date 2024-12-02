namespace SinemaBiletOtomasyonu // Projenin ad alanı
{
    partial class RegisterForm // RegisterForm adlı formun kısmi tanımı
    {
        /// <summary>
        /// Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        /// <param name="disposing">Yönetilen kaynakların temizlenip temizlenmeyeceğini belirten bir parametre.</param>
        protected override void Dispose(bool disposing) // Dispose metodu, kaynakların temizlenmesini sağlar.
        {
            if (disposing && (components != null)) // Eğer bileşenler boş değilse
            {
                components.Dispose(); // Bileşenleri temizle
            }
            base.Dispose(disposing); // Base sınıfın Dispose metodunu çağır
        }

        #region Windows Form Designer tarafından oluşturulan kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot
        /// Bu metodun içeriğini kod düzenleyicisi ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent() // Formun bileşenlerini başlatan metot
        {
            this.components = new System.ComponentModel.Container(); // Formdaki bileşenler için kapsayıcı oluşturulur
            // Form bileşenlerinin tanımlanması
            this.txtUsername = new System.Windows.Forms.TextBox(); // Kullanıcı adı girişi için TextBox
            this.txtPassword = new System.Windows.Forms.TextBox(); // Şifre girişi için TextBox
            this.txtEmail = new System.Windows.Forms.TextBox(); // Email girişi için TextBox
            this.txtFullName = new System.Windows.Forms.TextBox(); // Ad soyad girişi için TextBox
            this.btnRegister = new System.Windows.Forms.Button(); // Kayıt olma butonu
            this.btnBack = new System.Windows.Forms.Button(); // Geri butonu
            this.label1 = new System.Windows.Forms.Label(); // Kullanıcı adı etiketi
            this.label2 = new System.Windows.Forms.Label(); // Şifre etiketi
            this.label3 = new System.Windows.Forms.Label(); // Email etiketi
            this.label4 = new System.Windows.Forms.Label(); // Ad soyad etiketi

            this.SuspendLayout(); // Formun yeniden çizimini durdurur

            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F); // Kullanıcı adı TextBox yazı tipi
            this.txtUsername.Location = new System.Drawing.Point(200, 100); // TextBox'ın konumu
            this.txtUsername.Name = "txtUsername"; // Bileşen adı
            this.txtUsername.Size = new System.Drawing.Size(250, 25); // TextBox boyutu
            this.txtUsername.TabIndex = 0; // Tab sıralaması

            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F); // Şifre TextBox yazı tipi
            this.txtPassword.Location = new System.Drawing.Point(200, 150); // TextBox'ın konumu
            this.txtPassword.Name = "txtPassword"; // Bileşen adı
            this.txtPassword.PasswordChar = '*'; // Şifreyi gizlemek için karakter
            this.txtPassword.Size = new System.Drawing.Size(250, 25); // TextBox boyutu
            this.txtPassword.TabIndex = 1; // Tab sıralaması

            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F); // Email TextBox yazı tipi
            this.txtEmail.Location = new System.Drawing.Point(200, 200); // TextBox'ın konumu
            this.txtEmail.Name = "txtEmail"; // Bileşen adı
            this.txtEmail.Size = new System.Drawing.Size(250, 25); // TextBox boyutu
            this.txtEmail.TabIndex = 2; // Tab sıralaması

            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 10F); // Ad Soyad TextBox yazı tipi
            this.txtFullName.Location = new System.Drawing.Point(200, 250); // TextBox'ın konumu
            this.txtFullName.Name = "txtFullName"; // Bileşen adı
            this.txtFullName.Size = new System.Drawing.Size(250, 25); // TextBox boyutu
            this.txtFullName.TabIndex = 3; // Tab sıralaması

            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(149, 147, 230); // Buton arka plan rengi
            this.btnRegister.FlatAppearance.BorderSize = 0; // Buton kenarlık boyutu
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat; // Buton stilini düz yapar
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); // Yazı tipi
            this.btnRegister.ForeColor = System.Drawing.Color.White; // Yazı rengi
            this.btnRegister.Location = new System.Drawing.Point(200, 300); // Buton konumu
            this.btnRegister.Name = "btnRegister"; // Bileşen adı
            this.btnRegister.Size = new System.Drawing.Size(120, 35); // Buton boyutu
            this.btnRegister.TabIndex = 4; // Tab sıralaması
            this.btnRegister.Text = "Kayıt Ol"; // Buton üzerindeki metin
            this.btnRegister.UseVisualStyleBackColor = false; // Özel arka plan rengini etkinleştir
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click); // Tıklama olayını bağla

            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(88, 86, 214); // Buton arka plan rengi
            this.btnBack.FlatAppearance.BorderSize = 0; // Buton kenarlık boyutu
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat; // Buton stilini düz yapar
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); // Yazı tipi
            this.btnBack.ForeColor = System.Drawing.Color.White; // Yazı rengi
            this.btnBack.Location = new System.Drawing.Point(330, 300); // Buton konumu
            this.btnBack.Name = "btnBack"; // Bileşen adı
            this.btnBack.Size = new System.Drawing.Size(120, 35); // Buton boyutu
            this.btnBack.TabIndex = 5; // Tab sıralaması
            this.btnBack.Text = "Geri"; // Buton üzerindeki metin
            this.btnBack.UseVisualStyleBackColor = false; // Özel arka plan rengini etkinleştir
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click); // Tıklama olayını bağla

            // 
            // label1
            // 
            this.label1.AutoSize = true; // Etiket boyutunu otomatik ayarlar
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F); // Yazı tipi
            this.label1.Location = new System.Drawing.Point(120, 103); // Etiket konumu
            this.label1.Name = "label1"; // Bileşen adı
            this.label1.Size = new System.Drawing.Size(74, 19); // Etiket boyutu
            this.label1.TabIndex = 6; // Tab sıralaması
            this.label1.Text = "Kullanıcı Adı:"; // Etiket üzerindeki metin

            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(120, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Şifre:";

            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(120, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Email:";

            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(120, 253);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ad Soyad:";

            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); // Formun ölçeklendirme boyutları
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; // Formun ölçeklendirme modu
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 255); // Form arka plan rengi
            this.ClientSize = new System.Drawing.Size(600, 450); // Formun boyutu
            this.Controls.Add(this.label4); // Etiketi forma ekle
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBack); // Butonu forma ekle
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.txtFullName); // TextBox'ı forma ekle
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Name = "RegisterForm"; // Formun adı
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; // Formun başlangıç konumu
            this.Text = "Kayıt Ol"; // Form başlığı
            this.ResumeLayout(false); // Form düzenlemeyi kapat
            this.PerformLayout(); // Düzeni uygula
        }

        #endregion

        // Bileşen tanımlamaları
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
