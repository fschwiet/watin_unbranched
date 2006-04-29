namespace WatiN.Logging
{
  public class Logger
  {
    private static ILogWriter mLogWriter = null;
    public static void LogAction(string message)
    {
      if (mLogWriter != null)
      {
        LogWriter.LogAction(message);
      }
    }

    public static ILogWriter LogWriter
    {
      get
      {
        return mLogWriter;
      }
      set
      {
        mLogWriter = value;
      }
    }
  }
  
  public interface ILogWriter
  {
    void LogAction(string message);
  }

	/// <summary>
	/// Summary description for LoggerDebug.
	/// </summary>
	public class DebugLogWriter : ILogWriter
	{
    public void LogAction(string message)
    {
      System.Diagnostics.Debug.WriteLine(message);
    }
  }
}
