namespace HttpUploader6.demoSql2005.db
{
    public class XDebug
	{
		public static void Output(string name, int v)
		{
			System.Diagnostics.Debug.Write(name);
			System.Diagnostics.Debug.Write(":");
			System.Diagnostics.Debug.Write(v);
			System.Diagnostics.Debug.Write("\n");
		}

		public static void Output(string name,long v)
		{
			System.Diagnostics.Debug.Write(name);
			System.Diagnostics.Debug.Write(":");
			System.Diagnostics.Debug.Write(v);
			System.Diagnostics.Debug.Write("\n");
		}

		public static void Output(string name,float v)
		{
			System.Diagnostics.Debug.Write(name);
			System.Diagnostics.Debug.Write(":");
			System.Diagnostics.Debug.Write(v);
			System.Diagnostics.Debug.Write("\n");
		}

		public static void Output(string name,double v)
		{
			System.Diagnostics.Debug.Write(name);
			System.Diagnostics.Debug.Write(":");
			System.Diagnostics.Debug.Write(v);
			System.Diagnostics.Debug.Write("\n");
		}

		public static void Output(string name, string v)
		{
			System.Diagnostics.Debug.Write(name);
			System.Diagnostics.Debug.Write(":");
			System.Diagnostics.Debug.Write(v);
			System.Diagnostics.Debug.Write("\n");
		}

		public static void Output(string v)
		{
			System.Diagnostics.Debug.Write(v);
			System.Diagnostics.Debug.Write("\n");
		}
	}
}
