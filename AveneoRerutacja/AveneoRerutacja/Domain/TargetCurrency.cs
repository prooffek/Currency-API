namespace AveneoRerutacja.Domain
{
    public class TargetCurrency : Currency
    {
        public TargetCurrency()
        { }
        
        public TargetCurrency(string code, string name = null, int codeLength = 3) : base(code, name, codeLength)
        {
            
        }
    }
}