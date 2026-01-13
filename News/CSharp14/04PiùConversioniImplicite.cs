namespace WhatsNewInDotNET10.CSharp14.PiùConversioniImplicite
{
	// Cos’è:
	// - “first-class span types”: il compilatore considera più conversioni built-in e regole di applicabilità,
	// riducendo la necessità di overload duplicati e rendendo più naturali molte chiamate/extension su ReadOnlySpan<T>.

	// Perché è utile:
	// - Librerie: puoi esporre una sola API su ReadOnlySpan<T> e farla funzionare bene con array/segment/span.
	// - Attenzione: può cambiare la risoluzione degli overload rispetto a C# 13 (possibili ambiguità nuove).

	#region Esempio: una sola API su ReadOnlySpan<T>

	public static class SpanHelpers
	{
		public static bool StartsWith<T>(this ReadOnlySpan<T> span, T value)
			where T : IEquatable<T>
			=> span.Length != 0 && EqualityComparer<T>.Default.Equals(span[0], value);
	}

	#endregion

	// Note:
	// - Se hai metodi overload(es.Equal(T, T) e Equal(Span<T>, Span<T>)), C# 14 può scegliere diversamente o segnalare ambiguità.
}
