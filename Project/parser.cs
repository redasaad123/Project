
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF        =  0, // (EOF)
        SYMBOL_ERROR      =  1, // (Error)
        SYMBOL_WHITESPACE =  2, // Whitespace
        SYMBOL_MINUS      =  3, // '-'
        SYMBOL_MINUSMINUS =  4, // '--'
        SYMBOL_EXCLAMEQ   =  5, // '!='
        SYMBOL_LPAREN     =  6, // '('
        SYMBOL_RPAREN     =  7, // ')'
        SYMBOL_TIMES      =  8, // '*'
        SYMBOL_TIMESTIMES =  9, // '**'
        SYMBOL_COMMA      = 10, // ','
        SYMBOL_DIV        = 11, // '/'
        SYMBOL_LBRACE     = 12, // '{'
        SYMBOL_RBRACE     = 13, // '}'
        SYMBOL_PLUS       = 14, // '+'
        SYMBOL_PLUSPLUS   = 15, // '++'
        SYMBOL_LT         = 16, // '<'
        SYMBOL_EQ         = 17, // '='
        SYMBOL_EQEQ       = 18, // '=='
        SYMBOL_GT         = 19, // '>'
        SYMBOL_DIGIT      = 20, // Digit
        SYMBOL_DOUBLE     = 21, // double
        SYMBOL_FLOAT      = 22, // float
        SYMBOL_FOR        = 23, // for
        SYMBOL_ID         = 24, // Id
        SYMBOL_IF         = 25, // if
        SYMBOL_INT        = 26, // int
        SYMBOL_STRING     = 27, // string
        SYMBOL_ASSIGN     = 28, // <assign>
        SYMBOL_CONCEPT    = 29, // <concept>
        SYMBOL_COND       = 30, // <cond>
        SYMBOL_DATA       = 31, // <data>
        SYMBOL_DIGIT2     = 32, // <digit>
        SYMBOL_EXP        = 33, // <exp>
        SYMBOL_EXPR       = 34, // <expr>
        SYMBOL_FACTOR     = 35, // <factor>
        SYMBOL_FOR_STMT   = 36, // <for_stmt>
        SYMBOL_ID2        = 37, // <id>
        SYMBOL_IF_STMT    = 38, // <if_stmt>
        SYMBOL_OP         = 39, // <op>
        SYMBOL_PROGRAM    = 40, // <program>
        SYMBOL_STEP       = 41, // <step>
        SYMBOL_STMT_LIST  = 42, // <stmt_list>
        SYMBOL_TERM       = 43  // <term>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_LPAREN_RPAREN                                =  0, // <program> ::= '(' <stmt_list> ')'
        RULE_STMT_LIST                                            =  1, // <stmt_list> ::= <concept>
        RULE_STMT_LIST2                                           =  2, // <stmt_list> ::= <concept> <stmt_list>
        RULE_CONCEPT                                              =  3, // <concept> ::= <assign>
        RULE_CONCEPT2                                             =  4, // <concept> ::= <if_stmt>
        RULE_CONCEPT3                                             =  5, // <concept> ::= <for_stmt>
        RULE_ASSIGN_EQ                                            =  6, // <assign> ::= <id> '=' <expr>
        RULE_ID_ID                                                =  7, // <id> ::= Id
        RULE_EXPR_PLUS                                            =  8, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                           =  9, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                                 = 10, // <expr> ::= <term>
        RULE_TERM_TIMES                                           = 11, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                             = 12, // <term> ::= <term> '/' <factor>
        RULE_TERM                                                 = 13, // <term> ::= <factor>
        RULE_FACTOR_TIMESTIMES                                    = 14, // <factor> ::= <factor> '**' <exp>
        RULE_FACTOR                                               = 15, // <factor> ::= <exp>
        RULE_EXP_LPAREN_RPAREN                                    = 16, // <exp> ::= '(' <expr> ')'
        RULE_EXP                                                  = 17, // <exp> ::= <id>
        RULE_EXP2                                                 = 18, // <exp> ::= <digit>
        RULE_DIGIT_DIGIT                                          = 19, // <digit> ::= Digit
        RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACE_RBRACE               = 20, // <if_stmt> ::= if '(' <cond> ')' '{' <stmt_list> '}'
        RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACE_RBRACE2              = 21, // <if_stmt> ::= if '(' <cond> ')' '{' <stmt_list> '}' <stmt_list>
        RULE_COND                                                 = 22, // <cond> ::= <expr> <op> <expr>
        RULE_OP_LT                                                = 23, // <op> ::= '<'
        RULE_OP_GT                                                = 24, // <op> ::= '>'
        RULE_OP_EQEQ                                              = 25, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                          = 26, // <op> ::= '!='
        RULE_FOR_STMT_FOR_LPAREN_COMMA_COMMA_RPAREN_LPAREN_RPAREN = 27, // <for_stmt> ::= for '(' <data> <assign> ',' <cond> ',' <step> ')' '(' <stmt_list> ')'
        RULE_DATA_INT                                             = 28, // <data> ::= int
        RULE_DATA_DOUBLE                                          = 29, // <data> ::= double
        RULE_DATA_FLOAT                                           = 30, // <data> ::= float
        RULE_DATA_STRING                                          = 31, // <data> ::= string
        RULE_STEP_MINUSMINUS                                      = 32, // <step> ::= '--' <id>
        RULE_STEP_MINUSMINUS2                                     = 33, // <step> ::= <id> '--'
        RULE_STEP_PLUSPLUS                                        = 34, // <step> ::= <id> '++'
        RULE_STEP_PLUSPLUS2                                       = 35, // <step> ::= '++' <id>
        RULE_STEP                                                 = 36  // <step> ::= <assign>
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox listBox1;
        ListBox listBox2;

        public MyParser(string filename , ListBox listBox1 , ListBox listBox2 )
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);

            this.listBox1 = listBox1;
            this.listBox2 = listBox2;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);

        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //Id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONCEPT :
                //<concept>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DATA :
                //<data>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_LPAREN_RPAREN :
                //<program> ::= '(' <stmt_list> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_list> ::= <concept>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST2 :
                //<stmt_list> ::= <concept> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT :
                //<concept> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT2 :
                //<concept> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT3 :
                //<concept> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ :
                //<assign> ::= <id> '=' <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPAREN_RPAREN :
                //<exp> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACE_RBRACE :
                //<if_stmt> ::= if '(' <cond> ')' '{' <stmt_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACE_RBRACE2 :
                //<if_stmt> ::= if '(' <cond> ')' '{' <stmt_list> '}' <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expr> <op> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_LPAREN_COMMA_COMMA_RPAREN_LPAREN_RPAREN :
                //<for_stmt> ::= for '(' <data> <assign> ',' <cond> ',' <step> ')' '(' <stmt_list> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_INT :
                //<data> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_DOUBLE :
                //<data> ::= double
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_FLOAT :
                //<data> ::= float
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_STRING :
                //<data> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS :
                //<step> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS2 :
                //<step> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS :
                //<step> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS2 :
                //<step> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP :
                //<step> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = $"Parse error caused by token: ' {args.UnexpectedToken.ToString()} ' , in line  {args.UnexpectedToken.Location.LineNr} " ;
            listBox1.Items.Add(message);
            string message2 = $"Expected token: ' {args.ExpectedTokens.ToString()} ' ";
            listBox1.Items.Add(message2);
            //todo: Report message to UI?
        }


        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            string info = $"{args.Token.Text} \t{args.Token.Symbol.Id} \t{(SymbolConstants)args.Token.Symbol.Id} ";

            listBox2.Items.Add(info);

        }

    }
}
