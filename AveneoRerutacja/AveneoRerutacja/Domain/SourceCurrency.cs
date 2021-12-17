namespace AveneoRerutacja.Domain
{
    public class SourceCurrency : Currency
    {
        public SourceCurrency()
        { }
        
        public SourceCurrency(string code, string name = null, int codeLength = 3) : base(code, name, codeLength)
        { }
    }
}