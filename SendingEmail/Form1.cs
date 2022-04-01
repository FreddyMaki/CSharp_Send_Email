using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendingEmail
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		//
		private void button1_Click(object sender, EventArgs e)
		{
						
			//Read and Display Text File and Image File from Embedded Resource in C# and VB.Net
			//NB : In properties of embeded file set it as Resources incorporés:
			Assembly assembly = Assembly.GetExecutingAssembly();
			StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("SendingEmail.Data.Wazari.txt"));

			textBox1.Text = reader.ReadToEnd();

			//Send Email with Html Template
			SendEmail();
		}



		//Send HTML Page(File) as Email Body in ASP.Net using C# and VB.Net
		private string PopulateBody(string userName, string title, string url, string description)
		{
			string body = string.Empty;
			Assembly assembly = Assembly.GetExecutingAssembly();
			StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("SendingEmail.Data.EmailTemplate.html"));
			body = reader.ReadToEnd();
			body = body.Replace("{UserName}", userName);
			body = body.Replace("{Title}", title);
			body = body.Replace("{Url}", url);
			body = body.Replace("{Description}", description);

			return body;
		}

		private string populateWithAccueil()
		{
			string body = string.Empty;
			Assembly assembly = Assembly.GetExecutingAssembly();
			StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("SendingEmail.Data.accueil.html"));
			body = reader.ReadToEnd();
			body = body.Replace("{UserName}", "Oh le Con");
			body = body.Replace("{Name}", "merde");
			
			return body;
		}


		private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
		{
			using (MailMessage mailMessage = new MailMessage())
			{
				mailMessage.From = new MailAddress("rfr.fred@gmail.com");
				mailMessage.Subject = subject;
				mailMessage.Body = body;
				mailMessage.IsBodyHtml = true;
				mailMessage.To.Add(new MailAddress(recepientEmail));
				SmtpClient smtp = new SmtpClient();
				smtp.Host = "smtp.gmail.com";
				smtp.EnableSsl = true;
				System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
				NetworkCred.UserName = "rfr.fred@gmail.com";
				NetworkCred.Password = "xxxxxxxxxx";
				smtp.UseDefaultCredentials = true;
				smtp.Credentials = NetworkCred;
				smtp.Port = 587;
				smtp.Send(mailMessage);
				MessageBox.Show("Mail envoyer avec succès");
			}
		}

		protected void SendEmail()
		{
			string nom = "sdfsdfsdfsdf";
			string body = this.PopulateBody(nom,
				"Fetch multiple values as Key Value pair in ASP.Net AJAX AutoCompleteExtender",
				"//www.aspsnippets.com/Articles/Fetch-multiple-values-as-Key-Value-pair-" +
				"in-ASP.Net-AJAX-AutoCompleteExtender.aspx",
				"Here Mudassar Ahmed Khan has explained how to fetch multiple column values i.e." +
				" ID and Text values in the ASP.Net AJAX Control Toolkit AutocompleteExtender"
				+ "and also how to fetch the select text and value server side on postback");

			string body_meilleurs_taux = this.populateWithAccueil();
			//this.SendHtmlFormattedEmail("aandrianjatovoniaina@afiassurances.fr", "Test Email Meilleurs Taux!", body_meilleurs_taux);
			//this.SendHtmlFormattedEmail("aandrianjatovoniaina@afiassurances.fr", "New article published!", body);
			this.SendHtmlFormattedEmail("sandriamahefarizo@afiassurances.fr", "New article published!", body_meilleurs_taux);

		}

		//Another method 
		static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\EmailTemplate.html");
		private static string GenerateEmailBody(string nom, string prenom, string civility)
		{
			string emailBody = string.Empty;
			try
			{
				string strHTML = File.ReadAllText(path);
				strHTML = strHTML
					.Replace("[SourceFileName]", nom)
					.Replace("[SourceFileLastName]", prenom)
					.Replace("[SourceCivility]", civility);
				emailBody = strHTML;
			}
			catch (Exception ex)
			{
				System.Diagnostics.EventLog.WriteEntry(ex.Source, ex.Message, System.Diagnostics.EventLogEntryType.Error);
			}
			return emailBody;
		}

	}
}
