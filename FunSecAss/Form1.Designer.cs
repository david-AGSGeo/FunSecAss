namespace FunSecAss
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
            this.requestAuthenticationButton = new System.Windows.Forms.Button();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.ASreplyTextBox = new System.Windows.Forms.TextBox();
            this.ASreplyLabel = new System.Windows.Forms.Label();
            this.ticketTextBox = new System.Windows.Forms.TextBox();
            this.KtgsTextBox = new System.Windows.Forms.TextBox();
            this.ticketLabel = new System.Windows.Forms.Label();
            this.KtgsLabel = new System.Windows.Forms.Label();
            this.plaintextTextBox = new System.Windows.Forms.TextBox();
            this.TGSLabel = new System.Windows.Forms.Label();
            this.encryptedKtgsTextBox = new System.Windows.Forms.TextBox();
            this.plaintextLabel = new System.Windows.Forms.Label();
            this.encryptedKtgsLabel = new System.Windows.Forms.Label();
            this.hidePasswordCheckbox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // requestAuthenticationButton
            // 
            this.requestAuthenticationButton.Location = new System.Drawing.Point(12, 90);
            this.requestAuthenticationButton.Name = "requestAuthenticationButton";
            this.requestAuthenticationButton.Size = new System.Drawing.Size(175, 30);
            this.requestAuthenticationButton.TabIndex = 0;
            this.requestAuthenticationButton.Text = "Request Authentication";
            this.requestAuthenticationButton.UseVisualStyleBackColor = true;
            this.requestAuthenticationButton.Click += new System.EventHandler(this.requestAuthenticationButton_Click);
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(87, 10);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.userNameTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(87, 36);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(100, 20);
            this.passwordTextBox.TabIndex = 2;
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(18, 13);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(63, 13);
            this.userNameLabel.TabIndex = 3;
            this.userNameLabel.Text = "User Name:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(18, 38);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 4;
            this.passwordLabel.Text = "Password";
            // 
            // ASreplyTextBox
            // 
            this.ASreplyTextBox.Location = new System.Drawing.Point(12, 151);
            this.ASreplyTextBox.Multiline = true;
            this.ASreplyTextBox.Name = "ASreplyTextBox";
            this.ASreplyTextBox.ReadOnly = true;
            this.ASreplyTextBox.Size = new System.Drawing.Size(175, 56);
            this.ASreplyTextBox.TabIndex = 5;
            // 
            // ASreplyLabel
            // 
            this.ASreplyLabel.AutoSize = true;
            this.ASreplyLabel.Location = new System.Drawing.Point(9, 135);
            this.ASreplyLabel.Name = "ASreplyLabel";
            this.ASreplyLabel.Size = new System.Drawing.Size(162, 13);
            this.ASreplyLabel.TabIndex = 6;
            this.ASreplyLabel.Text = "Reply from Authentication Server";
            // 
            // ticketTextBox
            // 
            this.ticketTextBox.Location = new System.Drawing.Point(87, 215);
            this.ticketTextBox.Name = "ticketTextBox";
            this.ticketTextBox.ReadOnly = true;
            this.ticketTextBox.Size = new System.Drawing.Size(100, 20);
            this.ticketTextBox.TabIndex = 7;
            // 
            // KtgsTextBox
            // 
            this.KtgsTextBox.Location = new System.Drawing.Point(87, 241);
            this.KtgsTextBox.Name = "KtgsTextBox";
            this.KtgsTextBox.ReadOnly = true;
            this.KtgsTextBox.Size = new System.Drawing.Size(100, 20);
            this.KtgsTextBox.TabIndex = 8;
            // 
            // ticketLabel
            // 
            this.ticketLabel.AutoSize = true;
            this.ticketLabel.Location = new System.Drawing.Point(18, 218);
            this.ticketLabel.Name = "ticketLabel";
            this.ticketLabel.Size = new System.Drawing.Size(40, 13);
            this.ticketLabel.TabIndex = 9;
            this.ticketLabel.Text = "Ticket:";
            // 
            // KtgsLabel
            // 
            this.KtgsLabel.AutoSize = true;
            this.KtgsLabel.Location = new System.Drawing.Point(18, 244);
            this.KtgsLabel.Name = "KtgsLabel";
            this.KtgsLabel.Size = new System.Drawing.Size(52, 13);
            this.KtgsLabel.TabIndex = 10;
            this.KtgsLabel.Text = "Key Ktgs:";
            // 
            // plaintextTextBox
            // 
            this.plaintextTextBox.Location = new System.Drawing.Point(12, 302);
            this.plaintextTextBox.Multiline = true;
            this.plaintextTextBox.Name = "plaintextTextBox";
            this.plaintextTextBox.ReadOnly = true;
            this.plaintextTextBox.Size = new System.Drawing.Size(175, 41);
            this.plaintextTextBox.TabIndex = 11;
            // 
            // TGSLabel
            // 
            this.TGSLabel.AutoSize = true;
            this.TGSLabel.Location = new System.Drawing.Point(12, 271);
            this.TGSLabel.Name = "TGSLabel";
            this.TGSLabel.Size = new System.Drawing.Size(172, 13);
            this.TGSLabel.TabIndex = 12;
            this.TGSLabel.Text = "Message to Ticket Granting Server";
            // 
            // encryptedKtgsTextBox
            // 
            this.encryptedKtgsTextBox.Location = new System.Drawing.Point(12, 363);
            this.encryptedKtgsTextBox.Multiline = true;
            this.encryptedKtgsTextBox.Name = "encryptedKtgsTextBox";
            this.encryptedKtgsTextBox.ReadOnly = true;
            this.encryptedKtgsTextBox.Size = new System.Drawing.Size(175, 47);
            this.encryptedKtgsTextBox.TabIndex = 13;
            // 
            // plaintextLabel
            // 
            this.plaintextLabel.AutoSize = true;
            this.plaintextLabel.Location = new System.Drawing.Point(9, 286);
            this.plaintextLabel.Name = "plaintextLabel";
            this.plaintextLabel.Size = new System.Drawing.Size(47, 13);
            this.plaintextLabel.TabIndex = 14;
            this.plaintextLabel.Text = "Plaintext";
            // 
            // encryptedKtgsLabel
            // 
            this.encryptedKtgsLabel.AutoSize = true;
            this.encryptedKtgsLabel.Location = new System.Drawing.Point(9, 347);
            this.encryptedKtgsLabel.Name = "encryptedKtgsLabel";
            this.encryptedKtgsLabel.Size = new System.Drawing.Size(101, 13);
            this.encryptedKtgsLabel.TabIndex = 15;
            this.encryptedKtgsLabel.Text = "Encrypted with Ktgs";
            // 
            // hidePasswordCheckbox
            // 
            this.hidePasswordCheckbox.AutoSize = true;
            this.hidePasswordCheckbox.Checked = true;
            this.hidePasswordCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hidePasswordCheckbox.Location = new System.Drawing.Point(87, 62);
            this.hidePasswordCheckbox.Name = "hidePasswordCheckbox";
            this.hidePasswordCheckbox.Size = new System.Drawing.Size(97, 17);
            this.hidePasswordCheckbox.TabIndex = 16;
            this.hidePasswordCheckbox.Text = "Hide Password";
            this.hidePasswordCheckbox.UseVisualStyleBackColor = true;
            this.hidePasswordCheckbox.CheckedChanged += new System.EventHandler(this.hidePasswordCheckbox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(491, 342);
            this.button1.TabIndex = 17;
            this.button1.Text = "Push Me David";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(231, 383);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(491, 115);
            this.textBox1.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 528);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.hidePasswordCheckbox);
            this.Controls.Add(this.encryptedKtgsLabel);
            this.Controls.Add(this.plaintextLabel);
            this.Controls.Add(this.encryptedKtgsTextBox);
            this.Controls.Add(this.TGSLabel);
            this.Controls.Add(this.plaintextTextBox);
            this.Controls.Add(this.KtgsLabel);
            this.Controls.Add(this.ticketLabel);
            this.Controls.Add(this.KtgsTextBox);
            this.Controls.Add(this.ticketTextBox);
            this.Controls.Add(this.ASreplyLabel);
            this.Controls.Add(this.ASreplyTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.requestAuthenticationButton);
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "KerberOS server Authentication Simulation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button requestAuthenticationButton;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox ASreplyTextBox;
        private System.Windows.Forms.Label ASreplyLabel;
        private System.Windows.Forms.TextBox ticketTextBox;
        private System.Windows.Forms.TextBox KtgsTextBox;
        private System.Windows.Forms.Label ticketLabel;
        private System.Windows.Forms.Label KtgsLabel;
        private System.Windows.Forms.TextBox plaintextTextBox;
        private System.Windows.Forms.Label TGSLabel;
        private System.Windows.Forms.TextBox encryptedKtgsTextBox;
        private System.Windows.Forms.Label plaintextLabel;
        private System.Windows.Forms.Label encryptedKtgsLabel;
        private System.Windows.Forms.CheckBox hidePasswordCheckbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

