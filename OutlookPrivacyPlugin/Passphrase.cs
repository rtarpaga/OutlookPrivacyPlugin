﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OutlookPrivacyPlugin
{
	internal partial class Passphrase : Form
	{
		//private readonly string _defaultKey;
		private const string _enterPhrase = " < enter passphrase > ";
		private char _passwordChar = '*';	// Global password char, if case it's changed 
											// before PreparePrivateKeyField()

		public string EnteredPassphrase { get { return this.PrivateKey.Text; } }

		internal Passphrase(string defaultKey, string buttonText)
		{
			TopMost = true;

			InitializeComponent();

			//OkButton.Text = buttonText;
			AcceptButton = OkButton;
		}

		private void Passphrase_Load(object sender, EventArgs e)
		{
			EmptyPrivateKeyField(PrivateKey);
		}

		private void EmptyPrivateKeyField(Control focusControl)
		{
			PrivateKey.PasswordChar = (char)0;
			PrivateKey.Text = _enterPhrase;
			PrivateKey.TextAlign = HorizontalAlignment.Center;
			PrivateKey.ForeColor = Color.LightGray;

			focusControl.Focus();
		}

		private void PreparePrivateKeyField()
		{
			PrivateKey.PasswordChar = _passwordChar;
			PrivateKey.Text = string.Empty;
			PrivateKey.TextAlign = HorizontalAlignment.Left;
			PrivateKey.ForeColor = Color.Black;
		}

		private void PrivateKey_Enter(object sender, EventArgs e)
		{
			if (PrivateKey.Text == _enterPhrase)
				PreparePrivateKeyField();
		}

		private void OkButton_Enter(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(PrivateKey.Text))
				EmptyPrivateKeyField(OkButton);
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			if ((PrivateKey.Text == _enterPhrase) || (string.IsNullOrEmpty(PrivateKey.Text)))
			{
				// No Passphrase provided, complain!
				Errors.SetError(PrivateKey, "No passphrase provided!");
				PrivateKey.Margin = new Padding(3, 3, 20, 3);
			}
			else
			{
				Errors.SetError(PrivateKey, string.Empty);
				PrivateKey.Margin = new Padding(3, 3, 3, 3);

				// Hide the form and let our main addin take over again
				Hide();
			}
		}
	}
}
