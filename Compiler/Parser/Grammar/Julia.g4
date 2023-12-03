grammar Julia;

/* Parser */
start: (function | statement)* EOF;

// Functions
function: FUNCTION_T IDENTIFIER parameters return_type? statement* END_T;
parameters: LPAREN (IDENTIFIER DCOLON type (COMMA IDENTIFIER DCOLON type)*)? RPAREN;
return_type: DCOLON type;

// Control flow
if: IF_T expression statement* (ELSE_T statement*)? END_T; // if x then y else z
while: WHILE_T expression statement* END_T; // while x y
block: BEGIN_T statement* END_T; // begin x end

// Statements
statement: declaration | assignment | call | return | if | while | block;
declaration: IDENTIFIER DCOLON type EQ expression; // x::T = y
assignment: IDENTIFIER EQ expression; // x = y
call: IDENTIFIER LPAREN (expression (COMMA expression)*)? RPAREN; // f(x, y)
return: RETURN_T expression; // return x

// Expressions
expression: expression multOp expression # MultExpr
    | expression addOp expression # AddExpr
    | expression compOp expression # CompExpr
    | expression boolOp expression # BoolExpr
    | NOT expression # NotExpr
    | LPAREN expression RPAREN # ParenExpr
    | call # CallExpr
    | const # ConstExpr
    | IDENTIFIER # VarExpr
    ;

// Operators
multOp: STAR | SLASH | PERCENT;
addOp: PLUS | MINUS;
compOp: EQEQ | LT | GT | LTE | GTE | NEQ;
boolOp: AND | OR;

// Helper
const: INTCONST | FLTCONST | STRCONST | BOOLCONST;
type: INTEGER_T | FLOAT64_T | STRING_T | BOOL_T;

/* Lexer */

fragment Letter: [a-zA-Z];
fragment Digit: [0-9];
fragment LetterOrDigit: Letter | Digit;

// Tokens
FUNCTION_T: 'function';
RETURN_T: 'return';
BEGIN_T: 'begin';
END_T: 'end';
IF_T: 'if';
ELSE_T: 'else';
WHILE_T: 'while';
//TRUE_T: 'true';
//FALSE_T: 'false';
INTEGER_T: 'Integer';
FLOAT64_T: 'Float64';
BOOL_T: 'Bool';
STRING_T: 'String';

// Brackets and punctuation
LBRACE: '{';
RBRACE: '}';
LPAREN: '(';
RPAREN: ')';
LBRACK: '[';
RBRACK: ']';
DCOLON: '::';
COMMA: ',';
SEMICOLON: ';';
COLON: ':';
DOT: '.';
EQ: '=';
AMP: '&';
PIPE: '|';

// Arithmetic operators
PLUS: '+';
MINUS: '-';
STAR: '*';
SLASH: '/';
PERCENT: '%';

// Boolean operators
AND: AMP AMP;
OR: PIPE PIPE;
NOT: '!';

// Comparison operators
EQEQ: EQ EQ;
LT: '<';
GT: '>';
LTE: LT EQ;
GTE: GT EQ;
NEQ: NOT EQ;

// Literals
INTCONST: MINUS? [0-9]+;
FLTCONST: MINUS? [0-9]+ DOT [0-9]+;
STRCONST: '"' .*? '"';
BOOLCONST: 'true' | 'false';

// Identifiers
IDENTIFIER: Letter LetterOrDigit*;

// Comments and whitespace
COMMENT: '#' ~[\r\n]* -> skip;
COMMENT_BLOCK: '#=' .*? '=#' -> skip;
WS: [ \t\r\n]+ -> skip;
NL: [\r\n]+ -> skip;
