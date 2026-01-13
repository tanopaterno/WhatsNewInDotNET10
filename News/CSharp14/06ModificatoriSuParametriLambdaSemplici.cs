namespace WhatsNewInDotNET10.CSharp14.ModificatoriSuParametriLambdaSemplici
{
	// Cos’è:
	// - puoi scrivere (text, out result) => ... oppure (ref x) => ... senza dover esplicitare il tipo del parametro quando è inferibile dal delegate.

	// Perché è utile:
	// - Lambdas più terse, ma con semantiche ref/out/in/scoped visibili.

	#region Esempio con out

	delegate bool TryParse<T>(string text, out T result);

	#endregion

	#region Esempio con ref

	delegate void Mutator<T>(ref T value);

	#endregion
}
