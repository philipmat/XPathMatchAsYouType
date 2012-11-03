using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Xml;

namespace XPathMatchAsYouType {
	public delegate void FileOpenDelegate (string filename);
	
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form {
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbFile;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbXpath;
		private System.Windows.Forms.TextBox tbResult;
		private System.Windows.Forms.OpenFileDialog ofd;
		private XmlNamespaceManager _ns;
		
		// my private variables
		private XmlDocument xdoc = null;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.LinkLabel selectFont;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.tbFile = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbXpath = new System.Windows.Forms.TextBox();
			this.tbResult = new System.Windows.Forms.TextBox();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.selectFont = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// tbFile
			// 
			this.tbFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbFile.Location = new System.Drawing.Point(104, 8);
			this.tbFile.Name = "tbFile";
			this.tbFile.Size = new System.Drawing.Size(544, 20);
			this.tbFile.TabIndex = 0;
			this.tbFile.Text = "";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(648, 7);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(25, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "...";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 30);
			this.label1.TabIndex = 2;
			this.label1.Text = "Select XML File \n(or drag n\' drop)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(219, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Type an XPath expression to see matches:";
			// 
			// tbXpath
			// 
			this.tbXpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbXpath.Location = new System.Drawing.Point(8, 64);
			this.tbXpath.Name = "tbXpath";
			this.tbXpath.Size = new System.Drawing.Size(664, 20);
			this.tbXpath.TabIndex = 4;
			this.tbXpath.Text = "/";
			this.tbXpath.TextChanged += new System.EventHandler(this.tbXpath_TextChanged);
			// 
			// tbResult
			// 
			this.tbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbResult.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tbResult.ForeColor = System.Drawing.SystemColors.WindowText;
			this.tbResult.Location = new System.Drawing.Point(8, 96);
			this.tbResult.Multiline = true;
			this.tbResult.Name = "tbResult";
			this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbResult.Size = new System.Drawing.Size(664, 456);
			this.tbResult.TabIndex = 5;
			this.tbResult.Text = "";
			this.tbResult.WordWrap = false;
			// 
			// ofd
			// 
			this.ofd.Filter = "XML Files (*.xml, *.config)|*.xml;*.config|All files|*.*";
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// selectFont
			// 
			this.selectFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.selectFont.Location = new System.Drawing.Point(640, 552);
			this.selectFont.Name = "selectFont";
			this.selectFont.Size = new System.Drawing.Size(40, 23);
			this.selectFont.TabIndex = 6;
			this.selectFont.TabStop = true;
			this.selectFont.Text = "Font?";
			this.selectFont.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.selectFont_LinkClicked);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(688, 566);
			this.Controls.Add(this.selectFont);
			this.Controls.Add(this.tbResult);
			this.Controls.Add(this.tbXpath);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tbFile);
			this.Name = "MainForm";
			this.Text = "XPath Match-As-You-Type";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new MainForm());
		}

		private void button1_Click(object sender, System.EventArgs e) {
			if (ofd.ShowDialog() == DialogResult.OK) {
				LoadXml(ofd.FileName);
				tbFile.Text = ofd.FileName;
				tbXpath.Focus();
			}
		}

		private void LoadXml(string filename)
		{
			try {
				xdoc = new XmlDocument();
				xdoc.Load(filename);
				XmlElement de;
				de = xdoc.DocumentElement;
				_ns = null;
				
				XmlNameTable xt = new NameTable();
				_ns = new XmlNamespaceManager(xt);
												
				foreach (XmlAttribute xa in de.Attributes) {
					if (xa.Name.StartsWith("xmlns")) {
						string[] xmlNs = xa.Name.Split(':');
						if (xmlNs.Length == 1) {
							// add ns as default namespace
							_ns.AddNamespace("ns", xa.Value);
						} else if (xmlNs.Length == 2) {
							_ns.AddNamespace(xmlNs[1], xa.Value);
						}
					}
				}
//				if (de.NamespaceURI != "") {
//					Debug.WriteLine("NamespaceURI=" + de.NamespaceURI);
//					XmlNameTable xt = new NameTable();
//					_ns = new XmlNamespaceManager(xt);
//					_ns.AddNamespace("ns", de.NamespaceURI);
//				}
//				XmlNameTable xt = de.OwnerDocument.NameTable;
//				_ns = new XmlNamespaceManager(xt);
//				_ns.AddNamespace("", de.NamespaceURI);
				tbResult.Text = "";
				this.MatchXPath(tbXpath.Text);
			} catch (Exception ex) {
				MessageBox.Show("Error loading XML from" + filename + ": " + ex, "XML Load Error");
				return;
			}
		}
		
		private string FormatXml(XmlNode node) {
			StringBuilder sb = new StringBuilder();
			using (StringWriter sw = new StringWriter(sb)) {
				XmlTextWriter xtw = new XmlTextWriter(sw);
				xtw.Formatting = Formatting.Indented;
				node.WriteTo(xtw);
				xtw.Flush();
			}
			return sb.ToString();
		}
		private void MatchXPath(string xpath) {
			if (xdoc == null) return;
			try {
				XmlNodeList nodes;
				nodes = xdoc.SelectNodes(tbXpath.Text, _ns);
				if (nodes.Count > 0 ) {
					errorProvider1.SetError(tbXpath, "");
					tbResult.Text = "";
					tbResult.ForeColor = SystemColors.WindowText;
					foreach (XmlNode xn in nodes) {
						tbResult.Text += FormatXml(xn) + "\r\n";
					}
				} else {
					tbResult.ForeColor = SystemColors.InactiveCaptionText;
					errorProvider1.SetError(tbXpath, "No match for this XPath");
				}
			} catch (Exception ex) {
				Debug.WriteLine("Exception finding XPath {" + xpath + "}: " + ex);
			}
		}
			
		private void tbXpath_TextChanged(object sender, System.EventArgs e) {
			this.MatchXPath(tbXpath.Text);
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e) {
			try {
				Array a = (Array)e.Data.GetData(DataFormats.FileDrop);

				if ( a != null ) {
					// Extract string from first array element
					// (ignore all files except first if number of files are dropped).
					string s = a.GetValue(0).ToString();
					tbFile.Text = s;
					tbXpath.Focus();
					// Call OpenFile asynchronously.
					// Explorer instance from which file is dropped is not responding
					// all the time when DragDrop handler is active, so we need to return
					// immidiately (especially if OpenFile shows MessageBox).

					this.BeginInvoke(new FileOpenDelegate(this.LoadXml), new Object[] {s});
					this.Activate();        // in the case Explorer overlaps this form
				}
			}
			catch (Exception ex) {
				Trace.WriteLine("Error in DragDrop function: " + ex.Message);

				// don't show MessageBox here - Explorer is waiting !
			}
		}
		
		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Copy;
			} else {
				e.Effect = DragDropEffects.None;
			}
			
		}

		private void selectFont_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			fontDialog1.Font = tbResult.Font;
			fontDialog1.Color = tbResult.ForeColor;
			
			if(fontDialog1.ShowDialog() != DialogResult.Cancel ) {
				tbResult.Font = fontDialog1.Font ;
				tbResult.ForeColor = fontDialog1.Color;
			}

		}
	}
}
