namespace WhatsNewInDotNET10.CSharp14.PartialPerCostruttoriEdEventi
{
	// Cos’è:
	// - puoi separare dichiarazione e implementazione di costruttori ed eventi in classi partial, 
	//   un po’ come già succede con i partial methods/properties.

	// Perché è utile
	// - Source generators: il dev dichiara, il generator implementa(o viceversa).
	// - Modularità(anche senza generator).
	// - Permette di separare chiaramente le responsabilità tra chi definisce l’API (firma di costruttori/ eventi) 
	//   e chi ne fornisce l’implementazione.
	// - Utile in scenari con code generation (source generators) o framework che generano codice (ORM, DI container, ecc).

	#region Esempio: partial constructor

	public partial class Widget
	{
		// Parte “defining”
		partial Widget(int x, string name);
	}

	// In un altro file (o più sotto nello stesso esempio)
	public partial class Widget
	{
		// Parte “implementing”
		partial Widget(int x, string name)
		{
			X = x;
			Name = name;
		}

		public int X { get; }
		public string Name { get; }
	}

	#endregion

	#region Esempio: partial event

	public partial class Bus
	{
		partial event Action<string> Message;
	}

	// Implementazione separata
	public partial class Bus
	{
		private Action<string>? _message;

		partial event Action<string> Message
		{
			add { _message += value; }
			remove { _message -= value; }
		}

		public void Publish(string s) => _message?.Invoke(s);
	}

	#endregion
}
