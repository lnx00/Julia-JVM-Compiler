//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/LNX/Documents/Rider/Coba-Projekt/Compiler/Parser/Grammar/Julia.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public partial class JuliaLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		FUNCTION_T=1, RETURN_T=2, BEGIN_T=3, END_T=4, IF_T=5, ELSE_T=6, WHILE_T=7, 
		INTEGER_T=8, FLOAT64_T=9, BOOL_T=10, STRING_T=11, LBRACE=12, RBRACE=13, 
		LPAREN=14, RPAREN=15, LBRACK=16, RBRACK=17, DCOLON=18, COMMA=19, SEMICOLON=20, 
		COLON=21, DOT=22, EQ=23, AMP=24, PIPE=25, PLUS=26, MINUS=27, STAR=28, 
		SLASH=29, PERCENT=30, AND=31, OR=32, NOT=33, EQEQ=34, LT=35, GT=36, LTE=37, 
		GTE=38, NEQ=39, INTCONST=40, FLTCONST=41, STRCONST=42, BOOLCONST=43, IDENTIFIER=44, 
		COMMENT=45, COMMENT_BLOCK=46, WS=47;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"Letter", "Digit", "LetterOrDigit", "FUNCTION_T", "RETURN_T", "BEGIN_T", 
		"END_T", "IF_T", "ELSE_T", "WHILE_T", "INTEGER_T", "FLOAT64_T", "BOOL_T", 
		"STRING_T", "LBRACE", "RBRACE", "LPAREN", "RPAREN", "LBRACK", "RBRACK", 
		"DCOLON", "COMMA", "SEMICOLON", "COLON", "DOT", "EQ", "AMP", "PIPE", "PLUS", 
		"MINUS", "STAR", "SLASH", "PERCENT", "AND", "OR", "NOT", "EQEQ", "LT", 
		"GT", "LTE", "GTE", "NEQ", "INTCONST", "FLTCONST", "STRCONST", "BOOLCONST", 
		"IDENTIFIER", "COMMENT", "COMMENT_BLOCK", "WS"
	};


	public JuliaLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public JuliaLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'function'", "'return'", "'begin'", "'end'", "'if'", "'else'", 
		"'while'", "'Integer'", "'Float64'", "'Bool'", "'String'", "'{'", "'}'", 
		"'('", "')'", "'['", "']'", "'::'", "','", "';'", "':'", "'.'", "'='", 
		"'&'", "'|'", "'+'", "'-'", "'*'", "'/'", "'%'", null, null, "'!'", null, 
		"'<'", "'>'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "FUNCTION_T", "RETURN_T", "BEGIN_T", "END_T", "IF_T", "ELSE_T", 
		"WHILE_T", "INTEGER_T", "FLOAT64_T", "BOOL_T", "STRING_T", "LBRACE", "RBRACE", 
		"LPAREN", "RPAREN", "LBRACK", "RBRACK", "DCOLON", "COMMA", "SEMICOLON", 
		"COLON", "DOT", "EQ", "AMP", "PIPE", "PLUS", "MINUS", "STAR", "SLASH", 
		"PERCENT", "AND", "OR", "NOT", "EQEQ", "LT", "GT", "LTE", "GTE", "NEQ", 
		"INTCONST", "FLTCONST", "STRCONST", "BOOLCONST", "IDENTIFIER", "COMMENT", 
		"COMMENT_BLOCK", "WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Julia.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static JuliaLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,47,314,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,7,33,2,34,7,34,2,35,
		7,35,2,36,7,36,2,37,7,37,2,38,7,38,2,39,7,39,2,40,7,40,2,41,7,41,2,42,
		7,42,2,43,7,43,2,44,7,44,2,45,7,45,2,46,7,46,2,47,7,47,2,48,7,48,2,49,
		7,49,1,0,1,0,1,1,1,1,1,2,1,2,3,2,108,8,2,1,3,1,3,1,3,1,3,1,3,1,3,1,3,1,
		3,1,3,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,5,1,5,1,5,1,5,1,5,1,5,1,6,1,6,1,6,
		1,6,1,7,1,7,1,7,1,8,1,8,1,8,1,8,1,8,1,9,1,9,1,9,1,9,1,9,1,9,1,10,1,10,
		1,10,1,10,1,10,1,10,1,10,1,10,1,11,1,11,1,11,1,11,1,11,1,11,1,11,1,11,
		1,12,1,12,1,12,1,12,1,12,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,14,1,14,
		1,15,1,15,1,16,1,16,1,17,1,17,1,18,1,18,1,19,1,19,1,20,1,20,1,20,1,21,
		1,21,1,22,1,22,1,23,1,23,1,24,1,24,1,25,1,25,1,26,1,26,1,27,1,27,1,28,
		1,28,1,29,1,29,1,30,1,30,1,31,1,31,1,32,1,32,1,33,1,33,1,33,1,34,1,34,
		1,34,1,35,1,35,1,36,1,36,1,36,1,37,1,37,1,38,1,38,1,39,1,39,1,39,1,40,
		1,40,1,40,1,41,1,41,1,41,1,42,4,42,242,8,42,11,42,12,42,243,1,43,4,43,
		247,8,43,11,43,12,43,248,1,43,1,43,4,43,253,8,43,11,43,12,43,254,1,44,
		1,44,5,44,259,8,44,10,44,12,44,262,9,44,1,44,1,44,1,45,1,45,1,45,1,45,
		1,45,1,45,1,45,1,45,1,45,3,45,275,8,45,1,46,1,46,1,46,5,46,280,8,46,10,
		46,12,46,283,9,46,1,47,1,47,5,47,287,8,47,10,47,12,47,290,9,47,1,47,1,
		47,1,48,1,48,1,48,1,48,5,48,298,8,48,10,48,12,48,301,9,48,1,48,1,48,1,
		48,1,48,1,48,1,49,4,49,309,8,49,11,49,12,49,310,1,49,1,49,2,260,299,0,
		50,1,0,3,0,5,0,7,1,9,2,11,3,13,4,15,5,17,6,19,7,21,8,23,9,25,10,27,11,
		29,12,31,13,33,14,35,15,37,16,39,17,41,18,43,19,45,20,47,21,49,22,51,23,
		53,24,55,25,57,26,59,27,61,28,63,29,65,30,67,31,69,32,71,33,73,34,75,35,
		77,36,79,37,81,38,83,39,85,40,87,41,89,42,91,43,93,44,95,45,97,46,99,47,
		1,0,4,2,0,65,90,97,122,1,0,48,57,2,0,10,10,13,13,3,0,9,10,13,13,32,32,
		321,0,7,1,0,0,0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,
		1,0,0,0,0,19,1,0,0,0,0,21,1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,
		0,0,29,1,0,0,0,0,31,1,0,0,0,0,33,1,0,0,0,0,35,1,0,0,0,0,37,1,0,0,0,0,39,
		1,0,0,0,0,41,1,0,0,0,0,43,1,0,0,0,0,45,1,0,0,0,0,47,1,0,0,0,0,49,1,0,0,
		0,0,51,1,0,0,0,0,53,1,0,0,0,0,55,1,0,0,0,0,57,1,0,0,0,0,59,1,0,0,0,0,61,
		1,0,0,0,0,63,1,0,0,0,0,65,1,0,0,0,0,67,1,0,0,0,0,69,1,0,0,0,0,71,1,0,0,
		0,0,73,1,0,0,0,0,75,1,0,0,0,0,77,1,0,0,0,0,79,1,0,0,0,0,81,1,0,0,0,0,83,
		1,0,0,0,0,85,1,0,0,0,0,87,1,0,0,0,0,89,1,0,0,0,0,91,1,0,0,0,0,93,1,0,0,
		0,0,95,1,0,0,0,0,97,1,0,0,0,0,99,1,0,0,0,1,101,1,0,0,0,3,103,1,0,0,0,5,
		107,1,0,0,0,7,109,1,0,0,0,9,118,1,0,0,0,11,125,1,0,0,0,13,131,1,0,0,0,
		15,135,1,0,0,0,17,138,1,0,0,0,19,143,1,0,0,0,21,149,1,0,0,0,23,157,1,0,
		0,0,25,165,1,0,0,0,27,170,1,0,0,0,29,177,1,0,0,0,31,179,1,0,0,0,33,181,
		1,0,0,0,35,183,1,0,0,0,37,185,1,0,0,0,39,187,1,0,0,0,41,189,1,0,0,0,43,
		192,1,0,0,0,45,194,1,0,0,0,47,196,1,0,0,0,49,198,1,0,0,0,51,200,1,0,0,
		0,53,202,1,0,0,0,55,204,1,0,0,0,57,206,1,0,0,0,59,208,1,0,0,0,61,210,1,
		0,0,0,63,212,1,0,0,0,65,214,1,0,0,0,67,216,1,0,0,0,69,219,1,0,0,0,71,222,
		1,0,0,0,73,224,1,0,0,0,75,227,1,0,0,0,77,229,1,0,0,0,79,231,1,0,0,0,81,
		234,1,0,0,0,83,237,1,0,0,0,85,241,1,0,0,0,87,246,1,0,0,0,89,256,1,0,0,
		0,91,274,1,0,0,0,93,276,1,0,0,0,95,284,1,0,0,0,97,293,1,0,0,0,99,308,1,
		0,0,0,101,102,7,0,0,0,102,2,1,0,0,0,103,104,7,1,0,0,104,4,1,0,0,0,105,
		108,3,1,0,0,106,108,3,3,1,0,107,105,1,0,0,0,107,106,1,0,0,0,108,6,1,0,
		0,0,109,110,5,102,0,0,110,111,5,117,0,0,111,112,5,110,0,0,112,113,5,99,
		0,0,113,114,5,116,0,0,114,115,5,105,0,0,115,116,5,111,0,0,116,117,5,110,
		0,0,117,8,1,0,0,0,118,119,5,114,0,0,119,120,5,101,0,0,120,121,5,116,0,
		0,121,122,5,117,0,0,122,123,5,114,0,0,123,124,5,110,0,0,124,10,1,0,0,0,
		125,126,5,98,0,0,126,127,5,101,0,0,127,128,5,103,0,0,128,129,5,105,0,0,
		129,130,5,110,0,0,130,12,1,0,0,0,131,132,5,101,0,0,132,133,5,110,0,0,133,
		134,5,100,0,0,134,14,1,0,0,0,135,136,5,105,0,0,136,137,5,102,0,0,137,16,
		1,0,0,0,138,139,5,101,0,0,139,140,5,108,0,0,140,141,5,115,0,0,141,142,
		5,101,0,0,142,18,1,0,0,0,143,144,5,119,0,0,144,145,5,104,0,0,145,146,5,
		105,0,0,146,147,5,108,0,0,147,148,5,101,0,0,148,20,1,0,0,0,149,150,5,73,
		0,0,150,151,5,110,0,0,151,152,5,116,0,0,152,153,5,101,0,0,153,154,5,103,
		0,0,154,155,5,101,0,0,155,156,5,114,0,0,156,22,1,0,0,0,157,158,5,70,0,
		0,158,159,5,108,0,0,159,160,5,111,0,0,160,161,5,97,0,0,161,162,5,116,0,
		0,162,163,5,54,0,0,163,164,5,52,0,0,164,24,1,0,0,0,165,166,5,66,0,0,166,
		167,5,111,0,0,167,168,5,111,0,0,168,169,5,108,0,0,169,26,1,0,0,0,170,171,
		5,83,0,0,171,172,5,116,0,0,172,173,5,114,0,0,173,174,5,105,0,0,174,175,
		5,110,0,0,175,176,5,103,0,0,176,28,1,0,0,0,177,178,5,123,0,0,178,30,1,
		0,0,0,179,180,5,125,0,0,180,32,1,0,0,0,181,182,5,40,0,0,182,34,1,0,0,0,
		183,184,5,41,0,0,184,36,1,0,0,0,185,186,5,91,0,0,186,38,1,0,0,0,187,188,
		5,93,0,0,188,40,1,0,0,0,189,190,5,58,0,0,190,191,5,58,0,0,191,42,1,0,0,
		0,192,193,5,44,0,0,193,44,1,0,0,0,194,195,5,59,0,0,195,46,1,0,0,0,196,
		197,5,58,0,0,197,48,1,0,0,0,198,199,5,46,0,0,199,50,1,0,0,0,200,201,5,
		61,0,0,201,52,1,0,0,0,202,203,5,38,0,0,203,54,1,0,0,0,204,205,5,124,0,
		0,205,56,1,0,0,0,206,207,5,43,0,0,207,58,1,0,0,0,208,209,5,45,0,0,209,
		60,1,0,0,0,210,211,5,42,0,0,211,62,1,0,0,0,212,213,5,47,0,0,213,64,1,0,
		0,0,214,215,5,37,0,0,215,66,1,0,0,0,216,217,3,53,26,0,217,218,3,53,26,
		0,218,68,1,0,0,0,219,220,3,55,27,0,220,221,3,55,27,0,221,70,1,0,0,0,222,
		223,5,33,0,0,223,72,1,0,0,0,224,225,3,51,25,0,225,226,3,51,25,0,226,74,
		1,0,0,0,227,228,5,60,0,0,228,76,1,0,0,0,229,230,5,62,0,0,230,78,1,0,0,
		0,231,232,3,75,37,0,232,233,3,51,25,0,233,80,1,0,0,0,234,235,3,77,38,0,
		235,236,3,51,25,0,236,82,1,0,0,0,237,238,3,71,35,0,238,239,3,51,25,0,239,
		84,1,0,0,0,240,242,7,1,0,0,241,240,1,0,0,0,242,243,1,0,0,0,243,241,1,0,
		0,0,243,244,1,0,0,0,244,86,1,0,0,0,245,247,7,1,0,0,246,245,1,0,0,0,247,
		248,1,0,0,0,248,246,1,0,0,0,248,249,1,0,0,0,249,250,1,0,0,0,250,252,3,
		49,24,0,251,253,7,1,0,0,252,251,1,0,0,0,253,254,1,0,0,0,254,252,1,0,0,
		0,254,255,1,0,0,0,255,88,1,0,0,0,256,260,5,34,0,0,257,259,9,0,0,0,258,
		257,1,0,0,0,259,262,1,0,0,0,260,261,1,0,0,0,260,258,1,0,0,0,261,263,1,
		0,0,0,262,260,1,0,0,0,263,264,5,34,0,0,264,90,1,0,0,0,265,266,5,116,0,
		0,266,267,5,114,0,0,267,268,5,117,0,0,268,275,5,101,0,0,269,270,5,102,
		0,0,270,271,5,97,0,0,271,272,5,108,0,0,272,273,5,115,0,0,273,275,5,101,
		0,0,274,265,1,0,0,0,274,269,1,0,0,0,275,92,1,0,0,0,276,281,3,1,0,0,277,
		280,3,5,2,0,278,280,5,95,0,0,279,277,1,0,0,0,279,278,1,0,0,0,280,283,1,
		0,0,0,281,279,1,0,0,0,281,282,1,0,0,0,282,94,1,0,0,0,283,281,1,0,0,0,284,
		288,5,35,0,0,285,287,8,2,0,0,286,285,1,0,0,0,287,290,1,0,0,0,288,286,1,
		0,0,0,288,289,1,0,0,0,289,291,1,0,0,0,290,288,1,0,0,0,291,292,6,47,0,0,
		292,96,1,0,0,0,293,294,5,35,0,0,294,295,5,61,0,0,295,299,1,0,0,0,296,298,
		9,0,0,0,297,296,1,0,0,0,298,301,1,0,0,0,299,300,1,0,0,0,299,297,1,0,0,
		0,300,302,1,0,0,0,301,299,1,0,0,0,302,303,5,61,0,0,303,304,5,35,0,0,304,
		305,1,0,0,0,305,306,6,48,0,0,306,98,1,0,0,0,307,309,7,3,0,0,308,307,1,
		0,0,0,309,310,1,0,0,0,310,308,1,0,0,0,310,311,1,0,0,0,311,312,1,0,0,0,
		312,313,6,49,0,0,313,100,1,0,0,0,12,0,107,243,248,254,260,274,279,281,
		288,299,310,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
