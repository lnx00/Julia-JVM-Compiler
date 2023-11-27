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
expression: expression (STAR | SLASH | PERCENT) expression // x * y, x / y, x % y
    | expression (PLUS | MINUS) expression // x + y, x - y
    | expression (EQEQ | LT | GT | LTE | GTE | NEQ) expression // x == y, x < y, x > y, x <= y, x >= y, x != y
    | expression (AND | OR) expression // x && y, x || y
    | NOT expression // !x
    | LPAREN expression RPAREN // (x)
    | call // f(x, y)
    | IDENTIFIER
    | INTCONST
    | FLTCONST
    | STRCONST
    | BOOLCONST;
    
// Helper
type: INTEGER_T | FLOAT64_T | BOOL_T | STRING_T;

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
TRUE_T: 'true';
FALSE_T: 'false';
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

// Arithmetic operators
PLUS: '+';
MINUS: '-';
STAR: '*';
SLASH: '/';
PERCENT: '%';

// Comparison operators
EQEQ: '==';
LT: '<';
GT: '>';
LTE: '<=';
GTE: '>=';
NEQ: '!=';

// Boolean operators
AND: '&&';
OR: '||';
NOT: '!';

// Assignment operators
EQ: '=';

IDENTIFIER: Letter LetterOrDigit*;

// Literals
INTCONST: MINUS? [0-9]+;
FLTCONST: MINUS? [0-9]+ DOT [0-9]+;
STRCONST: '"' .*? '"';
BOOLCONST: TRUE_T | FALSE_T;

// Comments and whitespace
COMMENT: '#' ~[\r\n]* -> skip;
COMMENT_BLOCK: '#=' .*? '=#' -> skip;
WS: [ \t\r\n]+ -> skip;
NL: [\r\n]+ -> skip;
