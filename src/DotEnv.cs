namespace Utils
{
  public static class DotEnv
  {
    public static void Load(string path)
    {
      if (!File.Exists(path))
      {
        return;
      }

      foreach (var line in File.ReadAllLines(path))
      {
        var parts = line.Split('=', 2);
        if (parts.Length != 2)
        {
          continue;
        }

        var key = parts[0].Trim();
        var value = parts[1].Trim();
        Environment.SetEnvironmentVariable(key, value);
      }
    }
  }
}