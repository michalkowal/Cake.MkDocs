public static class Version
{
	private static ICakeContext _context;

	public static string Cake { get; private set; }
	public static string MkDocs { get; private set; }
	
	public static void Initialize(ICakeContext context)
	{
		_context = context;
	
		Cake = GetCakeVersion();
		MkDocs = GetMkDocsVersion();
	}

	private static string GetCakeVersion()
	{
		return GetVersionInShared("CakeVersion");
	}
	
	private static string GetMkDocsVersion()
	{
		return GetVersionInShared("MkDocsVersion");
	}

	private static string GetVersionInShared(string variable)
	{
		var sharedFile = new FilePath("./src/Shared.msbuild");
		var version = _context.XmlPeek(sharedFile, $"/Project/PropertyGroup/{variable}");
		
		return version;
	}
}