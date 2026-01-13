namespace WhatsNewInDotNET10.CSharp14.ExtensionMembers
{
	// Cos’è: 
	// - una nuova sintassi che generalizza le extension methods: puoi aggiungere proprietà, membri statici “come se fossero del tipo” e anche operatori come estensioni.

	// Perché è utile: 
	// - API di utilità più “naturali” (es.s.IsBlank invece di StringExtensions.IsBlank(s)). 
	// - Possibilità di offrire proprietà e operatori senza poter modificare il tipo originale(o senza wrapper).

	#region Esempio: extension property + extension method

	public static class StringExtras
	{
		extension(string s)
		{
			public bool IsBlank => string.IsNullOrWhiteSpace(s);

			public string OrIfBlank(string fallback) =>
				string.IsNullOrWhiteSpace(s) ? fallback : s;
		}
	}

	#endregion

	#region Esempio: extension “statica” sul tipo
	//Qui “estendi il tipo” (non l’istanza), quindi i membri appaiono come statici del tipo esteso.

	public static class Int32TypeExtras
	{
		extension(int)
		{
			public static bool IsEven(int value) => (value & 1) == 0;
		}
	}

	#endregion

	// Note:
	// - Le extension declarations stanno in static class (come oggi per le extension methods).
	// - L’idea è “più potere alle extension”: ottimo per librerie, DSL e API ergonomiche.
}
