namespace SimuladorBanco.Presentacion
{
    /// <summary>
    /// Centraliza toda la presentación visual en consola: colores, marcos,
    /// mensajes de éxito / error y lectura de datos con validación básica.
    /// Separa la interfaz de usuario de la lógica del banco.
    /// </summary>
    public static class ConsolaHelper
    {
        // ── Escritura con color ───────────────────────────────────────────

        public static void Exito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✔  {mensaje}");
            Console.ResetColor();
        }

        public static void Error(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ✘  ERROR: {mensaje}");
            Console.ResetColor();
        }

        public static void Titulo(string titulo)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  ══  {titulo.ToUpper()}  ══");
            Console.ResetColor();
        }

        public static void Info(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  ℹ  {mensaje}");
            Console.ResetColor();
        }

        public static void Separador(int ancho = 76)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + new string('─', ancho));
            Console.ResetColor();
        }

        public static void Escribir(string texto) => Console.Write(texto);
        public static void EscribirLinea(string texto) => Console.WriteLine($"  {texto}");

        // ── Encabezado ────────────────────────────────────────────────────

        public static void MostrarEncabezado(string nombreBanco)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════════════════════════════╗");
            Console.WriteLine($"  ║  {nombreBanco.ToUpper().PadRight(51)}║");
            Console.WriteLine("  ║  SIMULADOR BANCARIO  ·  ESTRUCTURAS DE DATOS         ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        // ── Lectura con validación ────────────────────────────────────────

        /// <summary>Lee una cadena no vacía desde consola.</summary>
        public static string LeerTexto(string etiqueta)
        {
            while (true)
            {
                Console.Write($"  {etiqueta}");
                string? entrada = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(entrada))
                    return entrada;
                Error("El campo no puede estar vacío. Intente de nuevo.");
            }
        }

        /// <summary>Lee un entero válido desde consola.</summary>
        public static int LeerEntero(string etiqueta)
        {
            while (true)
            {
                Console.Write($"  {etiqueta}");
                if (int.TryParse(Console.ReadLine(), out int resultado))
                    return resultado;
                Error("Ingrese un número entero válido.");
            }
        }

        /// <summary>Lee un número decimal positivo desde consola.</summary>
        public static double LeerDouble(string etiqueta)
        {
            while (true)
            {
                Console.Write($"  {etiqueta}");
                string? raw = Console.ReadLine()?.Replace(",", ".");
                if (double.TryParse(raw,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out double resultado) && resultado >= 0)
                    return resultado;
                Error("Ingrese un valor numérico válido (mayor o igual a cero).");
            }
        }

        /// <summary>Pausa la ejecución hasta que el usuario presione una tecla.</summary>
        public static void Pausar()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  Presione cualquier tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey(intercept: true);
        }
    }
}
