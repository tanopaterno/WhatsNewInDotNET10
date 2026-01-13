namespace WhatsNewInDotNET10.CSharp14.UserDefinedCompoundAssignment
{
	// Cos’è:
	// - puoi dichiarare operatori composti come metodi di istanza void con sintassi tipo public void operator +=(T y) { ... },
	// così x += y può mutare x in-place (utile per tipi “grossi” o mutabili).

	// Perché è utile:
	// - Evita allocazioni/copie quando prima eri costretto a implementare solo operator + creando un nuovo oggetto.

	#region Esempio: tipo mutabile

	public sealed class Counter
	{
		public int Value { get; private set; }

		public void operator +=(int x) => Value += x;

		// (Opzionale) anche l'operatore binario “classico”
		public static Counter operator +(Counter c, int x)
			=> new Counter { Value = c.Value + x };
	}

	#endregion

	// Note:
	// - La spec definisce chiaramente regole e priorità rispetto all’overload “classico” di + quando risolvi +=.
}
