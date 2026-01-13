namespace WhatsNewInDotNET10.CSharp14.NullConditionalAssignment
{
	// Cos’è:
	// - puoi usare l’operatore null-conditional come destinazione di un’assegnazione, tipo obj?.Prop = value o dict?["k"] = v.

	// Perché è utile:
	// - Sostituisce pattern ripetitivi: if (x is not null) x.Prop = ...;
	// - La parte destra non viene valutata se il receiver è null (quindi evita lavoro/side-effects inutili).

	#region Esempio: proprietà

	public static class Demo
	{
		public static string Expensive() => DateTime.UtcNow.Ticks.ToString();
	}

	class Order { public string? Note { get; set; } }

	#endregion

	// Note:
	// - È pensato soprattutto per expression statements(uso “da riga singola”).
}
