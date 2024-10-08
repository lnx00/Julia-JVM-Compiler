grammar Julia;

/* Parser */
start: (function | statement)* EOF;

// Functions
function: FUNCTION_T IDENTIFIER parameters (DCOLON type)? body END_T;
parameters: LPAREN (IDENTIFIER DCOLON type (COMMA IDENTIFIER DCOLON type)*)? RPAREN;

// Control flow
if: IF_T expression body (ELSE_T body)? END_T; // if x then y else z
while: WHILE_T expression body END_T; // while x y
block: BEGIN_T body END_T; // begin x end
body: statement*;

// Statements
statement: declaration | assignment | call | return | if | while | block;
declaration: IDENTIFIER DCOLON type EQ expression; // x::T = y
assignment: IDENTIFIER EQ expression; // x = y
call: IDENTIFIER LPAREN (expression (COMMA expression)*)? RPAREN; // f(x, y)
return: RETURN_T expression?; // return x

// Expressions
expression: unaryOp expression # UnaryExpr
    | expression multOp expression # MultExpr
    | expression addOp expression # AddExpr
    | expression compOp expression # CompExpr
    | expression boolOp=AND expression # BoolExpr
    | expression boolOp=OR expression # BoolExpr
    | LPAREN expression RPAREN # ParenExpr
    | call # CallExpr
    | const # ConstExpr
    | IDENTIFIER # VarExpr
    ;

// Operators
multOp: STAR | SLASH | PERCENT;
addOp: PLUS | MINUS;
compOp: EQEQ | LT | GT | LTE | GTE | NEQ;
unaryOp: PLUS | MINUS | NOT;

// Helper
const: intValue | floatValue | STRCONST | BOOLCONST;
intValue: (PLUS | MINUS)? INTCONST;
floatValue: (PLUS | MINUS)? FLTCONST;

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
INTCONST: [0-9]+;
FLTCONST: [0-9]+ DOT [0-9]+;
STRCONST: '"' .*? '"';
BOOLCONST: 'true' | 'false';

// Identifiers
IDENTIFIER: Letter (LetterOrDigit | '_')*;

// Comments and whitespace
COMMENT: '#' ~[\r\n]* -> skip;
COMMENT_BLOCK: '#=' .*? '=#' -> skip;
WS: [ \t\r\n]+ -> skip;
