namespace ExtensionMethods
{
    public class Parenthesis
    {
        private int _position;
        private int _type;
        private int _openClose;

        public Parenthesis(char ch, int pos)
        {
            _position = pos;
            (_type, _openClose) = ParenthesisType.GetType(ch);
        }

        public int Position { get => _position; }
        public int Type { get => _type; }
        public int OpenClose { get => _openClose; }
    }

  

} // end of namespace
