namespace WhatsNewInDotNET10.CSharp14.FieldKeyword
{
	// Cos’è:
	// - field è una parola chiave contestuale che dentro get/set/init ti fa accedere al backing field generato dal compilatore, 
	//   evitando di dichiararlo a mano.

	// Perché è utile:
	// - Mantieni la compattezza delle auto-properties, ma inserisci logica(validazione/normalizzazione).

	#region Esempio: normalizzazione + null guard

	public sealed class Person
	{
		public string Name
		{
			get;
			init => field = (value ?? throw new ArgumentNullException(nameof(value))).Trim();
		}
	}

	#endregion

	// Note:
	// - field esiste solo nel corpo degli accessor della proprietà.
	// - È contestuale: se in vecchie versioni avevi davvero una variabile chiamata field in quel contesto, possono emergere warning/edge case.
}
