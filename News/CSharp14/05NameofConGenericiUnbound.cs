namespace WhatsNewInDotNET10.CSharp14.NameofConGenericiUnbound
{
	// Cos’è:
	// - nameof(List<>), nameof(Dictionary<,>) ecc. senza dover inventare un tipo concreto(List<int>).

	// Perché è utile:
	// - Logging/diagnostica in librerie generiche senza “hack”.
	// - Evita stringhe hardcoded.

	#region Esempio
	public static class GenericHelper
	{
		public static void ThrowExpected<T>()
		{
			throw new InvalidOperationException($"Expected {nameof(List<>)} but got {typeof(T).Name}");
		}
	}

	#endregion
}
