namespace Projeto.Core.Contexts.CompartilhadoContext.Helpers
{
    public static class CriadorStringAleatorio
    {
        public static string GerarSeisCaracteres()
        {
            return Guid.NewGuid().ToString("N")[0..6].ToUpper();
        }
        public static string GerarOitoCracteres()
        {
            return Guid.NewGuid().ToString("N")[0..8].ToUpper();
        }
    }
}
