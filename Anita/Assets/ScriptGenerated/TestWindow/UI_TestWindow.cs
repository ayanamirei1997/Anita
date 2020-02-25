/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace TestWindow.TestWindow
{
	public partial class UI_TestWindow : GComponent
	{
		public GButton m_btn1;

		public const string URL = "ui://qf1e4ryws8e50";

		public static UI_TestWindow CreateInstance()
		{
			return (UI_TestWindow)UIPackage.CreateObject("TestWindow","TestWindow");
		}

		public UI_TestWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btn1 = (GButton)this.GetChild("btn1");
		}
	}
}